import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/atoms";
import { FcGoogle } from "react-icons/fc";
import { FaFacebook, FaApple } from "react-icons/fa";
import { RiTwitterXFill } from "react-icons/ri";
import { Loader2 } from "lucide-react";

const SocialLoginButtons = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [provider, setProvider] = useState("");
  const [mounted, setMounted] = useState(false);
  const [showExtra, setShowExtra] = useState(false);

  // Efecto para animación de entrada
  useEffect(() => {
    setMounted(true);
  }, []);

  const handleSocialLogin = (providerName) => {
    setLoading(true);
    setProvider(providerName);

    // Tracking de análisis
    console.log(`Login attempt with: ${providerName}`);

    // Simular login con redes sociales
    setTimeout(() => {
      // Tracking de análisis - Login exitoso
      console.log(`Login success with: ${providerName}`);

      // Guardar proveedor de login para personalización futura
      localStorage.setItem("lastLoginProvider", providerName);

      navigate("/");
    }, 1500);
  };

  // Mostrar botones adicionales
  const toggleExtraProviders = () => {
    setShowExtra(!showExtra);
  };

  return (
    <div
      className={`space-y-6 transition-all duration-500 ${
        mounted ? "opacity-100" : "opacity-0"
      }`}
    >
      <div className="relative mt-8 mb-6">
        <div className="absolute inset-0 flex items-center">
          <div className="w-full border-t border-gray-200" />
        </div>
        <div className="relative flex justify-center text-sm">
          <span className="px-2 bg-white text-gray-500 font-medium">
            O continuar con
          </span>
        </div>
      </div>

      <div className="grid grid-cols-2 gap-4">
        {/* Botón de Google */}
        <Button
          type="button"
          variant="outline"
          className="w-full h-12 flex items-center justify-center gap-2 transition-all rounded-lg border-2 border-gray-200 hover:border-gray-300 hover:bg-gray-50 relative overflow-hidden group"
          onClick={() => handleSocialLogin("google")}
          disabled={loading}
        >
          <div className="absolute inset-0 w-0 bg-gray-50 transition-all duration-300 ease-out group-hover:w-full"></div>
          {loading && provider === "google" ? (
            <span className="flex items-center justify-center z-10">
              <Loader2 className="h-5 w-5 animate-spin mr-2" />
              <span>Conectando...</span>
            </span>
          ) : (
            <>
              <FcGoogle className="h-5 w-5 z-10" />
              <span className="font-medium z-10">Google</span>
            </>
          )}
        </Button>

        {/* Botón de Facebook */}
        <Button
          type="button"
          variant="outline"
          className="w-full h-12 flex items-center justify-center gap-2 transition-all rounded-lg border-2 border-gray-200 hover:border-blue-200 hover:bg-blue-50 text-blue-600 relative overflow-hidden group"
          onClick={() => handleSocialLogin("facebook")}
          disabled={loading}
        >
          <div className="absolute inset-0 w-0 bg-blue-50 transition-all duration-300 ease-out group-hover:w-full"></div>
          {loading && provider === "facebook" ? (
            <span className="flex items-center justify-center text-gray-700 z-10">
              <Loader2 className="h-5 w-5 animate-spin mr-2" />
              <span>Conectando...</span>
            </span>
          ) : (
            <>
              <FaFacebook className="h-5 w-5 z-10" />
              <span className="font-medium z-10">Facebook</span>
            </>
          )}
        </Button>
      </div>

      {/* Botones adicionales con animación */}
      <div
        className={`grid grid-cols-2 gap-4 transition-all duration-500 ${
          showExtra
            ? "opacity-100 max-h-24"
            : "opacity-0 max-h-0 overflow-hidden"
        }`}
      >
        {/* Botón de Apple */}
        <Button
          type="button"
          variant="outline"
          className="w-full h-12 flex items-center justify-center gap-2 transition-all rounded-lg border-2 border-gray-200 hover:border-gray-900 hover:bg-gray-900 hover:text-white group"
          onClick={() => handleSocialLogin("apple")}
          disabled={loading}
        >
          {loading && provider === "apple" ? (
            <span className="flex items-center justify-center">
              <Loader2 className="h-5 w-5 animate-spin mr-2" />
              <span>Conectando...</span>
            </span>
          ) : (
            <>
              <FaApple className="h-5 w-5 group-hover:text-white transition-colors" />
              <span className="font-medium group-hover:text-white transition-colors">
                Apple
              </span>
            </>
          )}
        </Button>

        {/* Botón de Twitter/X */}
        <Button
          type="button"
          variant="outline"
          className="w-full h-12 flex items-center justify-center gap-2 transition-all rounded-lg border-2 border-gray-200 hover:border-gray-900 hover:bg-gray-900 hover:text-white group"
          onClick={() => handleSocialLogin("twitter")}
          disabled={loading}
        >
          {loading && provider === "twitter" ? (
            <span className="flex items-center justify-center">
              <Loader2 className="h-5 w-5 animate-spin mr-2" />
              <span>Conectando...</span>
            </span>
          ) : (
            <>
              <RiTwitterXFill className="h-5 w-5 group-hover:text-white transition-colors" />
              <span className="font-medium group-hover:text-white transition-colors">
                Twitter
              </span>
            </>
          )}
        </Button>
      </div>

      {/* Texto legal e información */}
      <div className="text-center mt-6 text-sm text-gray-500 group relative">
        Al iniciar sesión o registrarte, aceptas nuestra
        <a
          href="#"
          className="text-black font-medium hover:underline mx-1 relative inline-block"
        >
          política de privacidad
          <span className="absolute bottom-0 left-0 w-full h-0.5 bg-primary/20 transform scale-x-0 group-hover:scale-x-100 transition-transform duration-300 origin-left"></span>
        </a>
        y
        <a
          href="#"
          className="text-black font-medium hover:underline ml-1 relative inline-block"
        >
          términos de servicio
          <span className="absolute bottom-0 left-0 w-full h-0.5 bg-primary/20 transform scale-x-0 group-hover:scale-x-100 transition-transform duration-300 origin-left"></span>
        </a>
        {/* Información de seguridad - visible al hacer hover */}
        <div className="hidden group-hover:block absolute -top-12 left-1/2 transform -translate-x-1/2 bg-white shadow-lg p-2 rounded-lg border border-gray-100 text-xs text-gray-600 w-64 z-10 animate-fadeIn">
          <strong>Seguridad:</strong> Usamos encriptación de nivel bancario y
          nunca compartimos tus datos
        </div>
      </div>
    </div>
  );
};

export default SocialLoginButtons;
