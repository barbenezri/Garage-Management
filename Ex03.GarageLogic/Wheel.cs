namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get => r_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get => m_ManufacturerName;
            set => m_ManufacturerName = value;
        }

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            private set => m_CurrentAirPressure = value;
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
                const string message = "Too much air can cause the wheel explosion. Go buy a bigger wheels or try to inflate less.";

                throw new ValueOutOfRangeException(r_MaxAirPressure, 0, message);
            }
        }

        public void InflatingWheelToMax()
        {
            float missingAirPressureForMax = r_MaxAirPressure - m_CurrentAirPressure;

            InflatingWheel(missingAirPressureForMax);
        }

        public void SetInformationOfWheel(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            if ((i_CurrentAirPressure < 0) || (i_CurrentAirPressure > r_MaxAirPressure))
            {
                const string message = "The current wheel air pressure is higher than maximum wheel air pressure";

                throw new ValueOutOfRangeException(r_MaxAirPressure, 0, message);
            }
            else
            {
                m_ManufacturerName = i_ManufacturerName;
                m_CurrentAirPressure = i_CurrentAirPressure;
            }
        }
    }
}