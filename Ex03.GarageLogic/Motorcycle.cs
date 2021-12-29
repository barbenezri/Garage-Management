using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private const float k_MaxWheelAirPressure = 30;
        private int m_EngineCapacity;
        private eLicenseType m_LicenseType;

        public enum eLicenseType
        {
            A = 1,
            A2,
            AA,
            B,
        }

        public Motorcycle()
        {
            m_NumberOfWheels = 2;
            InitialWheelsForFirstTime(k_MaxWheelAirPressure);
        }

        public override void SetVehicleUniqueInformation(List<string> i_ListOfUniqueInformation)
        {
            setUniqueFirstInformation(i_ListOfUniqueInformation[0]);
            setUniqueSecondInformation(i_ListOfUniqueInformation[1]);
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
m_LicensePlate,
m_VehicleModel,
Enum.GetName(typeof(eLicenseType), m_LicenseType),
m_EngineCapacity,
m_VehicleEngine.ToString(),
GetWheelInformationOfVehicle());
        }

        public override string GetSpecialInfoMessage(out int io_AmountOfUniqueInformation)
        {
            io_AmountOfUniqueInformation = 2;

            return 
@"Please enter the motorcycle license type <A,A2,AA,B>.
Then please enter the engine capacity of the motorcycle,should be a number higher than 0.
Notice: the system is case sensitive";
        }

        public override void InsertEngineInformation(float i_CurrentEngineCapacityLeft)
        {
            float engineMaxCapacity = MaxEngineCapacity();

            SetEnergyEngineCapacityLeft(i_CurrentEngineCapacityLeft, engineMaxCapacity);
        }

        protected override float MaxEngineCapacity()
        {
            float engineMaxCapacity;

            if (m_VehicleEngine is CombustionEngine)
            {
                engineMaxCapacity = 5.8f;
            }
            else
            {
                engineMaxCapacity = 2.3f;
            }

            return engineMaxCapacity;
        }

        private void setUniqueFirstInformation(string i_FirstUniqueInformation)
        {
            bool isInsideEnum = Enum.IsDefined(typeof(eLicenseType), i_FirstUniqueInformation);

            if (isInsideEnum == true)
            {
                m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_FirstUniqueInformation);
            }
            else
            {
                throw new ArgumentException("Invalid desired license type.");
            }
        }

        private void setUniqueSecondInformation(string i_SecondUniqueInformation)
        {
            bool isValidNumber = int.TryParse(i_SecondUniqueInformation, out int engineCapacity);

            if (isValidNumber == true)
            {
                if (engineCapacity > 0)
                {
                    m_EngineCapacity = engineCapacity;
                }
                else
                {
                    throw new ArgumentException("Desired engine capacity need to be positive.");
                }
            }
            else
            {
                throw new FormatException("Invalid desired engine capacity, it's need to be a positive number.");
            }
        }
    }
}
