namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public float MaxAirPressure
        {
            get => r_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get => m_ManufacturerName;
            private set => m_ManufacturerName = value;
        }

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            private set => m_CurrentAirPressure = value;
        }

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflatingWheel(float i_AirPressureToAdd)
        {
            float newAirPressure = CurrentAirPressure + i_AirPressureToAdd;
            if (newAirPressure <= r_MaxAirPressure)
            {
                CurrentAirPressure = newAirPressure;
            }
            else
            {
                string message = "The air to add to the wheel is overpass the max capacity of the air pressure wheel";

                throw new ValueOutOfRangeException(r_MaxAirPressure, 0, message);
            }
        }

        public void InflatingWheelToMax()
        {
            float airPressureNeededToFillToGetMaxAirPressure = r_MaxAirPressure - m_CurrentAirPressure;

            InflatingWheel(airPressureNeededToFillToGetMaxAirPressure);
        }

        public void SetInformationOfWheel(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            if ((i_CurrentAirPressure < 0) || (i_CurrentAirPressure > r_MaxAirPressure))
            {
                string message = "The current wheel air pressure is biger than max wheel air pressure";

                throw new ValueOutOfRangeException(r_MaxAirPressure, 0, message);
            }

            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }
    }
}