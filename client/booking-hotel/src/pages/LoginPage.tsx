import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/components/ui/molecules";
import { LoginCarousel } from "@/features/auth/components/LoginCarousel";
import SplitLayout from "@/components/layouts/SplitLayout";
import LoginForm from "@/features/auth/components/LoginForm";
import RegisterForm from "@/features/auth/components/RegisterForm";
import SocialLoginButtons from "@/features/auth/components/SocialLoginButtons";

const Login = () => {
  return (
    <SplitLayout
      title="BookFast"
      subtitle="Tu destino para hospedajes exclusivos"
      left={<LoginCarousel />}
      right={
        <Card className="border-none shadow-2xl rounded-xl overflow-hidden bg-white">
          <CardHeader className="pb-0 pt-8">
            <CardTitle className="text-3xl font-bold text-center text-gray-900">
              Bienvenido
            </CardTitle>
            <CardDescription className="text-center text-gray-600 text-base mt-2">
              Inicia sesión o crea una cuenta para continuar
            </CardDescription>
          </CardHeader>

          <CardContent className="pt-8 px-10 pb-10">
            <Tabs defaultValue="login" className="w-full">
              <TabsList className="grid grid-cols-2 mb-8 bg-gray-100 p-1 rounded-lg">
                <TabsTrigger
                  value="login"
                  className="data-[state=active]:bg-white data-[state=active]:text-black data-[state=active]:shadow-sm rounded-md transition-all py-2.5 text-sm font-medium"
                >
                  Iniciar sesión
                </TabsTrigger>
                <TabsTrigger
                  value="register"
                  className="data-[state=active]:bg-white data-[state=active]:text-black data-[state=active]:shadow-sm rounded-md transition-all py-2.5 text-sm font-medium"
                >
                  Registrarse
                </TabsTrigger>
              </TabsList>

              <TabsContent value="login">
                <LoginForm />
              </TabsContent>

              <TabsContent value="register">
                <RegisterForm />
              </TabsContent>
            </Tabs>

            <SocialLoginButtons />
          </CardContent>
        </Card>
      }
    />
  );
};

export default Login;
