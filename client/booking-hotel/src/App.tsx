import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import "./App.css";
import { TooltipProvider } from "@/components/ui/atoms";
import { Toaster, SonnerToaster as Sonner } from "@/components/ui/organisms";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import HotelDetail from "./features/hotel/components/HotelDetail";
import Home from "./pages/HomePage";
import Login from "./pages/LoginPage";

const queryClient = new QueryClient();

const App = () => (
  <QueryClientProvider client={queryClient}>
    <TooltipProvider>
      <Toaster />
      <Sonner />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/hotel/:id" element={<HotelDetail />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </BrowserRouter>
    </TooltipProvider>
  </QueryClientProvider>
);

export default App;
