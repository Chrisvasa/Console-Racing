namespace Racing
{
    public class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Current_Speed { get; set; } = 0;
        public int Max_Speed { get; set; } = 120;
        public double Distance { get; set; } = 0;
        public double DistanceToDrive { get; set; } = 10;
        public double TimeDriven { get; set; } = 0;
        public async Task CalculateDistance()
        {
            //if(Distance > 9)
            //{
            //    double temp = 10 - Distance;
            //    TimeDriven += (temp * 1000) / (Current_Speed / 3.6);
            //    Distance = 10;
            //}
            //else
            //{
            TimeDriven += 100 / (Current_Speed / 3.6);
            Distance += 0.1;
            await Task.Delay(100);

            //}
        }
    }
}
