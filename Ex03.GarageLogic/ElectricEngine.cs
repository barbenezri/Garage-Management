namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public float EnergyLeft
        {
            get => m_EnergyLeft;
        }

        public float MaximumEnergyCapacity
        {
            get => m_MaximumEnergyCapacity;
        }

        public void ChargeEngineBattery(float i_HoursToAddToBattery)
        {
            if (FillVehicleEnergy(i_HoursToAddToBattery) == false)
            {
                float batteryLeftover = MaximumEnergyCapacity - EnergyLeft;
                const string message = "Desired charging time is out of range.";

                throw new ValueOutOfRangeException(batteryLeftover, 0, message);
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Engine type : Electric
Percent of hours of battery left : {0}%
Maximum of hours of battery is : {1} 
Current hours that left in the battery : {2}",
m_PercentOfEnergyLeft,
m_MaximumEnergyCapacity,
EnergyLeft);
        }
    }
}
