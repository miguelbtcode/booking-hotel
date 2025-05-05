import { Card, CardContent, CardFooter } from "@/components/ui/molecules";
import { Button } from "@/components/ui/atoms";
import { BedDouble } from "lucide-react";

interface RoomCardProps {
  id: string;
  name: string;
  price: number;
  image: string;
  capacity: number;
}

export const RoomCard = ({
  id,
  name,
  price,
  image,
  capacity,
}: RoomCardProps) => {
  return (
    <Card className="overflow-hidden hover:shadow-lg transition-shadow">
      <div className="relative h-48 overflow-hidden">
        <img
          src={image}
          alt={name}
          className="w-full h-full object-cover hover:scale-105 transition-transform duration-300"
        />
      </div>
      <CardContent className="p-4">
        <h3 className="text-lg font-semibold mb-2">{name}</h3>
        <div className="flex items-center gap-2 text-gray-600 mb-2">
          <BedDouble size={18} />
          <span>Up to {capacity} guests</span>
        </div>
      </CardContent>
      <CardFooter className="p-4 pt-0 flex items-center justify-between">
        <span className="text-xl font-semibold">${price}/night</span>
        <Button onClick={() => (window.location.href = `/hotel/${id}`)}>
          Book Now
        </Button>
      </CardFooter>
    </Card>
  );
};
