namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            GarageUI myGarage = GarageUI.Singelton();
            myGarage.InitiateGarageMenu();
        }
    }
}
