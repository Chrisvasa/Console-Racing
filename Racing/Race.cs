using System.Diagnostics;

namespace Racing
{
    public class Race
    {
        private static bool isRacing = true;
        //When called starts a race
        public static async Task Start()
        {
            List<Car> cars = PopulateCarList(); // Fills the list with cars
            List<Task> tasks = new List<Task>();
            // Adds a task for each car in the cars list, which allows the race to happen
            cars.ForEach(car =>
            {
                Task currentTask = RaceLogic(car);
                tasks.Add(currentTask);
            });

            Task printStatsTask = PrintStatsAsync(cars);
            // When all tasks are finished the race is over
            await Task.WhenAll(tasks);

            Console.WriteLine("Race has ended!");
            await printStatsTask; // Wait for the print stats task to complete
        }

        //Prompts the user for how many cars to be added
        //If valid input is given, fills the list with cars and names them in an numeric order
        private static List<Car> PopulateCarList()
        {
            List<Car> cars = new List<Car>();

            Console.WriteLine("How many cars do you want in this race?");
            if (int.TryParse(Console.ReadLine(), out int amountOfCars))
            {
                for (int i = 0; i < amountOfCars; i++)
                {
                    Car car = new Car
                    {
                        Id = i + 1,
                        Name = "Car " + (i + 1)
                    };
                    cars.Add(car);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Restart the program and try again.");
            }
            return cars;
        }

        // Contains the logic for every car in the race
        private static async Task RaceLogic(Car car)
        {
            await PrintMessage($"{car.Name} has started");
            double time = 0;
            // While the car is still under 10km distance driven
            while (car.Distance < car.DistanceToDrive)
            {
                // Checks if the car has been driving 30 seconds since the last check
                if (car.TimeDriven - 30 >= time)
                {
                    // Calls the Problem method for the given car
                    await Problems(car);
                    time = car.TimeDriven;
                }
                // Sets the current speed to be equal to the allowed maximum speed for the car
                car.Current_Speed = car.Max_Speed;
                if (car.Distance < car.DistanceToDrive)
                {
                    await car.CalculateDistance();
                    //Console.WriteLine($"{car.Name} - Distance driven: {car.Distance} km");
                }
            }
            Console.WriteLine($"{car.Name} drove past the finish line! - They drove {car.Distance:N1} km \nCurrent top speed is: {car.Current_Speed} and it took them: {car.TimeDriven:N3} seconds");
            // Since the race is over, this will disable the possibility for the user to print stats
            isRacing = false;
        }

        // Allows the user to print some stats for each car during the race
        private static async Task PrintStatsAsync(List<Car> cars)
        {
            while (isRacing)
            {
                PrintStats(cars);
            }
        }

        // Prints the cars current speed and distance driven so far
        private static void PrintStats(List<Car> cars)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true); // Clear the key buffer
                Console.WriteLine("\nRace Stats:");
                foreach (Car car in cars)
                {
                    Console.WriteLine($"{car.Name} is currently driving at {car.Current_Speed}km/h and has driven a distance of {car.Distance:N1}km");
                }
                Console.WriteLine();
            }
        }
        // This method contains different problems that can occur to each car when called
        // Will get a new random number every time its called
        private static async Task Problems(Car car)
        {
            Random random = new Random();
            switch (random.Next(50))
            {
                case 0:
                    // Refuel - Wait 30 sec
                    PrintMessage($"{car.Name} is refuelling");
                    await Wait(car, 30);
                    break;
                case int n when (n > 0 && n <= 2):
                    // Tire change - Wait 20 sec
                    PrintMessage($"{car.Name} are changing their wheels");
                    await Wait(car, 20);
                    break;
                case int n when (n >= 3 && n <= 7):
                    // Wash windscreen - Wait 10 sec
                    PrintMessage($"{car.Name} is washing their windscreen squeaky clean");
                    await Wait(car, 10);
                    break;
                case int n when (n >= 8 && n <= 19):
                    // Lower max speed by 1km/h
                    PrintMessage($"{car.Name} had a minor engine failure. Speed is now decreased by 1km/h");
                    car.Max_Speed -= 1;
                    break;
                default:
                    // No fault - Carry on
                    break;
            }
        }
        // Prints the given message in the console
        private static async Task PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
        // Adds the given delay to the cars total time, and delays the task
        private static async Task Wait(Car car, int delay = 1)
        {
            car.TimeDriven += delay;
            await Task.Delay(TimeSpan.FromSeconds(delay / 10));
        }
    }
}