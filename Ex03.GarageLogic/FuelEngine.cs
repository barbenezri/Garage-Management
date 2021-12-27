using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eKindOfFuel r_KindOfFuel;

        public FuelEngine(eKindOfFuel i_KindOfFuel)
        {
            if (Enum.IsDefined(typeof(eKindOfFuel), i_KindOfFuel) == true)
            {
                r_KindOfFuel = i_KindOfFuel;
            }
        }

        public enum eKindOfFuel
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }

        public eKindOfFuel KindOfFuels
        {
            get => r_KindOfFuel;
        }

        public float CapacityOfEnergyLeft
        {
            get => m_CurrectCapacityOfEnergyLeft;
        }

        public float MaximumCapacityOfEnergy
        {
            get => m_MaximumCapacityOfEnergy;
        }

        public void Refueling(float i_AmountOfFuelToAdd, eKindOfFuel i_KindOfFuel)
        {
            if ((Enum.IsDefined(typeof(eKindOfFuel), i_KindOfFuel) == true) && (r_KindOfFuel == i_KindOfFuel))
            {
                if (FillingEnergyOfVehicle(i_AmountOfFuelToAdd) == false)
                {
                    float maximumCapacityThatCanFill = MaximumCapacityOfEnergy - CapacityOfEnergyLeft;
                    string message = "The ammout of fuel to add isn't in range";

                    throw new ValueOutOfRangeException(maximumCapacityThatCanFill, 0, message);
                }
            }
            else
            {
                throw new ArgumentException("The fuel type is incorrect");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Engine type : fuel
Fuel type : {0}
Precent of litters of fuel tank left : {1}%
Maximum of litters of fuel tank is : {2} 
Remaining litters of fuel : {3}",
Enum.GetName(typeof(eKindOfFuel), KindOfFuels),
m_PercentOfEnergyLeftOfTheVehicle,
m_MaximumCapacityOfEnergy,
CapacityOfEnergyLeft);
        }
    }
}
