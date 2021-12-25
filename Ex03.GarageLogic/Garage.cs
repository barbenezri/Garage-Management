namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class Garage
    {
        private readonly Dictionary<int, InformationOfVehicleInGarage> r_DictionaryOfVehicles;

        public Garage()
        {
            r_DictionaryOfVehicles = new Dictionary<int, InformationOfVehicleInGarage>();
        }

        public void InsertVehicleToGarge(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleInGarageInformation)
        {
            string licenseNumber = i_VehicleInGarageInformation.LicenseNumber;

            if (IsVehicleExsistInDataStruct(licenseNumber) == false)
            {
                InformationOfVehicleInGarage informationOfVehicle = new InformationOfVehicleInGarage(i_OwnerName, i_OwnerPhoneNumber, i_VehicleInGarageInformation);
                r_DictionaryOfVehicles.Add(licenseNumber.GetHashCode(), informationOfVehicle);
            }
            else
            { /*protection if Ui didnt check with the method IsVehicleExsistInDataStruct*/
                InformationOfVehicleInGarage informationOfVehicleToChangeStatus = pullingInformationOfVehicleInGarageFromDataStruct(licenseNumber);
                informationOfVehicleToChangeStatus.StatusInGarge = InformationOfVehicleInGarage.eStatusInGarge.InRepair;
            }
        }

        public List<string> ListOfVehicleLicenseNumbersByFiltering(InformationOfVehicleInGarage.eStatusInGarge i_StatusInGarage)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (InformationOfVehicleInGarage vehicleInGarage in r_DictionaryOfVehicles.Values)
            {
                if (vehicleInGarage.StatusInGarge == i_StatusInGarage)
                {
                    licenseNumbers.Add(vehicleInGarage.Vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public List<string> ListOfVehicleLicenseNumbers()
        {
            List<string> licenseNumbers = new List<string>();

            foreach (InformationOfVehicleInGarage vehicleInGarage in r_DictionaryOfVehicles.Values)
            {
                licenseNumbers.Add(vehicleInGarage.Vehicle.LicenseNumber);
            }

            return licenseNumbers;
        }

        public void ChangeStatusOfVehicleInGarage(InformationOfVehicleInGarage.eStatusInGarge i_ChangeStatus, string i_LicenseNumber)
        {
            if (IsVehicleExsistInDataStruct(i_LicenseNumber) == true)
            {
                if (Enum.IsDefined(typeof(InformationOfVehicleInGarage.eStatusInGarge), i_ChangeStatus) == true)
                {
                    r_DictionaryOfVehicles[i_LicenseNumber.GetHashCode()].StatusInGarge = i_ChangeStatus;
                }
                else
                {
                    throw new ArgumentException("You tried to put not valiad status");
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntExsist();
            }
        }

        public void FillingAirWheelsToMax(string i_LicenseNumber)
        {
            if (IsVehicleExsistInDataStruct(i_LicenseNumber) == true)
            {
                foreach (Wheel wheelOfVehicle in r_DictionaryOfVehicles[i_LicenseNumber.GetHashCode()].Vehicle.Wheels)
                {
                    wheelOfVehicle.InflatingWheelToMax();
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntExsist();
            }
        }

        public bool IsVehicleExsistInDataStruct(string i_LicenseNumber)
        {
            return r_DictionaryOfVehicles.ContainsKey(i_LicenseNumber.GetHashCode());
        }

        public void RefuelVehicle(string i_LicenseNumber, FuelEngine.eKindOfFuel i_KindOfFuels, float i_AmountOfRefuel)
        {
            if (IsVehicleExsistInDataStruct(i_LicenseNumber) == true)
            {
                InformationOfVehicleInGarage currentInformationOfVehicleInGarage = this.pullingInformationOfVehicleInGarageFromDataStruct(i_LicenseNumber);
                FuelEngine currentFuelEngine = currentInformationOfVehicleInGarage.Vehicle.EngineOfVehicle as FuelEngine;

                if (currentFuelEngine != null)
                {
                    currentFuelEngine.Refueling(i_AmountOfRefuel, i_KindOfFuels);
                }
                else
                {
                    throw new ArgumentException("You try to fill a vehcile that doesn't run on fuel");
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntExsist();
            }
        }

        public void ChargingVehicle(string i_LicenseNumber, float i_AmountOfMinutesToCharge)
        {
            if (IsVehicleExsistInDataStruct(i_LicenseNumber) == true)
            {
                InformationOfVehicleInGarage currentInformationOfVehicleInGarage = this.pullingInformationOfVehicleInGarageFromDataStruct(i_LicenseNumber);
                ElectricEngine currentElectricEngine = currentInformationOfVehicleInGarage.Vehicle.EngineOfVehicle as ElectricEngine;

                if (currentElectricEngine != null)
                {
                    i_AmountOfMinutesToCharge /= 60;
                    currentElectricEngine.ChargeBatteryOfEngine(i_AmountOfMinutesToCharge);
                }
                else
                {
                    throw new ArgumentException("You try to charge a vehcile that does'nt run on electric");
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntExsist();
            }
        }

        public string GettingFullInformationOfVehicleInGarage(string i_LicenseNumber)
        {
            InformationOfVehicleInGarage currentInformationOfVehicleInGarage = null;

            if (IsVehicleExsistInDataStruct(i_LicenseNumber) == true)
            {
                currentInformationOfVehicleInGarage = this.pullingInformationOfVehicleInGarageFromDataStruct(i_LicenseNumber);
            }
            else
            {
                throwExceptionOfVehicleDoesntExsist();
            }

            return currentInformationOfVehicleInGarage.ToString();
        }

        private void throwExceptionOfVehicleDoesntExsist()
        {
            throw new ArgumentException("Your trying to work with vehicle that doesnt exsist in garage");
        }

        private InformationOfVehicleInGarage pullingInformationOfVehicleInGarageFromDataStruct(string i_LicenseNumber)
        {
            return r_DictionaryOfVehicles[i_LicenseNumber.GetHashCode()];
        }
    }
}
