import { ArrowLeft, Sun, Moon } from "lucide-react";
import { Button } from "@/components/ui/atoms";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";

interface SplitLayoutProps {
  left: React.ReactNode;
  right: React.ReactNode;
  title?: string;
  subtitle?: string;
}

const SplitLayout = ({ left, right, title, subtitle }: SplitLayoutProps) => {
  const navigate = useNavigate();
  const [mounted, setMounted] = useState(false);
  const [scrolled, setScrolled] = useState(false);
  const [theme, setTheme] = useState<"light" | "dark">("light");
  const [logoClicks, setLogoClicks] = useState(0);
  const [easterEggActive, setEasterEggActive] = useState(false);

  // Initialize on component mount
  useEffect(() => {
    // Apply animation entrance effect
    setMounted(true);

    // Check user preference for theme
    const savedTheme = localStorage.getItem("preferred-theme");
    if (savedTheme) {
      setTheme(savedTheme as "light" | "dark");
    } else if (
      window.matchMedia &&
      window.matchMedia("(prefers-color-scheme: dark)").matches
    ) {
      setTheme("dark");
    }

    // Detect scroll for subtle header effect
    const handleScroll = () => {
      if (window.scrollY > 10) {
        setScrolled(true);
      } else {
        setScrolled(false);
      }
    };

    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  // Handle theme toggle
  const toggleTheme = () => {
    const newTheme = theme === "light" ? "dark" : "light";
    setTheme(newTheme);
    localStorage.setItem("preferred-theme", newTheme);
  };

  // Easter egg functionality
  const handleLogoClick = () => {
    setLogoClicks((prev) => {
      const newCount = prev + 1;

      // If 5 clicks in rapid succession, activate easter egg
      if (newCount >= 5) {
        setEasterEggActive(true);

        // Show easter egg notification
        const easterEggElement = document.getElementById("easter-egg-banner");
        if (easterEggElement) {
          easterEggElement.style.opacity = "1";
          easterEggElement.style.pointerEvents = "auto";

          // Hide after 5 seconds
          setTimeout(() => {
            easterEggElement.style.opacity = "0";
            easterEggElement.style.pointerEvents = "none";
            setLogoClicks(0);
            setEasterEggActive(false);
          }, 5000);
        }

        return 0;
      }

      // Reset counter after 3 seconds of inactivity
      if (newCount === 1) {
        setTimeout(() => {
          setLogoClicks(0);
        }, 3000);
      }

      return newCount;
    });
  };

  return (
    <div
      className={`min-h-screen flex flex-col md:flex-row transition-colors duration-300 ${
        theme === "dark" ? "bg-gray-900" : "bg-white"
      }`}
    >
      {/* Left side - Enhanced with entrance animation and responsive design */}
      <div
        className={`hidden md:block md:w-1/2 relative overflow-hidden transition-all duration-700 border-0 shadow-xl bg-black
          ${
            mounted
              ? "opacity-100 m-3 rounded-3xl"
              : "opacity-0 m-0 rounded-none"
          }
        `}
      >
        {left}
      </div>

      {/* Right side - Enhanced with theme-aware styling and animations */}
      <div
        className={`flex-1 flex flex-col items-center justify-center p-6 md:py-12 relative transition-colors duration-300
          ${theme === "dark" ? "bg-gray-900 text-white" : "bg-white text-black"}
          ${mounted ? "opacity-100" : "opacity-0"}
        `}
      >
        {/* Header with scroll effect and theme-aware styling */}
        <div
          className={`absolute top-0 left-0 right-0 flex justify-between items-center px-6 py-3 transition-all duration-300
            ${
              scrolled
                ? "bg-white/80 dark:bg-gray-900/80 backdrop-blur-md shadow-sm"
                : "bg-transparent"
            }
          `}
        >
          {/* Back button */}
          <Button
            variant="ghost"
            className={`p-2 rounded-full transition-colors ${
              theme === "dark"
                ? "hover:bg-gray-700 text-gray-300"
                : "hover:bg-gray-100 text-gray-600"
            } group`}
            onClick={() => navigate("/")}
            aria-label="Volver"
          >
            <ArrowLeft
              size={20}
              className="group-hover:text-primary transition-colors"
            />
          </Button>

          {/* Theme toggle */}
          <Button
            variant="ghost"
            className={`p-2 rounded-full transition-colors ${
              theme === "dark"
                ? "hover:bg-gray-700 text-gray-300"
                : "hover:bg-gray-100 text-gray-600"
            }`}
            onClick={toggleTheme}
            aria-label={
              theme === "light" ? "Activar modo oscuro" : "Activar modo claro"
            }
          >
            {theme === "light" ? (
              <Moon
                size={20}
                className="transition-transform hover:rotate-45 duration-300"
              />
            ) : (
              <Sun
                size={20}
                className="transition-transform hover:rotate-45 duration-300"
              />
            )}
          </Button>
        </div>

        {/* Logo and title with enhanced animations and interactions */}
        <header
          className={`w-full max-w-md mb-6 lg:mb-10 mt-12 md:mt-0 transition-transform duration-500 ${
            mounted ? "translate-y-0 opacity-100" : "translate-y-8 opacity-0"
          }`}
        >
          <div className="flex flex-col items-center justify-center">
            {/* Interactive logo with Easter egg */}
            <div
              className="mb-4 relative cursor-pointer transform transition-transform duration-300 hover:scale-105"
              onClick={handleLogoClick}
            >
              <svg
                width="52"
                height="52"
                viewBox="0 0 52 52"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
                className={`transition-transform duration-500 ${
                  easterEggActive ? "animate-bounce" : ""
                }`}
              >
                {/* Background Shape */}
                <rect
                  x="1"
                  y="1"
                  width="50"
                  height="50"
                  rx="14"
                  fill={theme === "dark" ? "#1F2937" : "white"}
                />

                {/* Subtle Gradient Background */}
                <rect
                  x="1"
                  y="1"
                  width="50"
                  height="50"
                  rx="14"
                  fill="url(#paint0_linear)"
                  fillOpacity="0.6"
                />

                {/* Letter B - Main */}
                <path
                  d="M16 14H27C29.7614 14 32 16.2386 32 19C32 21.7614 29.7614 24 27 24H16V14Z"
                  fill="#9b87f5"
                  className="transition-all duration-300"
                />
                <path
                  d="M16 24H30C32.7614 24 35 26.2386 35 29C35 31.7614 32.7614 34 30 34H16V24Z"
                  fill="#7E69AB"
                  className="transition-all duration-300"
                />

                {/* Letter F - Overlapping */}
                <path
                  d="M26 14V38"
                  stroke="#1EAEDB"
                  strokeWidth="4"
                  strokeLinecap="round"
                  className="transition-all duration-300"
                />
                <path
                  d="M26 22H37"
                  stroke="#33C3F0"
                  strokeWidth="4"
                  strokeLinecap="round"
                  className="transition-all duration-300"
                />
                <path
                  d="M26 30H34"
                  stroke="#33C3F0"
                  strokeWidth="4"
                  strokeLinecap="round"
                  className="transition-all duration-300"
                />

                {/* Border */}
                <rect
                  x="1"
                  y="1"
                  width="50"
                  height="50"
                  rx="14"
                  stroke={theme === "dark" ? "#4B5563" : "#D6BCFA"}
                  strokeWidth="1"
                  className="transition-colors duration-300"
                />

                {/* Define gradient */}
                <defs>
                  <linearGradient
                    id="paint0_linear"
                    x1="1"
                    y1="1"
                    x2="51"
                    y2="51"
                    gradientUnits="userSpaceOnUse"
                  >
                    <stop stopColor="#9b87f5" stopOpacity="0.2" />
                    <stop offset="1" stopColor="#6E59A5" stopOpacity="0.1" />
                  </linearGradient>
                </defs>
              </svg>

              {/* Enhanced animated dot accent */}
              <div
                className={`absolute -top-1 -right-1 w-3 h-3 rounded-full ${
                  theme === "dark" ? "bg-white" : "bg-primary"
                } ${easterEggActive ? "animate-ping" : "animate-pulse"}
                `}
              />
            </div>

            {/* Enhanced text treatment with theme awareness and animations */}
            <h1
              className={`text-3xl font-bold transition-colors duration-300 ${
                theme === "dark" ? "text-white" : "text-black"
              }`}
            >
              <span
                className={`relative inline-block ${
                  theme === "dark" ? "text-purple-400" : "text-primary"
                }`}
                style={{
                  textShadow:
                    theme === "dark"
                      ? "0 0 20px rgba(192, 132, 252, 0.6), 0 0 30px rgba(155, 135, 245, 0.4)"
                      : "none",
                }}
              >
                Book
                {/* Subtle hover effect */}
                <span
                  className={`absolute inset-0 ${
                    theme === "dark" ? "bg-purple-500/20" : "bg-primary/10"
                  } opacity-0 group-hover:opacity-100 transition-opacity duration-300 rounded-sm`}
                ></span>
              </span>
              <span className="relative">
                Fast
                <span
                  className={`absolute -bottom-1 left-0 w-full h-1 rounded-full opacity-50 transition-all duration-300 transform origin-left
                  ${theme === "dark" ? "bg-blue-500" : "bg-blue-400"}
                  group-hover:scale-x-110
                `}
                ></span>
              </span>
            </h1>
            <p
              className={`text-sm font-medium mt-1 transition-colors duration-300 ${
                theme === "dark" ? "text-gray-300" : "text-gray-600"
              }`}
            >
              {subtitle || "Tu destino para hospedajes exclusivos"}
            </p>
          </div>
        </header>

        {/* Main content with staggered entrance animation */}
        <div
          className={`w-full max-w-md flex items-center justify-center transition-all duration-700 delay-100 ${
            mounted ? "opacity-100 translate-y-0" : "opacity-0 translate-y-8"
          }`}
        >
          <div className="w-full">{right}</div>
        </div>

        {/* Footer with enhanced theme aware styling and animation */}
        <footer
          className={`mt-6 text-center text-sm w-full max-w-md transition-all duration-700 delay-200 ${
            theme === "dark" ? "text-gray-400" : "text-gray-500"
          } ${mounted ? "opacity-100" : "opacity-0"}`}
        >
          <p className="group">
            © 2025 BookFast. Todos los derechos reservados.
            <span className="block mx-auto w-0 group-hover:w-48 h-px bg-primary/30 mt-1 transition-all duration-500"></span>
          </p>
        </footer>
      </div>

      {/* Easter egg banner - hidden until activated */}
      <div
        id="easter-egg-banner"
        className="fixed bottom-4 left-1/2 transform -translate-x-1/2 z-50 opacity-0 pointer-events-none transition-opacity duration-500"
      >
        <div className="bg-primary text-white px-4 py-2 rounded-lg shadow-lg text-center animate-bounce">
          ¡Sorpresa! Usa el código <strong>BOOKFAST10</strong> para 10% extra
        </div>
      </div>
    </div>
  );
};

export default SplitLayout;
