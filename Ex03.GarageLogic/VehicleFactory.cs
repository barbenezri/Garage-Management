namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            FuelCar = 1,
            ElectircCar,
            FuelMotocycle,
            ElectricMotocycle,
            Truck,
        }

        public static Vehicle MakeVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicleToReturn = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    vehicleToReturn = new Car();
                    vehicleToReturn.VehicleEngine = new CombustionEngine(CombustionEngine.eFuelKind.Octan95);
                    break;
                case eVehicleType.ElectircCar:
                    vehicleToReturn = new Car();
                    vehicleToReturn.VehicleEngine = new ElectricEngine();
                    break;
                case eVehicleType.FuelMotocycle:
                    vehicleToReturn = new Motocycle();
                    vehicleToReturn.VehicleEngine = new CombustionEngine(CombustionEngine.eFuelKind.Octan98);
                    break;
                case eVehicleType.ElectricMotocycle:
                    vehicleToReturn = new Motocycle();
                    vehicleToReturn.VehicleEngine = new ElectricEngine();
                    break;
                case eVehicleType.Truck:
                    vehicleToReturn = new Truck();
                    vehicleToReturn.VehicleEngine = new CombustionEngine(CombustionEngine.eFuelKind.Soler);
                    break;
            }

            return vehicleToReturn;
        }
    }
}
