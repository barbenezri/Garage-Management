namespace Ex03.GarageLogic
{
    public class PersonInformation
    {
        private string m_PersonFullName;
        private string m_PersonPhoneNumber;

        public PersonInformation(string i_PersonName, string i_PersonPhoneNumber)
        {
            m_PersonFullName = i_PersonName;
            m_PersonPhoneNumber = i_PersonPhoneNumber;
        }

        public string PersonFullName
        {
            get
            {
                return m_PersonFullName;
            }
        }

        public string PersonPhoneNumber
        {
            get
            {
                return m_PersonPhoneNumber;
            }
        }
    }
}
