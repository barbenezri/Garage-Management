namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_MaximumCapacityOfEnergy;
        protected float m_CurrectCapacityOfEnergyLeft;
        protected float m_PercentOfEnergyLeftOfTheVehicle;

        public void SetEnergyLeftAndMaxInEngineCapacity(float i_MaxEngineCapacity, float i_CurrentEngineCapcityLeft)
        {
            if ((i_CurrentEngineCapcityLeft < 0) || (i_CurrentEngineCapcityLeft > i_MaxEngineCapacity))
            {
                string message = "The current energy enegine capacity is biger than max enegine capacity";

                throw new ValueOutOfRangeException(i_MaxEngineCapacity, 0, message);
            }

            m_MaximumCapacityOfEnergy = i_MaxEngineCapacity;
            m_CurrectCapacityOfEnergyLeft = i_CurrentEngineCapcityLeft;
            SetPrecentOfEnergyLeft();
        }

        protected bool FillingEnergyOfVehicle(float i_EnergyToFill)
        {
            float capacityOfBattery = this.m_CurrectCapacityOfEnergyLeft + i_EnergyToFill;
            bool isFilledEnergy = false;

            if (capacityOfBattery <= this.m_MaximumCapacityOfEnergy)
            {
                m_CurrectCapacityOfEnergyLeft = capacityOfBattery;
                SetPrecentOfEnergyLeft();
                isFilledEnergy = true;
            }

            return isFilledEnergy;
        }

        protected void SetPrecentOfEnergyLeft()
        {
            m_PercentOfEnergyLeftOfTheVehicle = (m_CurrectCapacityOfEnergyLeft / m_MaximumCapacityOfEnergy) * 100;
        }
    }
}
