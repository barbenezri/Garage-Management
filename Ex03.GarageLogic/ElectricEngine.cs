namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void ChargeBatteryOfEngine(float i_HoursToAddToBattery)
        {
            if (FillingEnergyOfVehicle(i_HoursToAddToBattery) == false)
            {
                float maximumOfCapacityToFillBattery = MaximumCapacityOfEnergy - CapacityOfEnergyLeft;
                string message = "The charging time of battery in engine is out of range";

                throw new ValueOutOfRangeException(maximumOfCapacityToFillBattery, 0, message);
            }
        }

        public float CapacityOfEnergyLeft
        {
            get => m_CurrectCapacityOfEnergyLeft;
        }

        public float MaximumCapacityOfEnergy
        {
            get => m_MaximumCapacityOfEnergy;
        }

        public override string ToString()
        {
            return string.Format(
@"Engine type : Electric.
Precent of hours of battery left : {0}
Maximum of hours of battery is : {1} 
Current hours that left in the battery : {2}", m_PercentOfEnergyLeftOfTheVehicle, m_MaximumCapacityOfEnergy, CapacityOfEnergyLeft);
        }
    }
}
