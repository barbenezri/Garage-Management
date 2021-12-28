namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public float CapacityOfEnergyLeft
        {
            get => m_CurrectCapacity;
        }

        public float MaximumEnergyCapacity
        {
            get => m_MaximumCapacity;
        }

        public void ChargeBatteryOfEngine(float i_HoursToAddToBattery)
        {
            if (FillVehicleEnergy(i_HoursToAddToBattery) == false)
            {
                float battaryLeftover = MaximumEnergyCapacity - CapacityOfEnergyLeft;
                string message = "The charging time of battery in engine is out of range";

                throw new ValueOutOfRangeException(battaryLeftover, 0, message);
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Engine type : Electric
Precent of hours of battery left : {0}%
Maximum of hours of battery is : {1} 
Current hours that left in the battery : {2}",
m_PercentOfEnergyLeft,
m_MaximumCapacity,
CapacityOfEnergyLeft);
        }
    }
}
