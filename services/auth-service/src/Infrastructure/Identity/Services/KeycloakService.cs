using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.Aggregates.User.Entities;
using Infrastructure.Identity.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Services
{
    public class KeycloakService
    {
        private readonly KeycloakSettings _keycloakSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<KeycloakService> _logger;
        private string _adminToken;
        private DateTime _adminTokenExpiry = DateTime.MinValue;

        public KeycloakService(
            IOptions<KeycloakSettings> keycloakSettings,
            IHttpClientFactory httpClientFactory,
            ILogger<KeycloakService> logger)
        {
            _keycloakSettings = keycloakSettings.Value;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        #region User Management

        public async Task<string> CreateUserInKeycloakAsync(User user, string password)
        {
            try
            {
                // Asegurar que tenemos un token de administrador válido
                await EnsureAdminTokenAsync();

                var keycloakUser = new KeycloakUser
                {
                    Username = user.UserName.Value,
                    Email = user.Email.Value,
                    Enabled = true,
                    FirstName = user.Name.FirstName,
                    LastName = user.Name.LastName,
                    Credentials = new List<KeycloakCredential>
                    {
                        new KeycloakCredential
                        {
                            Type = "password",
                            Value = password,
                            Temporary = false
                        }
                    }
                };

                var client = CreateAdminClient();
                var response = await client.PostAsJsonAsync(
                    $"admin/realms/{_keycloakSettings.Realm}/users", 
                    keycloakUser);

                response.EnsureSuccessStatusCode();

                // Keycloak no devuelve el ID en la respuesta directamente,
                // necesitamos recuperar el usuario mediante username
                var userResponse = await client.GetAsync(
                    $"admin/realms/{_keycloakSettings.Realm}/users?username={Uri.EscapeDataString(user.UserName.Value)}");
                
                userResponse.EnsureSuccessStatusCode();
                
                var users = await JsonSerializer.DeserializeAsync<List<KeycloakUser>>(
                    await userResponse.Content.ReadAsStreamAsync());
                
                if (users != null && users.Count > 0)
                {
                    return users[0].Id;
                }

                throw new Exception("User created but ID could not be retrieved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user in Keycloak: {Message}", ex.Message);
                throw;
            }
        }

        public async Task UpdateUserInKeycloakAsync(User user)
        {
            try
            {
                await EnsureAdminTokenAsync();

                if (user.KeycloakId == null || string.IsNullOrEmpty(user.KeycloakId.Value))
                {
                    throw new ArgumentNullException(nameof(user.KeycloakId), "Keycloak ID is required for update operation");
                }

                var keycloakUser = new KeycloakUser
                {
                    Username = user.UserName.Value,
                    Email = user.Email.Value,
                    Enabled = user.Status == Domain.Aggregates.User.Enums.UserStatus.Active,
                    FirstName = user.Name.FirstName,
                    LastName = user.Name.LastName
                };

                var client = CreateAdminClient();
                var response = await client.PutAsJsonAsync(
                    $"admin/realms/{_keycloakSettings.Realm}/users/{user.KeycloakId.Value}", 
                    keycloakUser);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user in Keycloak: {Message}", ex.Message);
                throw;
            }
        }

        public async Task DeleteUserInKeycloakAsync(string keycloakId)
        {
            try
            {
                await EnsureAdminTokenAsync();

                var client = CreateAdminClient();
                var response = await client.DeleteAsync(
                    $"admin/realms/{_keycloakSettings.Realm}/users/{keycloakId}");

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user from Keycloak: {Message}", ex.Message);
                throw;
            }
        }

        public async Task AssignRolesToUserInKeycloakAsync(string keycloakId, IEnumerable<string> roleNames)
        {
            try
            {
                await EnsureAdminTokenAsync();
                var client = CreateAdminClient();

                // Obtener todos los roles disponibles
                var rolesResponse = await client.GetAsync(
                    $"admin/realms/{_keycloakSettings.Realm}/roles");
                
                rolesResponse.EnsureSuccessStatusCode();
                
                var allRoles = await JsonSerializer.DeserializeAsync<List<KeycloakRole>>(
                    await rolesResponse.Content.ReadAsStreamAsync());

                if (allRoles == null)
                {
                    throw new Exception("Could not retrieve roles from Keycloak");
                }

                // Filtrar los roles que deseamos asignar
                var rolesToAssign = allRoles
                    .Where(r => roleNames.Contains(r.Name))
                    .ToList();

                // Asignar los roles al usuario
                var response = await client.PostAsJsonAsync(
                    $"admin/realms/{_keycloakSettings.Realm}/users/{keycloakId}/role-mappings/realm",
                    rolesToAssign);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning roles to user in Keycloak: {Message}", ex.Message);
                throw;
            }
        }

        #endregion

        #region Authentication

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = _keycloakSettings.ClientId,
                    ["client_secret"] = _keycloakSettings.ClientSecret,
                    ["grant_type"] = "password",
                    ["username"] = username,
                    ["password"] = password
                });

                var response = await client.PostAsync(
                    $"{_keycloakSettings.Authority}/realms/{_keycloakSettings.Realm}/protocol/openid-connect/token", 
                    content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating credentials in Keycloak: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<string> GetTokenAsync(string username)
        {
            try
            {
                // Para un caso real, necesitarías almacenar las credenciales del usuario
                // o utilizar un flujo de OAuth más apropiado como "client_credentials"
                // Esta es una implementación simplificada
                var client = _httpClientFactory.CreateClient();
                
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = _keycloakSettings.ClientId,
                    ["client_secret"] = _keycloakSettings.ClientSecret,
                    ["grant_type"] = "client_credentials",
                    ["scope"] = "openid profile email"
                });

                var response = await client.PostAsync(
                    $"{_keycloakSettings.Authority}/realms/{_keycloakSettings.Realm}/protocol/openid-connect/token", 
                    content);

                response.EnsureSuccessStatusCode();

                var tokenResponse = await JsonSerializer.DeserializeAsync<KeycloakTokenResponse>(
                    await response.Content.ReadAsStreamAsync());

                return tokenResponse?.AccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting token from Keycloak: {Message}", ex.Message);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        private async Task EnsureAdminTokenAsync()
        {
            if (DateTime.UtcNow < _adminTokenExpiry && !string.IsNullOrEmpty(_adminToken))
            {
                return;
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = "admin-cli",
                    ["username"] = _keycloakSettings.AdminUsername,
                    ["password"] = _keycloakSettings.AdminPassword,
                    ["grant_type"] = "password"
                });

                var response = await client.PostAsync(
                    $"{_keycloakSettings.Authority}/realms/master/protocol/openid-connect/token", 
                    content);

                response.EnsureSuccessStatusCode();

                var tokenResponse = await JsonSerializer.DeserializeAsync<KeycloakTokenResponse>(
                    await response.Content.ReadAsStreamAsync());

                if (tokenResponse != null)
                {
                    _adminToken = tokenResponse.AccessToken;
                    _adminTokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 30); // 30 segundos de margen
                }
                else
                {
                    throw new Exception("Failed to obtain admin token from Keycloak");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obtaining admin token from Keycloak: {Message}", ex.Message);
                throw;
            }
        }

        private HttpClient CreateAdminClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken);
            client.BaseAddress = new Uri(_keycloakSettings.Authority);
            return client;
        }

        #endregion
    }

    public class KeycloakUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public List<KeycloakCredential> Credentials { get; set; }
    }

    public class KeycloakCredential
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Temporary { get; set; }
    }

    public class KeycloakRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class KeycloakTokenResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}