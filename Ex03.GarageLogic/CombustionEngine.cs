using System;

namespace Ex03.GarageLogic
{
    public class CombustionEngine : Engine
    {
        private readonly eFuelKind r_KindOfFuel;

        public enum eFuelKind
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }

        public CombustionEngine(eFuelKind i_FuelKind)
        {
            if (Enum.IsDefined(typeof(eFuelKind), i_FuelKind) == true)
            {
                r_KindOfFuel = i_FuelKind;
            }
        }

        public eFuelKind KindOfFuels
        {
            get => r_KindOfFuel;
        }

        public float EnergyLeft
        {
            get => m_EnergyLeft;
        }

        public float MaximumCapacity
        {
            get => m_MaximumEnergyCapacity;
        }

        public void Refueling(float i_AmountOfFuelToAdd, eFuelKind i_KindOfFuel)
        {
            if ((Enum.IsDefined(typeof(eFuelKind), i_KindOfFuel) == true) && (r_KindOfFuel == i_KindOfFuel))
            {
                if (FillVehicleEnergy(i_AmountOfFuelToAdd) == false)
                {
                    float missingFuelForFullTank = MaximumCapacity - EnergyLeft;
                    const string message = "Invalid amount of fuel to refuel";

                    throw new ValueOutOfRangeException(missingFuelForFullTank, 0, message);
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
Percent of litters of fuel tank left : {1}%
Maximum of litters of fuel tank is : {2} 
Remaining litters of fuel : {3}",
Enum.GetName(typeof(eFuelKind), KindOfFuels),
m_PercentOfEnergyLeft,
m_MaximumEnergyCapacity,
EnergyLeft);
        }
    }
}
