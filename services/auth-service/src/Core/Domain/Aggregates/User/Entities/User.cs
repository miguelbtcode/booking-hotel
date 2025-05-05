using Domain.Aggregates.User.Enums;
using Domain.Aggregates.User.Errors;
using Domain.Aggregates.User.Events;
using Domain.Aggregates.User.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.User.Entities
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public UserName UserName { get; private set; } = null!;
        public PersonName Name { get; private set; } = null!;
        public PhoneNumber? PhoneNumber { get; private set; }
        public UserStatus Status { get; private set; }
        public ExternalId? KeycloakId { get; private set; }
        
        private readonly List<Role.Entities.Role> _roles = [];
        public IReadOnlyCollection<Role.Entities.Role> Roles => _roles.AsReadOnly();
        
        private User() { } // Para EF Core
        
        private User(
            UserId id,
            Email email,
            Password password,
            UserName userName,
            PersonName name,
            PhoneNumber? phoneNumber,
            UserStatus status = UserStatus.Active,
            ExternalId? keycloakId = null) : base(id)
        {
            Id = id;
            Email = email;
            Password = password;
            UserName = userName;
            Name = name;
            PhoneNumber = phoneNumber;
            Status = status;
            KeycloakId = keycloakId;
        }

        public static Result<User> Create(
            Email email,
            Password password,
            UserName userName,
            PersonName name,
            PhoneNumber? phoneNumber = null,
            string? externalId = null)
        {
            var userId = UserId.CreateUnique();

            var user = new User(
                userId,
                email,
                password,
                userName,
                name,
                phoneNumber,
                UserStatus.Active,
                externalId is null ? null : ExternalId.Create(externalId));

            user.RaiseDomainEvent(new UserCreatedDomainEvent(userId));
            
            return Result.Success(user);
        }
        
        public Result AssignRole(Role.Entities.Role role)
        {
            if (_roles.Contains(role))
                return Result.Failure(UserErrors.RoleAlreadyAssigned);
            
            _roles.Add(role);
            SetModifiedDate();

            RaiseDomainEvent(new RoleAssignedDomainEvent(Id, role.Id));
            
            return Result.Success();
        }
        
        public Result RemoveRole(Role.Entities.Role role)
        {
            if (!_roles.Contains(role))
                return Result.Failure(UserErrors.RoleNotAssigned);
            
            _roles.Remove(role);
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result ChangeEmail(Email email)
        {
            if (Email.Equals(email))
                return Result.Failure(UserErrors.EmailNotChanged);
            
            Email = email;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result ChangeUserName(UserName userName)
        {
            if (UserName.Equals(userName))
                return Result.Failure(UserErrors.UserNameNotChanged);
            
            UserName = userName;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result ChangePassword(Password password)
        {
            Password = password;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result UpdatePersonalInfo(PersonName name, PhoneNumber phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public void Activate()
        {
            if (Status == UserStatus.Active)
                return;
            
            Status = UserStatus.Active;
            SetModifiedDate();
        }
        
        public void Deactivate()
        {
            if (Status == UserStatus.Inactive)
                return;
            
            Status = UserStatus.Inactive;
            SetModifiedDate();
        }
        
        public void Block()
        {
            if (Status == UserStatus.Blocked)
                return;
            
            Status = UserStatus.Blocked;
            SetModifiedDate();
        }
        
        public void SetPendingVerification()
        {
            if (Status == UserStatus.PendingVerification)
                return;
            
            Status = UserStatus.PendingVerification;
            SetModifiedDate();
        }
        
        public void SetExternalId(string externalId)
        {
            KeycloakId = ExternalId.Create(externalId);
            SetModifiedDate();
        }
    }
}