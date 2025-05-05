import { useState, useEffect } from "react";
import { Avatar } from "@/components/ui/atoms";
import { Loader2 } from "lucide-react";

// Hotel images for the carousel
const carouselImages = [
  {
    url: "https://images.unsplash.com/photo-1566073771259-6a8506099945?q=80&w=1920&auto=format&fit=crop",
    alt: "Luxury hotel room with ocean view",
    testimonial: {
      text: "Encontré el hotel perfecto para mi luna de miel en menos de 5 minutos.",
      author: "María López",
      avatar: "https://i.pravatar.cc/150?img=32",
    },
  },
  {
    url: "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?q=80&w=1920&auto=format&fit=crop",
    alt: "Modern hotel lobby with elegant design",
    testimonial: {
      text: "Los mejores precios y promociones exclusivas para mis viajes de negocios.",
      author: "Carlos Méndez",
      avatar: "https://i.pravatar.cc/150?img=53",
    },
  },
  {
    url: "https://images.unsplash.com/photo-1540541338287-41700207dee6?q=80&w=1920&auto=format&fit=crop",
    alt: "Infinity pool with sunset view",
    testimonial: {
      text: "BookFast me ayudó a encontrar los mejores hoteles boutique fuera de lo común.",
      author: "Lucía Fernández",
      avatar: "https://i.pravatar.cc/150?img=23",
    },
  },
];

