using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Wheel
    {
        private string m_ManufacturerName;
        private string m_CurrentAirPressure;
        private readonly float r_MaximumAirPressure;

        public Wheel(float i_MaximunAirPressure)
        {
            r_MaximumAirPressure = i_MaximunAirPressure;
        }



        public string ManufacturerName
        {
            get => m_ManufacturerName;
            set => m_ManufacturerName = value;
        }


        public string CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            set => m_CurrentAirPressure = value;
        }

        public float MaximumAirPressure
        {
            get => r_MaximumAirPressure;
        }


    }
}
