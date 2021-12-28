using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Motocycle : Vehicle
    {
        private int m_EngineCapacity;
        private eLicenseType m_LicenseType;
        private const float k_MaxWheelAirPressure = 30;
        public enum eLicenseType
        {
            A = 1,
            A2,
            AA,
            B,
        }

        public Motocycle()
        {
            m_NumberOfWheels = 2;
            InitialWheelsForFirstTime(k_MaxWheelAirPressure);
        }

        public override void SetVehicleUniqueInformation(List<string> i_ListOfUniqueInformation)
        {
            setUniqueFirstInormation(i_ListOfUniqueInformation[0]);
            setUniqueSecondInormation(i_ListOfUniqueInformation[1]);
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type : Motorcycle
License plate : {0}
Model name : {1}
License type : {2}
Engine volume : {3} 
-----Engine details-----
{4}
-----Wheels details-----
{5}",
m_licensePlate,
m_VehicleModel,
Enum.GetName(typeof(eLicenseType), m_LicenseType),
m_EngineCapacity,
m_Engine.ToString(),
GetWheelInformationOfVehicle());
        }

        public override string GetSpecialInfoMessage(out int o_AmountOfUniqueInformation)
        {
            o_AmountOfUniqueInformation = 2;

            return string.Format(
@"Please enter the motocycle license type <A,A2,AA,B>.
Then please enter the engine capacity of the motocycle,should be a number bigget than 0.
Notice: the system is case sensetive");
        }

        public override void InsertEngineInformation(float i_CurrentEngineCapcityLeft)
        {
            float engineMaxCapacity = MaxEngineCapacity();

            SetEnergyEngineCapacityLeft(i_CurrentEngineCapcityLeft, engineMaxCapacity);
        }

        protected override float MaxEngineCapacity()
        {
            float engineMaxCapacity;


            if (m_Engine is CombustionEngine)
            {
                engineMaxCapacity = 5.8f;
            }
            else
            { /*if is not fuel its electric engine*/
                engineMaxCapacity = 2.3f;
            }

            return engineMaxCapacity;
        }

        private void setUniqueFirstInormation(string i_FirstUniqueInformation)
        {
            bool isInsideEnum = Enum.IsDefined(typeof(eLicenseType), i_FirstUniqueInformation);

            if (isInsideEnum == true)
            {
                m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_FirstUniqueInformation);
            }
            else
            {
                throw new ArgumentException("You try to set a license type that doesnt exsist");
            }
        }

        private void setUniqueSecondInormation(string i_SecondUniqueInformation)
        {
            int engineCapacity;
            bool isParseWork = int.TryParse((string)i_SecondUniqueInformation, out engineCapacity);

            if (isParseWork == true)
            {
                if (engineCapacity > 0)
                {
                    m_EngineCapacity = engineCapacity;
                }
                else
                {
                    throw new ArgumentException("You try to set not a positive engine capacity");
                }
            }
            else
            {
                throw new FormatException("You try to set an engine capacity with not valiad value");
            }
        }
    }
}