export const LoginCarousel = () => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [loading, setLoading] = useState(true);
  const [imagesLoaded, setImagesLoaded] = useState(
    new Array(carouselImages.length).fill(false)
  );

  useEffect(() => {
    // Preload all images
    carouselImages.forEach((image, index) => {
      const img = new Image();
      img.src = image.url;
      img.onload = () => {
        setImagesLoaded((prev) => {
          const newState = [...prev];
          newState[index] = true;
          return newState;
        });
      };
    });
  }, []);

  useEffect(() => {
    // Check if all images are loaded
    if (imagesLoaded.every((loaded) => loaded)) {
      setLoading(false);
    }
  }, [imagesLoaded]);

  useEffect(() => {
    // Change image every
    const interval = setInterval(() => {
      setCurrentIndex((prevIndex) => (prevIndex + 1) % carouselImages.length);
    }, 7000);

    return () => clearInterval(interval);
  }, []);

  return (
    <div className="absolute inset-0 w-full h-full overflow-hidden">
      {/* BookFast logo in top left */}
      <div className="absolute top-8 left-8 z-20 flex items-center space-x-3">
        <div className="h-12 w-12 rounded-xl bg-white/90 backdrop-blur-sm shadow-lg flex items-center justify-center p-2">
          <svg
            width="32"
            height="32"
            viewBox="0 0 52 52"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            {/* Background Shape */}
            <rect x="1" y="1" width="50" height="50" rx="14" fill="white" />

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
            />
            <path
              d="M16 24H30C32.7614 24 35 26.2386 35 29C35 31.7614 32.7614 34 30 34H16V24Z"
              fill="#7E69AB"
            />

            {/* Letter F - Overlapping */}
            <path
              d="M26 14V38"
              stroke="#1EAEDB"
              strokeWidth="4"
              strokeLinecap="round"
            />
            <path
              d="M26 22H37"
              stroke="#33C3F0"
              strokeWidth="4"
              strokeLinecap="round"
            />
            <path
              d="M26 30H34"
              stroke="#33C3F0"
              strokeWidth="4"
              strokeLinecap="round"
            />

            {/* Border */}
            <rect
              x="1"
              y="1"
              width="50"
              height="50"
              rx="14"
              stroke="#D6BCFA"
              strokeWidth="1"
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
        </div>
        <h2 className="text-white text-3xl font-bold drop-shadow-md">
          <span className="text-primary-foreground">Book</span>
          <span className="relative">
            Fast
            <span className="absolute -bottom-1 left-0 w-full h-1 bg-blue-400 rounded-full opacity-80"></span>
          </span>
        </h2>
      </div>

      {loading && (
        <div className="absolute inset-0 flex items-center justify-center bg-black">
          <div className="flex flex-col items-center">
            <Loader2 className="h-12 w-12 animate-spin text-primary" />
            <p className="mt-4 text-white font-medium">
              Cargando experiencias...
            </p>
          </div>
        </div>
      )}

      {/* Reduced opacity dark overlay for better image visibility */}
      <div className="absolute inset-0 bg-black/25 z-10"></div>

      {carouselImages.map((image, index) => (
        <div
          key={index}
          className={`absolute inset-0 transition-opacity duration-1000 ${
            currentIndex === index ? "opacity-100" : "opacity-0"
          }`}
          style={{ zIndex: currentIndex === index ? 1 : 0 }}
        >
          <img
            src={image.url}
            alt={image.alt}
            className="object-cover w-full h-full"
          />

          {/* Call to action text */}
          <div className="absolute top-1/4 left-1/2 transform -translate-x-1/2 z-20 text-center w-full max-w-xl px-6">
            <h1 className="text-5xl font-bold text-white mb-6 leading-tight drop-shadow-lg">
              Descubre hospedajes{" "}
              <span className="text-primary-foreground underline decoration-2 underline-offset-4">
                excepcionales
              </span>
            </h1>
            <p className="text-white/90 text-xl font-medium drop-shadow-md">
              Hoteles exclusivos y experiencias únicas al mejor precio
            </p>
          </div>

          {/* Enhanced testimonial card with better visibility */}
          <div className="absolute bottom-24 left-0 right-0 mx-auto z-20 max-w-md w-full px-6">
            <div className="bg-gradient-to-br from-white/50 to-white/30 backdrop-blur-md p-6 rounded-2xl shadow-xl border border-white/40">
              <div className="flex justify-center mb-4">
                <div className="bg-primary/20 p-2 rounded-full">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="28"
                    height="28"
                    viewBox="0 0 24 24"
                    fill="none"
                    stroke="currentColor"
                    strokeWidth="2"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    className="text-white"
                  >
                    <path d="M3 21c3 0 7-1 7-8V5c0-1.25-.756-2.017-2-2H4c-1.25 0-2 .75-2 1.972V11c0 1.25.75 2 2 2 1 0 1 0 1 1v1c0 1-1 2-2 2s-1 .008-1 1.031V20c0 1 0 1 1 1z"></path>
                    <path d="M15 21c3 0 7-1 7-8V5c0-1.25-.757-2.017-2-2h-4c-1.25 0-2 .75-2 1.972V11c0 1.25.75 2 2 2h.75c0 2.25.25 4-2.75 4v3c0 1 0 1 1 1z"></path>
                  </svg>
                </div>
              </div>
              <p className="text-white text-xl font-medium text-center mb-6 drop-shadow">
                "{image.testimonial.text}"
              </p>
              <div className="flex items-center justify-center">
                <Avatar className="h-12 w-12 mr-3 border-2 border-white ring-2 ring-white/30">
                  <img
                    src={image.testimonial.avatar}
                    alt={image.testimonial.author}
                  />
                </Avatar>
                <span className="text-white font-semibold text-lg drop-shadow-sm">
                  {image.testimonial.author}
                </span>
              </div>
            </div>
          </div>
        </div>
      ))}

      {/* Carousel dots */}
      <div className="absolute bottom-6 left-0 right-0 z-20 flex justify-center">
        {carouselImages.map((_, index) => (
          <button
            key={index}
            className={`w-2 h-2 mx-1.5 rounded-full transition-all ${
              currentIndex === index
                ? "bg-white w-8"
                : "bg-white/70 hover:bg-white/90"
            }`}
            onClick={() => setCurrentIndex(index)}
            aria-label={`Go to slide ${index + 1}`}
          />
        ))}
      </div>
    </div>
  );
};
