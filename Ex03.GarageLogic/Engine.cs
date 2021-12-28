namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_MaximumCapacity;
        protected float m_CurrectCapacity;
        protected float m_PercentOfEnergyLeft;

        public void SetEnergyLeftAndMaxInEngineCapacity(float i_MaxEngineCapacity, float i_CurrentEngineCapcityLeft)
        {
            if ((i_CurrentEngineCapcityLeft < 0) || (i_CurrentEngineCapcityLeft > i_MaxEngineCapacity))
            {
                string message = "The current energy enegine capacity is biger than max enegine capacity";

                throw new ValueOutOfRangeException(i_MaxEngineCapacity, 0, message);
            }
            else
            {
                m_MaximumCapacity = i_MaxEngineCapacity;
                m_CurrectCapacity = i_CurrentEngineCapcityLeft;
                SetPrecentOfEnergyLeft();
            }
        }

        protected bool FillingEnergyOfVehicle(float i_EnergyToFill)
        {
            float capacityOfBattery = m_CurrectCapacity + i_EnergyToFill;
            bool isFilledEnergy = false;

            if (capacityOfBattery <= m_MaximumCapacity)
            {
                m_CurrectCapacity = capacityOfBattery;
                SetPrecentOfEnergyLeft();
                isFilledEnergy = true;
            }

            return isFilledEnergy;
        }

        protected void SetPrecentOfEnergyLeft()
        {
            m_PercentOfEnergyLeft = (m_CurrectCapacity / m_MaximumCapacity) * 100;
        }
    }
}
