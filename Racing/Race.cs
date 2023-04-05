using Spectre.Console;

namespace Racing
{
    public class Race
    {
        static List<Car> cars = new List<Car>();
        public async Task Start()
        {
            List<Task> tasks = new List<Task>();
            PopulateCarList(2);
            Console.WriteLine("Task started");
            cars.ForEach(async car =>
            {
                var LastTask = RaceLogic(car);
                tasks.Add(LastTask);
            });

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Task ended");
            Console.ReadKey();
        }

        private void PopulateCarList(int amountOfCars)
        {
            for (int i = 0; i < amountOfCars; i++)
            {
                Car car = new Car();
                car.Id = i + 1;
                car.Name = "Car " + (i + 1);
                cars.Add(car);
            }
        }
        private async Task ShowRaceProgress(Car car)
        {
            List<ProgressTask> tasks = new List<ProgressTask>();
            await AnsiConsole.Progress()
                    .AutoRefresh(true)
                    .AutoClear(false)
                    .HideCompleted(false)
                    .Columns(new ProgressColumn[]
                    {
                    new TaskDescriptionColumn(),    // Task Description
                    new ProgressBarColumn(),        // Progress bar
                    new PercentageColumn(),         // Percentage
                    new ElapsedTimeColumn(),        // Elapsed time
                    new SpinnerColumn(),            // Spinner
                    })
                    .StartAsync(async ctx =>
                    {
                        var task = ctx.AddTask($"[{RandomColor()}]Car{car.Id}[/]");
                        while (!ctx.IsFinished)
                        {
                            await Task.Delay(100);
                            task.Increment(10);
                        }
                    });
        }

        private async Task RaceLogic(Car car)
        {
            double tempDist = 0;
            while (car.Distance <= car.DistanceToDrive)
            {
                car.Current_Speed = car.Max_Speed;
                //await DriveCar(car);
                car.CalculateDistance(6);
                if (tempDist + 0.99 <= car.Distance)
                {
                    await Task.Delay(1000);
                    tempDist = car.Distance;
                    Console.WriteLine($"{car.Name} - Distance driven: {car.Distance:N1}km");
                    await Problems(car);
                }
            }
            Console.WriteLine($"{car.Name} drove past the finish line! - Current top speed is: {car.Current_Speed}");
        }

        private string RandomColor()
        {
            Random random = new Random();
            string color;
            switch (random.Next(5))
            {
                case 0:
                    color = "red";
                    break;
                case 1:
                    color = "green";
                    break;
                case 2:
                    color = "blue";
                    break;
                case 3:
                    color = "yellow";
                    break;
                default:
                    color = "purple";
                    break;
            }
            return color;
        }

        private async Task Problems(Car car)
        {
            Random random = new Random();
            switch (random.Next(50))
            {
                case 0:
                    // Refuel - Wait 30 sec
                    await Wait(30);
                    Console.WriteLine($"{car.Name} is refuelling");
                    break;
                case int n when (n > 0 && n <= 2):
                    // Tire change - Wait 20 sec
                    await Wait(20);
                    Console.WriteLine($"{car.Name} are changing their wheels");
                    break;
                case int n when (n >= 3 && n <= 7):
                    // Wash windscreen - Wait 10 sec
                    await Wait(10);
                    Console.WriteLine($"{car.Name} is washing their windscreen squeaky clean");
                    break;
                case int n when (n >= 8 && n <= 19):
                    // Lower max speed by 1km/h
                    Console.WriteLine($"{car.Name} had a minor engine failure. Speed is now decreased by 1km/h");
                    car.Max_Speed -= 1;
                    break;
                default:
                    // No fault - Carry on
                    break;
            }
        }

        public async Task<Car> DriveCar(Car car)
        {
            int racingTime = 10;
            while (true)
            {
                await Wait(racingTime);
                car.CalculateDistance(1);

                if (car.Distance >= car.DistanceToDrive)
                {
                    return car;
                }
            }
        }

        private async Task Wait(int delay = 1)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay / 10));
        }
    }
}

/* RACE
 * Switch case for errors?
 * > Every 30 seconds each car gets a number between 1-50(?)
 * > And then calls for a method containing a switch case with different scenarios
 * EXAMPLE:
 * - switch(random number)
 * - case 1:
 *      Fuel break;
 * - case 2:
 *      Change tires.
 * - case 3-15:
 *       Do task;
 *--------------------------------------------------------------------------------
 */