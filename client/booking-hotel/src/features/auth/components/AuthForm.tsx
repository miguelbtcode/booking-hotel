import { useState } from "react";
import { Button, Input } from "@/components/ui/atoms";
import { FcGoogle } from "react-icons/fc";
// import { signInWithEmail, signInWithGoogle, signUpWithEmail } from "@/lib/auth";
import { useToast } from "@/hooks/useToast";

export const AuthForm = () => {
  const [isLogin, setIsLogin] = useState(true);
  const [loading, setLoading] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const { toast } = useToast();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      if (isLogin) {
        // await signInWithEmail(email, password);
      } else {
        // await signUpWithEmail(email, password, firstName, lastName);
      }
      toast({
        title: isLogin ? "Inicio de sesión exitoso" : "Registro exitoso",
        description: isLogin
          ? "Bienvenido de vuelta!"
          : "Tu cuenta ha sido creada",
      });
    } catch (error: any) {
      toast({
        title: "Error",
        description: error.message,
        variant: "destructive",
      });
    } finally {
      setLoading(false);
    }
  };

  const handleGoogleSignIn = async () => {
    try {
      //   await signInWithGoogle();
    } catch (error: any) {
      toast({
        title: "Error",
        description: error.message,
        variant: "destructive",
      });
    }
  };

  return (
    <div className="w-full max-w-md space-y-8 p-8 bg-white rounded-xl shadow-lg">
      <div className="text-center">
        <h2 className="text-2xl font-bold">
          {isLogin ? "Iniciar Sesión" : "Crear Cuenta"}
        </h2>
      </div>

      <form onSubmit={handleSubmit} className="space-y-6">
        {!isLogin && (
          <>
            <Input
              type="text"
              placeholder="Nombre"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
              required
            />
            <Input
              type="text"
              placeholder="Apellido"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
              required
            />
          </>
        )}
        <Input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <Input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <Button type="submit" className="w-full" disabled={loading}>
          {loading ? "Cargando..." : isLogin ? "Iniciar Sesión" : "Registrarse"}
        </Button>
      </form>

      <div className="relative">
        <div className="absolute inset-0 flex items-center">
          <div className="w-full border-t border-gray-300" />
        </div>
        <div className="relative flex justify-center text-sm">
          <span className="px-2 bg-white text-gray-500">O continuar con</span>
        </div>
      </div>

      <Button
        type="button"
        variant="outline"
        className="w-full"
        onClick={handleGoogleSignIn}
      >
        <FcGoogle className="mr-2 h-5 w-5" />
        Google
      </Button>

      <p className="text-center text-sm">
        {isLogin ? "¿No tienes una cuenta?" : "¿Ya tienes una cuenta?"}{" "}
        <button
          type="button"
          className="text-primary hover:underline"
          onClick={() => setIsLogin(!isLogin)}
        >
          {isLogin ? "Regístrate" : "Inicia sesión"}
        </button>
      </p>
    </div>
  );
};
