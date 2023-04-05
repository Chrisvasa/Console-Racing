namespace Racing
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Race racing = new();
            await racing.Start();
        }
    }
}