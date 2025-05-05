import { Button } from "@/components/ui/atoms";
import { useState } from "react";
import { Menu, BedDouble, User } from "lucide-react";

export const Header = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  return (
    <header className="fixed top-0 left-0 right-0 w-full bg-white/80 backdrop-blur-md z-50 shadow-2xs">
      <div className="w-full max-w-[1400px] mx-auto px-4 md:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <a href="/" className="text-2xl font-semibold text-primary">
            LuxStay
          </a>

          {/* Desktop Navigation */}
          <nav className="hidden md:flex items-center space-x-8">
            <a
              href="/"
              className="text-gray-600 hover:text-primary transition-colors"
            >
              Home
            </a>
            <a
              href="/rooms"
              className="text-gray-600 hover:text-primary transition-colors flex items-center gap-2"
            >
              <BedDouble className="h-4 w-4" />
              Rooms
            </a>
            <Button
              variant="ghost"
              className="flex items-center gap-2"
              onClick={() => (window.location.href = "/login")}
            >
              <User size={18} />
              Sign In
            </Button>
          </nav>

          {/* Mobile Menu Button */}
          <button
            className="md:hidden p-2"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
          >
            <Menu size={24} />
          </button>
        </div>

        {/* Mobile Navigation */}
        {isMenuOpen && (
          <div className="md:hidden py-4 border-t">
            <nav className="flex flex-col space-y-4">
              <a
                href="/"
                className="text-gray-600 hover:text-primary transition-colors px-4"
              >
                Home
              </a>
              <a
                href="/rooms"
                className="text-gray-600 hover:text-primary transition-colors px-4 flex items-center gap-2"
              >
                <BedDouble className="h-4 w-4" />
                Rooms
              </a>
              <a
                href="/signin"
                className="text-gray-600 hover:text-primary transition-colors px-4 flex items-center gap-2"
              >
                <User className="h-4 w-4" />
                Sign In
              </a>
            </nav>
          </div>
        )}
      </div>
    </header>
  );
};
