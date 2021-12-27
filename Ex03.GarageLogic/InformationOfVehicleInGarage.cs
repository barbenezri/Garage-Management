using System;

namespace Ex03.GarageLogic
{
    public class InformationOfVehicleInGarage
    {
        private PersonInformation m_PersonInformation;
        private Vehicle m_VehicleInGarageInformation;
        private eStatusInGarge m_StatusInGarge;

        public InformationOfVehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleInGarageInformation)
        {
            m_PersonInformation = new PersonInformation(i_OwnerName, i_OwnerPhoneNumber);
            m_VehicleInGarageInformation = i_VehicleInGarageInformation;
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
            get => m_PersonInformation.PersonFullName;
        }

        public Vehicle Vehicle
        {
            get => m_VehicleInGarageInformation;
        }

        public string OwnerPhoneNumber
        {
            get => m_PersonInformation.PersonPhoneNumber;
        }

        public override string ToString()
        {
            return string.Format(
@"-----Vehicle informations-----
Status in garage is : {0}
{1}.
-----Owner informations-----
Owner name is : {2}
Owner Phone is {3}",
Enum.GetName(typeof(eStatusInGarge), m_StatusInGarge),
m_VehicleInGarageInformation.ToString(),
m_PersonInformation.PersonFullName,
m_PersonInformation.PersonPhoneNumber);
        }
    }
}
