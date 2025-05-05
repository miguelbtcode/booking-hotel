import { AuthForm } from "../components/AuthForm";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
// import { supabase } from "@/integrations/supabase/client";

const Auth = () => {
  const navigate = useNavigate();

  useEffect(() => {
    // const { data: { subscription } } = supabase.auth.onAuthStateChange((event, session) => {
    //   if (session) {
    //     navigate('/');
    //   }
    // });
    // return () => subscription.unsubscribe();
  }, [navigate]);

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <AuthForm />
    </div>
  );
};

export default Auth;
