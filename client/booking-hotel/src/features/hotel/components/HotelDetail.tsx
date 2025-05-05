import {
  Card,
  Calendar,
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/molecules";
import { Button } from "@/components/ui/atoms";
import { useState } from "react";
import { Wifi, BedDouble, MapPin, Star, Users } from "lucide-react";

const HotelDetail = () => {
  const [date, setDate] = useState<Date | undefined>(new Date());

  const hotelImages = [
    "https://images.unsplash.com/photo-1566073771259-6a8506099945",
    "https://images.unsplash.com/photo-1582719508461-905c673771fd",
    "https://images.unsplash.com/photo-1571896349842-33c89424de2d",
  ];

  const amenities = [
    { icon: <Wifi className="h-6 w-6" />, name: "WiFi Gratis" },
    { icon: <BedDouble className="h-6 w-6" />, name: "King Size Bed" },
    { icon: <Users className="h-6 w-6" />, name: "Hasta 4 personas" },
    { icon: <MapPin className="h-6 w-6" />, name: "Vista al mar" },
  ];

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Carousel de imágenes */}
          <div className="lg:col-span-2">
            <Carousel className="w-full">
              <CarouselContent>
                {hotelImages.map((image, index) => (
                  <CarouselItem key={index}>
                    <div className="aspect-video rounded-xl overflow-hidden">
                      <img
                        src={image}
                        alt={`Vista ${index + 1}`}
                        className="w-full h-full object-cover"
                      />
                    </div>
                  </CarouselItem>
                ))}
              </CarouselContent>
              <CarouselPrevious />
              <CarouselNext />
            </Carousel>

            {/* Información del hotel */}
            <div className="mt-8 space-y-6">
              <div className="flex items-start justify-between">
                <div>
                  <h1 className="text-3xl font-bold text-gray-900">
                    Luxury Ocean Resort
                  </h1>
                  <div className="flex items-center gap-2 mt-2">
                    <MapPin className="h-5 w-5 text-gray-500" />
                    <span className="text-gray-600">Cancún, México</span>
                    <div className="flex items-center gap-1 ml-4">
                      <Star className="h-5 w-5 text-yellow-400 fill-current" />
                      <span className="font-semibold">4.9</span>
                    </div>
                  </div>
                </div>
                <div className="text-right">
                  <p className="text-3xl font-bold text-primary">$299</p>
                  <p className="text-gray-600">por noche</p>
                </div>
              </div>

              <p className="text-gray-600 leading-relaxed">
                Disfruta de una experiencia única en nuestro resort de lujo con
                vista al mar. Nuestras habitaciones están diseñadas para
                brindarte el máximo confort y una estadía inolvidable. Con
                acceso directo a la playa y servicios premium las 24 horas.
              </p>

              <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                {amenities.map((amenity, index) => (
                  <Card key={index} className="p-4 flex items-center gap-3">
                    <div className="text-primary">{amenity.icon}</div>
                    <span className="text-sm font-medium">{amenity.name}</span>
                  </Card>
                ))}
              </div>
            </div>
          </div>

          {/* Formulario de reserva */}
          <div className="lg:col-span-1">
            <Card className="p-6 sticky top-24">
              <h3 className="text-xl font-semibold mb-4">
                Reserva tu estancia
              </h3>
              <div className="space-y-4">
                <Calendar
                  mode="single"
                  selected={date}
                  onSelect={setDate}
                  className="rounded-md border"
                />
                <Button className="w-full" size="lg">
                  Reservar ahora
                </Button>
                <p className="text-sm text-gray-500 text-center">
                  Sin cargo por cancelación antes de 48 horas
                </p>
              </div>
            </Card>
          </div>
        </div>
      </div>
    </div>
  );
};

export default HotelDetail;
