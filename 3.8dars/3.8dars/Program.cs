using System.Diagnostics;

namespace _3._8dars;

public class Program
{
    static async Task  Main(string[] args)
    {
        Console.WriteLine("Main Started");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        GetTea();
        Console.WriteLine(stopwatch.ToString());
        Console.WriteLine("Main Ended");
    }
     
    public static async Task<string> GetTea()
    {
        var boiledWater = BoilWater();
        Console.WriteLine("Shkafdan choynakni oldik");
        Console.WriteLine("quruqchoy soldik");
        var res = $"Choynakka {boiledWater} ni quydik";
        return res;

    }
    public static async Task<string> BoilWater()
    {
        Console.WriteLine("Tifalni yoqdik");
        await Task.Delay(5000);
        Console.WriteLine("Suv qaynadi");
        return "Qaynagan suv";
    }
}
