import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Input, Label, Checkbox } from "@/components/ui/atoms";
import { Eye, EyeOff, Loader2, Mail, Lock, User, Shield } from "lucide-react";

const RegisterForm = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
    acceptTerms: false,
  });
  const [focusedInput, setFocusedInput] = useState(null);
  const [isValidEmail, setIsValidEmail] = useState(null);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
    
    if (name === "email" && value.length > 0) {
      setIsValidEmail(/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value));
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    // Simulate registration (automatically redirect after 1.5 seconds)
    setTimeout(() => {
      navigate("/");
    }, 1500);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div className="space-y-2">
        <div className="relative">
          <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500">
            <User size={18} />
          </div>
          <Label
            htmlFor="name"
            className={`absolute left-10 transition-all duration-200 ${
              focusedInput === "name" || formData.name
                ? "-top-2 text-xs bg-white px-1 text-black font-medium"
                : "top-1/2 -translate-y-1/2 text-gray-500"
            }`}
          >
            Nombre completo
          </Label>
          <Input
            id="name"
            name="name"
            value={formData.name}
            onChange={handleChange}
            onFocus={() => setFocusedInput("name")}
            onBlur={() => setFocusedInput(null)}
            className={`pl-10 pt-4 transition-all border-2 rounded-lg ${
              focusedInput === "name" 
                ? "border-black ring-2 ring-gray-100" 
                : "border-gray-200"
            }`}
            required
          />
        </div>
      </div>

      <div className="space-y-2">
        <div className="relative">
          <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500">
            <Mail size={18} />
          </div>
          <Label
            htmlFor="register-email"
            className={`absolute left-10 transition-all duration-200 ${
              focusedInput === "register-email" || formData.email
                ? "-top-2 text-xs bg-white px-1 text-black font-medium"
                : "top-1/2 -translate-y-1/2 text-gray-500"
            }`}
          >
            Correo electrónico
          </Label>
          <Input
            id="register-email"
            name="email"
            type="email"
            value={formData.email}
            onChange={handleChange}
            onFocus={() => setFocusedInput("register-email")}
            onBlur={() => setFocusedInput(null)}
            className={`pl-10 pt-4 transition-all border-2 rounded-lg ${
              focusedInput === "register-email"
                ? "border-black ring-2 ring-gray-100"
                : isValidEmail === false && formData.email
                ? "border-red-500"
                : isValidEmail && formData.email
                ? "border-green-500"
                : "border-gray-200"
            }`}
            required
          />
          {isValidEmail === false && formData.email && (
            <p className="text-red-500 text-xs mt-1 ml-1">
              Por favor, ingresa un correo válido
            </p>
          )}
        </div>
      </div>

      <div className="space-y-2">
        <div className="relative">
          <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500">
            <Lock size={18} />
          </div>
          <Label
            htmlFor="register-password"
            className={`absolute left-10 transition-all duration-200 ${
              focusedInput === "register-password" || formData.password
                ? "-top-2 text-xs bg-white px-1 text-black font-medium"
                : "top-1/2 -translate-y-1/2 text-gray-500"
            }`}
          >
            Contraseña
          </Label>
          <Input
            id="register-password"
            name="password"
            type={showPassword ? "text" : "password"}
            value={formData.password}
            onChange={handleChange}
            onFocus={() => setFocusedInput("register-password")}
            onBlur={() => setFocusedInput(null)}
            className={`pl-10 pt-4 pr-10 transition-all border-2 rounded-lg ${
              focusedInput === "register-password"
                ? "border-black ring-2 ring-gray-100"
                : "border-gray-200"
            }`}
            required
          />
          <button
            type="button"
            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-black transition-colors"
            onClick={() => setShowPassword(!showPassword)}
          >
            {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
          </button>
        </div>
      </div>

      <div className="space-y-2">
        <div className="relative">
          <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-gray-500">
            <Shield size={18} />
          </div>
          <Label
            htmlFor="confirm-password"
            className={`absolute left-10 transition-all duration-200 ${
              focusedInput === "confirm-password" || formData.confirmPassword
                ? "-top-2 text-xs bg-white px-1 text-black font-medium"
                : "top-1/2 -translate-y-1/2 text-gray-500"
            }`}
          >
            Confirmar contraseña
          </Label>
          <Input
            id="confirm-password"
            name="confirmPassword"
            type="password"
            value={formData.confirmPassword}
            onChange={handleChange}
            onFocus={() => setFocusedInput("confirm-password")}
            onBlur={() => setFocusedInput(null)}
            className={`pl-10 pt-4 transition-all border-2 rounded-lg ${
              focusedInput === "confirm-password"
                ? "border-black ring-2 ring-gray-100"
                : formData.confirmPassword &&
                  formData.confirmPassword !== formData.password
                ? "border-red-500"
                : formData.confirmPassword &&
                  formData.confirmPassword === formData.password
                ? "border-green-500"
                : "border-gray-200"
            }`}
            required
          />
          {formData.confirmPassword &&
            formData.confirmPassword !== formData.password && (
              <p className="text-red-500 text-xs mt-1 ml-1">
                Las contraseñas no coinciden
              </p>
            )}
        </div>
      </div>

      <div className="flex items-start space-x-3 mt-6">
        <Checkbox
          id="terms"
          name="acceptTerms"
          checked={formData.acceptTerms}
          onCheckedChange={(checked) =>
            setFormData((prev) => ({ ...prev, acceptTerms: checked === true }))
          }
          required
          className="border-gray-300 text-black focus:ring-gray-500 mt-1"
        />
        <Label htmlFor="terms" className="text-sm text-gray-600">
          Acepto los{" "}
          <a href="#" className="text-black font-medium hover:underline">
            términos y condiciones
          </a>{" "}
          y la{" "}
          <a href="#" className="text-black font-medium hover:underline">
            política de privacidad
          </a>
        </Label>
      </div>

      <Button
        type="submit"
        className="w-full h-12 bg-black text-white hover:bg-gray-900 transition-all shadow-lg rounded-lg mt-2"
        disabled={loading}
      >
        {loading ? (
          <span className="flex items-center justify-center">
            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
            Creando cuenta...
          </span>
        ) : (
          "Crear cuenta"
        )}
      </Button>
    </form>
  );
};

export default RegisterForm;