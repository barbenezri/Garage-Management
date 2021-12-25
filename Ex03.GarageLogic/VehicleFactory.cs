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

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicleToReturn;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    {
                        vehicleToReturn = new Car();
                        vehicleToReturn.EngineOfVehicle = new FuelEngine(FuelEngine.eKindOfFuel.Octan95);
                        break;
                    }

                case eVehicleType.ElectircCar:
                    {
                        vehicleToReturn = new Car();
                        vehicleToReturn.EngineOfVehicle = new ElectricEngine();
                        break;
                    }

                case eVehicleType.FuelMotocycle:
                    {
                        vehicleToReturn = new Motocycle();
                        vehicleToReturn.EngineOfVehicle = new FuelEngine(FuelEngine.eKindOfFuel.Octan98);
                        break;
                    }

                case eVehicleType.ElectricMotocycle:
                    {
                        vehicleToReturn = new Motocycle();
                        vehicleToReturn.EngineOfVehicle = new ElectricEngine();
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        vehicleToReturn = new Truck();
                        vehicleToReturn.EngineOfVehicle = new FuelEngine(FuelEngine.eKindOfFuel.Soler);
                        break;
                    }

                default:
                    {
                        vehicleToReturn = null;
                        break;
                    }
            }

            return vehicleToReturn;
        }
    }
}
