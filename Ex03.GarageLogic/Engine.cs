namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_MaximumEnergyCapacity;
        protected float m_EnergyLeft;
        protected float m_PercentOfEnergyLeft;

        public void SetEngineCapacity(float i_MaxEngineCapacity, float i_CurrentEngineCapacityLeft)
        {
            if ((i_CurrentEngineCapacityLeft < 0) || (i_CurrentEngineCapacityLeft > i_MaxEngineCapacity))
            {
                const string message = "The desired energy engine capacity is higher than max engine capacity.";

                throw new ValueOutOfRangeException(i_MaxEngineCapacity, 0, message);
            }
            else
            {
                m_MaximumEnergyCapacity = i_MaxEngineCapacity;
                m_EnergyLeft = i_CurrentEngineCapacityLeft;
                SetPercentOfEnergyLeft();
            }
        }

        protected bool FillVehicleEnergy(float i_EnergyToFill)
        {
            float capacityOfBattery = m_EnergyLeft + i_EnergyToFill;
            bool isFilledEnergy = false;

            if (capacityOfBattery <= m_MaximumEnergyCapacity)
            {
                m_EnergyLeft = capacityOfBattery;
                SetPercentOfEnergyLeft();
                isFilledEnergy = true;
            }

            return isFilledEnergy;
        }

        protected void SetPercentOfEnergyLeft()
        {
            m_PercentOfEnergyLeft = (m_EnergyLeft / m_MaximumEnergyCapacity) * 100;
        }
    }
}
