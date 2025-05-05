import { CheckCircle, ShieldCheck, Clock, CreditCard } from "lucide-react";

const AuthBenefits = () => {
  const benefits = [
    {
      icon: <ShieldCheck size={24} className="text-black" />,
      title: "Reservas seguras",
      description: "Protección en cada reserva"
    },
    {
      icon: <CheckCircle size={24} className="text-black" />,
      title: "Alojamientos verificados",
      description: "Calidad garantizada"
    },
    {
      icon: <Clock size={24} className="text-black" />,
      title: "Soporte 24/7",
      description: "Asistencia en cualquier momento"
    },
    {
      icon: <CreditCard size={24} className="text-black" />,
      title: "Pagos protegidos",
      description: "Transacciones sin preocupaciones"
    }
  ];

  return (
    <div className="mt-12 mb-4">
      <h3 className="text-black font-semibold mb-6 text-center">
        ¿Por qué elegir BookFast?
      </h3>
      <div className="grid grid-cols-2 gap-4">
        {benefits.map((benefit, index) => (
          <div 
            key={index} 
            className="bg-white rounded-xl p-4 border border-gray-100 shadow-md hover:shadow-lg transition-all hover:translate-y-[-2px]"
          >
            <div className="flex items-start gap-3">
              <div className="bg-gray-50 p-2 rounded-lg">
                {benefit.icon}
              </div>
              <div>
                <h4 className="font-medium text-gray-900 text-sm">
                  {benefit.title}
                </h4>
                <p className="text-xs text-gray-500 mt-1">
                  {benefit.description}
                </p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default AuthBenefits;