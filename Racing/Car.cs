namespace Racing
{
    public class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Current_Speed { get; set; } = 0;
        public int Max_Speed { get; set; } = 120;
        public double Distance { get; set; } = 0;
        public double DistanceToDrive { get; set; } = 10;

        public void CalculateDistance(int timeInSeconds)
        {
            double tempDistance = (Current_Speed / 3.6) * timeInSeconds;
            Distance += tempDistance / 1000;
        }
    }
}
