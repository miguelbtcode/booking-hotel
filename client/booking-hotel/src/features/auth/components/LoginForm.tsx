import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
  Button,
  Input,
  Label,
  Checkbox,
} from "@/components/ui/atoms";
import {
  Eye,
  EyeOff,
  Loader2,
  Mail,
  Lock,
  Check,
  AlertCircle,
  Info,
} from "lucide-react";

const LoginForm = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const [focusedInput, setFocusedInput] = useState(null);
  const [isValidEmail, setIsValidEmail] = useState(null);
  const [passwordStrength, setPasswordStrength] = useState(0);
  const [validCriteria, setValidCriteria] = useState({
    length: false,
    number: false,
    special: false,
    uppercase: false,
  });
  const [showPasswordTips, setShowPasswordTips] = useState(false);

  // Efecto para animación de entrada
  useEffect(() => {
    // Simulación de autocompletar para demostración
    const lastEmail = localStorage.getItem("lastEmail");
    if (lastEmail) {
      setTimeout(() => {
        setFormData((prev) => ({ ...prev, email: lastEmail }));
        setIsValidEmail(true);
      }, 500);
    }
  }, []);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));

    if (name === "email" && value.length > 0) {
      setIsValidEmail(/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value));
    }

    if (name === "password") {
      // Validar criterios de contraseña
      const criteria = {
        length: value.length >= 8,
        number: /\d/.test(value),
        special: /[!@#$%^&*(),.?":{}|<>]/.test(value),
        uppercase: /[A-Z]/.test(value),
      };
      setValidCriteria(criteria);

      // Calcular fortaleza
      const strength = Object.values(criteria).filter(Boolean).length;
      setPasswordStrength(strength);
    }
  };

  const handleFocus = (input) => {
    setFocusedInput(input);
    if (input === "password") {
      setShowPasswordTips(true);
    }
  };

  const handleBlur = () => {
    setFocusedInput(null);
    setTimeout(() => {
      setShowPasswordTips(false);
    }, 200);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);

    // Guardamos el email para autocompletar
    localStorage.setItem("lastEmail", formData.email);

    // Simulate login with success animation
    setTimeout(() => {
      setSuccess(true);
      setTimeout(() => {
        navigate("/");
      }, 800);
    }, 1500);
  };

  // Determinar el color de fortaleza de la contraseña
  const getStrengthColor = () => {
    if (passwordStrength === 0) return "bg-gray-200";
    if (passwordStrength === 1) return "bg-red-500";
    if (passwordStrength === 2) return "bg-orange-500";
    if (passwordStrength === 3) return "bg-yellow-500";
    return "bg-green-500";
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-5">
      <div className={`space-y-3`}>
        <div className={`relative`}>
          <div
            className={`absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500 ${
              isValidEmail === false && formData.email ? "pb-4" : ""
            }`}
          >
            <Mail
              size={18}
              color={
                isValidEmail === false && formData.email ? "red" : "#6a7282"
              }
              className="transition-colors duration-300"
            />
          </div>
          <Label
            htmlFor="email"
            className={`absolute left-11 transition-all duration-200 ${
              focusedInput === "email" || formData.email
                ? "-top-2 text-xs bg-white px-1 text-black font-medium"
                : "top-1/2 -translate-y-1/2 text-gray-500"
            }`}
          >
            Correo electrónico
          </Label>
          <Input
            id="email"
            name="email"
            type="email"
            value={formData.email}
            onChange={handleChange}
            onFocus={() => handleFocus("email")}
            onBlur={handleBlur}
            className={`pl-10 pt-4 transition-all duration-300 border-2 rounded-lg ${
              focusedInput === "email"
                ? "border-primary ring-2 ring-primary/20"
                : isValidEmail === false && formData.email
                ? "border-red-500 ring-2 ring-red-200"
                : isValidEmail && formData.email
                ? "border-green-500 ring-2 ring-green-200"
                : "border-gray-200"
            }`}
            required
            autoComplete="email"
          />

          {/* Indicador de validación */}
          {formData.email && (
            <div
              className={`absolute right-3 top-1/2 transform -translate-y-1/2 transition-opacity duration-300 ${
                isValidEmail === false && formData.email ? "pb-4" : ""
              }`}
            >
              {isValidEmail ? (
                <Check size={18} className="text-green-500" />
              ) : (
                <AlertCircle size={18} className="text-red-500" />
              )}
            </div>
          )}

          {isValidEmail === false && formData.email && (
            <div className="mt-1 flex items-center text-red-500 text-xs animate-shake">
              <AlertCircle
                size={12}
                className="mr-1.5 flex-shrink-0 animate-pulse"
              />
              Por favor, ingresa un correo válido
            </div>
          )}
        </div>
      </div>

      <div className="space-y-2">
        <div className="flex items-center justify-between">
          <div className="relative w-full">
            <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500">
              <Lock size={18} className="transition-colors duration-300" />
            </div>
            <Label
              htmlFor="password"
              className={`absolute left-10 transition-all duration-200 ${
                focusedInput === "password" || formData.password
                  ? "-top-2 text-xs bg-white px-1 text-black font-medium"
                  : "top-1/2 -translate-y-1/2 text-gray-500"
              }`}
            >
              Contraseña
            </Label>
            <Input
              id="password"
              name="password"
              type={showPassword ? "text" : "password"}
              value={formData.password}
              onChange={handleChange}
              onFocus={() => handleFocus("password")}
              onBlur={handleBlur}
              className={`pl-10 pt-4 pr-10 transition-all duration-300 border-2 rounded-lg ${
                focusedInput === "password"
                  ? "border-primary ring-2 ring-primary/20"
                  : "border-gray-200"
              }`}
              required
              autoComplete="current-password"
            />

            {/* Indicador de fortaleza de contraseña */}
            {formData.password && (
              <div className="h-1 w-full mt-1 rounded-full bg-gray-100 overflow-hidden">
                <div
                  className={`
                    h-full 
                    transition-all 
                    duration-500 
                    ease-out 
                    ${getStrengthColor()}
                    ${passwordStrength === 4 ? "animate-pulse" : ""}
                  `}
                  style={{
                    width: `${passwordStrength * 25}%`,
                    transformOrigin: "left center",
                  }}
                ></div>
              </div>
            )}

            <button
              type="button"
              className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-black transition-colors"
              onClick={() => setShowPassword(!showPassword)}
            >
              {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
            </button>

            {/* Tips de contraseña */}
            {showPasswordTips && formData.password && (
              <div className="absolute mt-1 w-full p-2 bg-white shadow-lg rounded-md border border-gray-200 z-10 text-xs">
                <p className="font-medium mb-1 text-gray-700">
                  Requisitos de contraseña:
                </p>
                <ul className="space-y-1">
                  <li
                    className={`flex items-center gap-1 ${
                      validCriteria.length ? "text-green-600" : "text-gray-500"
                    }`}
                  >
                    <Check
                      size={12}
                      className={
                        validCriteria.length ? "opacity-100" : "opacity-0"
                      }
                    />
                    Mínimo 8 caracteres
                  </li>
                  <li
                    className={`flex items-center gap-1 ${
                      validCriteria.uppercase
                        ? "text-green-600"
                        : "text-gray-500"
                    }`}
                  >
                    <Check
                      size={12}
                      className={
                        validCriteria.uppercase ? "opacity-100" : "opacity-0"
                      }
                    />
                    Al menos una mayúscula
                  </li>
                  <li
                    className={`flex items-center gap-1 ${
                      validCriteria.number ? "text-green-600" : "text-gray-500"
                    }`}
                  >
                    <Check
                      size={12}
                      className={
                        validCriteria.number ? "opacity-100" : "opacity-0"
                      }
                    />
                    Al menos un número
                  </li>
                  <li
                    className={`flex items-center gap-1 ${
                      validCriteria.special ? "text-green-600" : "text-gray-500"
                    }`}
                  >
                    <Check
                      size={12}
                      className={
                        validCriteria.special ? "opacity-100" : "opacity-0"
                      }
                    />
                    Al menos un carácter especial
                  </li>
                </ul>
              </div>
            )}
          </div>
        </div>

        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-2">
            <Checkbox
              id="remember"
              className="border-gray-300 text-primary focus:ring-primary/25 transition-all duration-300"
            />
            <Label
              htmlFor="remember"
              className="text-sm text-gray-600 select-none"
            >
              Recordarme
            </Label>
          </div>
          <Popover>
            <PopoverTrigger asChild>
              <Button
                variant="link"
                className="p-0 h-auto text-sm text-primary font-medium hover:text-primary/80 transition-colors"
              >
                ¿Olvidaste tu contraseña?
              </Button>
            </PopoverTrigger>
            <PopoverContent className="w-80 p-5 shadow-2xl border-gray-100">
              <div className="space-y-4">
                <h4 className="font-medium text-black">
                  Recupera tu contraseña
                </h4>
                <p className="text-sm text-gray-600">
                  Ingresa tu correo electrónico y te enviaremos un enlace para
                  restablecer tu contraseña.
                </p>
                <div className="space-y-3">
                  <div className="relative">
                    <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500">
                      <Mail size={18} />
                    </div>
                    <Input
                      type="email"
                      placeholder="nombre@ejemplo.com"
                      className="pl-10 border-gray-200 focus:border-primary focus:ring-primary/25 rounded-lg transition-all duration-300"
                    />
                  </div>
                  <Button className="w-full bg-primary hover:bg-primary/90 text-white transition-all duration-300">
                    Enviar enlace
                  </Button>
                </div>
              </div>
            </PopoverContent>
          </Popover>
        </div>
      </div>

      <Button
        type="submit"
        className={`
          w-full 
          h-12 
          text-white 
          transition-all 
          duration-300 
          ease-in-out
          transform 
          hover:scale-[1.02] 
          active:scale-[0.98]
          shadow-lg 
          rounded-lg 
          relative 
          overflow-hidden ${
            success
              ? "bg-green-500 hover:bg-green-600"
              : "bg-primary hover:bg-primary/90"
          }`}
        disabled={loading || success}
      >
        <span
          className={`flex items-center justify-center transition-all duration-300 ${
            success ? "opacity-0" : "opacity-100"
          }`}
        >
          {loading ? (
            <>
              <Loader2 className="mr-2 h-4 w-4 animate-spin" />
              Iniciando sesión...
            </>
          ) : (
            "Iniciar sesión"
          )}
        </span>

        {/* Check mark animation on success */}
        {success && (
          <span className="absolute inset-0 flex items-center justify-center text-white animate-fadeIn">
            <Check className="h-6 w-6" />
          </span>
        )}

        {/* Button hover effect */}
        <span className="absolute inset-0 w-full h-full bg-black/10 transform scale-x-0 origin-left group-hover:scale-x-100 transition-transform duration-300"></span>
      </Button>
    </form>
  );
};

export default LoginForm;
