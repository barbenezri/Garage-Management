namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    internal class Truck : Vehicle
    {
        private bool m_IsCarringHazardousMaterials;
        private float m_MaxCarringWeight;

        public Truck()
        {
            float maxWheelAirPressure = 26;

            this.m_NumberOfWheels = 16;
            InitialWheelsForFirstTime(maxWheelAirPressure);
        }

        public override void SetVehicleUniqueInformation(List<string> i_ListOfUniqueInformation)
        {
            setFirstUniqueInformation(i_ListOfUniqueInformation[0]);
            setSecondUniqueInformation(i_ListOfUniqueInformation[1]);
        }

        public override string ToString()
        {
            string isCarringHazardousMaterial = m_IsCarringHazardousMaterials ? "is" : "is not";

            return string.Format(
@"Vehicle type : Truck
License plate : {0}
Model name : {1}
The truck {2} carring hazardous material.
Max Carring Weight : {3} 
Engine details : 
{4}
Wheels details : 
{5}",
m_LicenseNumber, m_VehicleModel, isCarringHazardousMaterial, m_MaxCarringWeight, m_Engine.ToString(), GetWheelInformationOfVehicle());
        }

        public override string GettingWithSpecialInformationOfVehicleUiNeedToEnter(out int o_AmountOfUniqueInformation)
        {
            o_AmountOfUniqueInformation = 2;

            return string.Format(
@"Please enter ifthe track carrying hazardous material, options is true foryes ,false forno, AFTER THAT PUSH ENTER
Please enter the max carring weight or the track, shoud be bigger than 0.");
        }

        public override void InsertEngineInformation(float i_CurrentEngineCapcityLeft)
        {
            float engineMaxCapacity = MaxEngineCapacity();

            SetEnergyEngineCapacityLeft(i_CurrentEngineCapcityLeft, engineMaxCapacity);
        }

        protected override float MaxEngineCapacity()
        {
            float engineMaxCapacity = 120;

            return engineMaxCapacity;
        }

        private void setFirstUniqueInformation(string i_FirstUniqueInformation)
        {
            bool isHazzard;
            bool isParse = bool.TryParse((string)i_FirstUniqueInformation, out isHazzard);

            if (isParse == true)
            {
                m_IsCarringHazardousMaterials = isHazzard;
            }
            else
            {
                throw new FormatException("You enter invalid value, value should be true or false");
            }
        }

        private void setSecondUniqueInformation(string i_SecondUniqueInformation)
        {
            float maxCarryingWeight;
            float maxPossibleCarryingWeight = 80000000f;
            bool isParse = float.TryParse((string)i_SecondUniqueInformation, out maxCarryingWeight);

            if (isParse == true)
            {
                if (maxCarryingWeight > 0 && maxCarryingWeight <= maxPossibleCarryingWeight)
                {
                    m_MaxCarringWeight = maxCarryingWeight;
                }
                else
                {
                    string stringMaxCarruingWeightIsNotGood = "not valid maximum carrying weight";

                    throw new ValueOutOfRangeException(maxPossibleCarryingWeight, 0, stringMaxCarruingWeightIsNotGood);
                }
            }
            else
            {
                throw new FormatException("You try to enter not a number to max carrying weight");
            }
        }
    }
}
