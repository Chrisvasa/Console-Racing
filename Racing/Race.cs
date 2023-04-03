using Spectre.Console;

namespace Racing
{
    public class Race
    {
        static List<Car> cars = new List<Car>();
        static List<Thread> threads = new List<Thread>();
        static int count = 0;
        public async void Start()
        {
            Console.WriteLine("Hur många bilar ska tävla?");
            int numOfThreads = int.Parse(Console.ReadLine());
            FillList(numOfThreads);
            foreach (Car car in cars)
            {
                Console.WriteLine(car.Name);
                var thread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    RaceTest();
                });
                threads.Add(thread);
            }
            while (count < numOfThreads)
            {
                for (int i = 0; i < threads.Count; i++)
                {
                    threads[i].Start();
                }
            };

        }

        private void FillList(int amountOfCars)
        {
            for (int i = 0; i < amountOfCars; i++)
            {
                Car car = new Car();
                car.Id = i;
                car.Name = "Car " + (i + 1);
                cars.Add(car);
            }
        }
        private async Task RaceTest()
        {
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
                        string test = RandomColor();
                        var task = ctx.AddTask($"[{test}]Car[/]");
                        while (!ctx.IsFinished)
                        {
                            await Task.Delay(150);
                            task.Increment(3);
                        }
                        count++;

                    });
        }

        private string RandomColor()
        {
            Random random = new Random();
            string test;
            switch (random.Next(5))
            {
                case 0:
                    test = "red";
                    break;
                case 1:
                    test = "green";
                    break;
                case 2:
                    test = "blue";
                    break;
                case 3:
                    test = "yellow";
                    break;
                default:
                    test = "purple";
                    break;
            }
            return test;
        }

        private void Problems(int num)
        {
            switch (num)
            {
                case 0:
                    // Refuel - Wait 30 sec
                    break;
                case 1:
                    // Tire change - Wait 20 sec
                    break;
                case 2 - 5:
                    // Wash windscreen - Wait 10 sec
                    break;
                case 6 - 16:
                    // Lower max speed by 1km/h
                    break;
                default:
                    // No fault - Carry on
                    break;
            }
        }
    }
}

//WaitHandle[] waitHandles = new WaitHandle[numOfThreads];
//FillList(numOfThreads);
//RaceTest(numOfThreads);
//foreach (Car car in cars)
//{
//    Console.WriteLine(car.Name);
//    var test = new Thread(Worker);
//    test.Start();
//    threads.Add(test);
//}

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