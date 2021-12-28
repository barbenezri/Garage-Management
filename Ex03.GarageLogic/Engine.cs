namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_MaximumEnergyCapacity;
        protected float m_EnergyLeft;
        protected float m_PercentOfEnergyLeft;

        public void SetEngineCapacity(float i_MaxEngineCapacity, float i_CurrentEngineCapcityLeft)
        {
            if ((i_CurrentEngineCapcityLeft < 0) || (i_CurrentEngineCapcityLeft > i_MaxEngineCapacity))
            {
                string message = "The desired energy engine capacity is higher than max engine capacity.";

                throw new ValueOutOfRangeException(i_MaxEngineCapacity, 0, message);
            }
            else
            {
                m_MaximumEnergyCapacity = i_MaxEngineCapacity;
                m_EnergyLeft = i_CurrentEngineCapcityLeft;
                SetPrecentOfEnergyLeft();
            }
        }

        protected bool FillVehicleEnergy(float i_EnergyToFill)
        {
            float capacityOfBattery = m_EnergyLeft + i_EnergyToFill;
            bool isFilledEnergy = false;

            if (capacityOfBattery <= m_MaximumEnergyCapacity)
            {
                m_EnergyLeft = capacityOfBattery;
                SetPrecentOfEnergyLeft();
                isFilledEnergy = true;
            }

            return isFilledEnergy;
        }

        protected void SetPrecentOfEnergyLeft()
        {
            m_PercentOfEnergyLeft = (m_EnergyLeft / m_MaximumEnergyCapacity) * 100;
        }
    }
}
