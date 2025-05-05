import { Header, RoomCard } from "@/components/ui/organisms";
import { Button } from "@/components/ui/atoms/";

const featuredRooms = [
  {
    id: "1",
    name: "Deluxe Ocean View",
    price: 299,
    image: "https://images.unsplash.com/photo-1721322800607-8c38375eef04",
    capacity: 2,
  },
  {
    id: "2",
    name: "Premium Suite",
    price: 499,
    image: "https://images.unsplash.com/photo-1488590528505-98d2b5aba04b",
    capacity: 4,
  },
  {
    id: "3",
    name: "Family Room",
    price: 399,
    image: "https://images.unsplash.com/photo-1649972904349-6e44c42644a7",
    capacity: 6,
  },
];

const Footer = () => {
  return (
    <footer className="bg-gray-900 text-white py-12">
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div>
            <h3 className="text-xl font-semibold mb-4">LuxStay</h3>
            <p className="text-gray-400">
              Experience luxury and comfort in our premium hotel accommodations.
            </p>
          </div>
          <div>
            <h3 className="text-xl font-semibold mb-4">Quick Links</h3>
            <ul className="space-y-2">
              <li>
                <a
                  href="/rooms"
                  className="text-gray-400 hover:text-white transition-colors"
                >
                  Rooms
                </a>
              </li>
              <li>
                <a
                  href="/about"
                  className="text-gray-400 hover:text-white transition-colors"
                >
                  About
                </a>
              </li>
              <li>
                <a
                  href="/contact"
                  className="text-gray-400 hover:text-white transition-colors"
                >
                  Contact
                </a>
              </li>
            </ul>
          </div>
          <div>
            <h3 className="text-xl font-semibold mb-4">Contact Us</h3>
            <p className="text-gray-400">
              123 Luxury Avenue
              <br />
              New York, NY 10001
              <br />
              Tel: (555) 123-4567
            </p>
          </div>
        </div>
      </div>
    </footer>
  );
};

const Home = () => {
  return (
    <div className="max-w-full bg-gray-50">
      <Header />

      {/* <div className="min-h-screen bg-gray-50"> */}
      {/* Hero Section */}
      <section className="pt-16 lg:pt-24 bg-linear-to-br from-purple-100 to-indigo-50">
        <div className="container mx-auto px-4">
          <div className="flex flex-col items-center text-center py-20 lg:py-32">
            <h1 className="text-4xl lg:text-6xl font-bold text-gray-900 mb-6">
              Experience Luxury & Comfort
            </h1>
            <p className="text-xl text-gray-600 mb-8 max-w-2xl">
              Discover our handpicked collection of luxurious rooms and suites,
              perfect for your next memorable stay.
            </p>
            <Button
              size="lg"
              className="text-lg px-8"
              onClick={() => (window.location.href = "/rooms")}
            >
              Explore Rooms
            </Button>
          </div>
        </div>
      </section>

      {/* Featured Rooms */}
      <section className="py-16 bg-white">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl font-bold text-center mb-12">
            Featured Rooms
          </h2>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {featuredRooms.map((room) => (
              <RoomCard key={room.id} {...room} />
            ))}
          </div>
          <div className="text-center mt-12">
            <Button
              variant="outline"
              size="lg"
              onClick={() => (window.location.href = "/rooms")}
            >
              View All Rooms
            </Button>
          </div>
        </div>
      </section>

      {/* Why Choose Us */}
      <section className="py-16 bg-gray-50">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl font-bold text-center mb-12">
            Why Choose Us
          </h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8 max-w-4xl mx-auto">
            {[
              {
                title: "Prime Location",
                description:
                  "Located in the heart of the city with easy access to attractions",
              },
              {
                title: "Luxury Amenities",
                description:
                  "Enjoy our world-class facilities and premium services",
              },
              {
                title: "24/7 Service",
                description: "Our dedicated staff is always here to assist you",
              },
            ].map((feature, index) => (
              <div key={index} className="text-center p-6">
                <h3 className="text-xl font-semibold mb-3">{feature.title}</h3>
                <p className="text-gray-600">{feature.description}</p>
              </div>
            ))}
          </div>
        </div>
      </section>
      {/* </div> */}

      {/* Footer */}
      <Footer />
    </div>
  );
};

export default Home;
