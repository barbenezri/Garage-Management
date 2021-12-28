using System;

namespace Ex03.GarageLogic
{
    public class InformationOfVehicleInGarage
    {
        private readonly PersonInformation r_PersonInformation;
        private readonly Vehicle r_VehicleInGarageInformation;
        private eStatusInGarge m_StatusInGarge;

        public InformationOfVehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleInGarageInformation)
        {
            r_PersonInformation = new PersonInformation(i_OwnerName, i_OwnerPhoneNumber);
            r_VehicleInGarageInformation = i_VehicleInGarageInformation;
            StatusInGarge = eStatusInGarge.InRepair;
        }

        public enum eStatusInGarge
        {
            InRepair = 1,
            Fixed,
            Paid,
        }

        public eStatusInGarge StatusInGarge
        {
            get => m_StatusInGarge;
            set => m_StatusInGarge = value;
        }

        public string OwnerName
        {
            get => r_PersonInformation.PersonFullName;
        }

        public Vehicle Vehicle
        {
            get => r_VehicleInGarageInformation;
        }

        public string OwnerPhoneNumber
        {
            get => r_PersonInformation.PersonPhoneNumber;
        }

        public override string ToString()
        {
            return string.Format(
@"-----Vehicle informations-----
Status in garage is : {0}
{1}.
-----Owner informations-----
Owner name is : {2}
Owner Phone is : {3}",
Enum.GetName(typeof(eStatusInGarge), m_StatusInGarge),
r_VehicleInGarageInformation.ToString(),
r_PersonInformation.PersonFullName,
r_PersonInformation.PersonPhoneNumber);
        }
    }
}
