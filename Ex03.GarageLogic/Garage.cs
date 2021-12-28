using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<int, GarageAccount> r_DictionaryOfVehicles;

        public Garage()
        {
            r_DictionaryOfVehicles = new Dictionary<int, GarageAccount>();
        }

        public void InsertVehicleToGarge(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleToInsert)
        {
            string licensePlate = i_VehicleToInsert.LicensePlate;

            if (IsVehicleExsistInDataStruct(licensePlate) == false)
            {
                GarageAccount informationOfVehicle = new GarageAccount(i_OwnerName, i_OwnerPhoneNumber, i_VehicleToInsert);
                r_DictionaryOfVehicles.Add(licensePlate.GetHashCode(), informationOfVehicle);
            }
            else
            { 
                GarageAccount informationOfVehicleToChangeStatus = getVehicleInfoFromGarage(licensePlate);
                informationOfVehicleToChangeStatus.StatusInGarge = GarageAccount.eStatusInGarge.InRepair;
            }
        }

        public List<string> ListOfVehiclelicensePlatesByFiltering(GarageAccount.eStatusInGarge i_StatusInGarage)
        {
            List<string> licensePlates = new List<string>();

            foreach (GarageAccount vehicleInGarage in r_DictionaryOfVehicles.Values)
            {
                if (vehicleInGarage.StatusInGarge == i_StatusInGarage)
                {
                    licensePlates.Add(vehicleInGarage.Vehicle.LicensePlate);
                }
            }

            return licensePlates;
        }

        public List<string> ListOfVehiclelicensePlates()
        {
            List<string> licensePlates = new List<string>();

            foreach (GarageAccount vehicleInGarage in r_DictionaryOfVehicles.Values)
            {
                licensePlates.Add(vehicleInGarage.Vehicle.LicensePlate);
            }

            return licensePlates;
        }

        public void ChangeStatusOfVehicleInGarage(GarageAccount.eStatusInGarge i_ChangeStatus, string i_LicensePlate)
        {
            if (IsVehicleExsistInDataStruct(i_LicensePlate) == true)
            {
                if (Enum.IsDefined(typeof(GarageAccount.eStatusInGarge), i_ChangeStatus) == true)
                {
                    r_DictionaryOfVehicles[i_LicensePlate.GetHashCode()].StatusInGarge = i_ChangeStatus;
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

        public void FillingAirWheelsToMax(string i_LicensePlate)
        {
            if (IsVehicleExsistInDataStruct(i_LicensePlate) == true)
            {
                foreach (Wheel wheelOfVehicle in r_DictionaryOfVehicles[i_LicensePlate.GetHashCode()].Vehicle.Wheels)
                {
                    wheelOfVehicle.InflatingWheelToMax();
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntExsist();
            }
        }

        public bool IsVehicleExsistInDataStruct(string i_LicensePlate)
        {
            return r_DictionaryOfVehicles.ContainsKey(i_LicensePlate.GetHashCode());
        }

        public void RefuelVehicle(string i_LicensePlate, CombustionEngine.eFuelKind i_KindOfFuels, float i_AmountOfRefuel)
        {
            if (IsVehicleExsistInDataStruct(i_LicensePlate) == true)
            {
                GarageAccount vehicleInfo = getVehicleInfoFromGarage(i_LicensePlate);
                CombustionEngine currentFuelEngine = vehicleInfo.Vehicle.VehicleEngine as CombustionEngine;

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

        public void ChargingVehicle(string i_LicensePlate, float i_AmountOfMinutesToCharge)
        {
            if (IsVehicleExsistInDataStruct(i_LicensePlate) == true)
            {
                GarageAccount currentInformationOfVehicleInGarage = this.getVehicleInfoFromGarage(i_LicensePlate);
                ElectricEngine currentElectricEngine = currentInformationOfVehicleInGarage.Vehicle.VehicleEngine as ElectricEngine;

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

        public string GettingFullInformationOfVehicleInGarage(string i_LicensePlate)
        {
            GarageAccount currentInformationOfVehicleInGarage = null;

            if (IsVehicleExsistInDataStruct(i_LicensePlate) == true)
            {
                currentInformationOfVehicleInGarage = getVehicleInfoFromGarage(i_LicensePlate);
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

        private GarageAccount getVehicleInfoFromGarage(string i_LicensePlate)
        {
            return r_DictionaryOfVehicles[i_LicensePlate.GetHashCode()];
        }
    }
}
