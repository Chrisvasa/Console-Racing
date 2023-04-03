namespace Racing
{
    public class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Current_Speed { get; set; } = 0;
        public int Max_Speed { get; set; } = 120;
    }
}
