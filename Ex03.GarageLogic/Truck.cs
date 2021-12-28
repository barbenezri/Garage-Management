using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const float k_MaxWheelAirPressure = 25;
        private bool m_IsCanKeepCargoCool;
        private float m_CargoVolume;

        public Truck()
        {
            m_NumberOfWheels = 16;
            InitialWheelsForFirstTime(k_MaxWheelAirPressure);
        }

        public override void SetVehicleUniqueInformation(List<string> i_ListOfUniqueInformation)
        {
            setFirstUniqueInformation(i_ListOfUniqueInformation[0]);
            setSecondUniqueInformation(i_ListOfUniqueInformation[1]);
        }

        public override string ToString()
        {
            string isCanKeepCargoCool = m_IsCanKeepCargoCool ? "can" : "can't";

            return string.Format(
@"Vehicle type : Truck
License plate : {0}
Model name : {1}
The truck {2} Keep Cargo Cool.
Max Carring Weight : {3} 
-----Engine details----- 
{4}
-----Wheels details-----
{5}",
m_LicensePlate,
m_VehicleModel,
isCanKeepCargoCool,
m_CargoVolume,
m_Engine.ToString(),
GetWheelInformationOfVehicle());
        }

        public override string GetSpecialInfoMessage(out int o_AmountOfUniqueInformation)
        {
            o_AmountOfUniqueInformation = 2;

            return string.Format(
@"Please enter if your truck keep cargo cool <[true]/[false]>.
Then please enter truck possible cargo volume, shoud be bigger than 0.
Notice: the system is case sensetive.");
        }

        public override void InsertEngineInformation(float i_CurrentEngineCapcityLeft)
        {
            float engineMaxCapacity = MaxEngineCapacity();

            SetEnergyEngineCapacityLeft(i_CurrentEngineCapcityLeft, engineMaxCapacity);
        }

        protected override float MaxEngineCapacity()
        {
            float engineMaxCapacity = 130;

            return engineMaxCapacity;
        }

        private void setFirstUniqueInformation(string i_FirstUniqueInformation)
        {
            bool isParse = bool.TryParse((string)i_FirstUniqueInformation, out bool isCanKeepCargoCool);

            if (isParse == true)
            {
                m_IsCanKeepCargoCool = isCanKeepCargoCool;
            }
            else
            {
                throw new FormatException("You enter invalid value, value should be true or false");
            }
        }

        private void setSecondUniqueInformation(string i_SecondUniqueInformation)
        {
            bool isParse = float.TryParse((string)i_SecondUniqueInformation, out float CargoVolume);
            
            if (isParse == true)
            {
                m_CargoVolume = CargoVolume;
            }
            else
            {
                throw new FormatException("You enter invalid value, value should be number bigger then 0");
            }
        }
    }
}
