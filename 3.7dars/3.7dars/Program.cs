namespace _3._7dars;

public class Program
{
    static void Main(string[] args)
    {
        var directoryPath = @"C:\Users\kamol\OneDrive\Pictures\Screenshots";
        var filePath = Directory.GetFiles(directoryPath);
        Console.WriteLine($"Start Main ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        foreach (var file in filePath)
        {
            Thread thread = new Thread(() => GetNumberDigits(file));
            thread.Start();
        }
      

    }
    public static object _lock = new object();

    public static void GetNumberDigits(string directoryPath)
    {
        


            lock (_lock)
            {
                var text = File.ReadAllText(directoryPath);
                var number = text.Count(char.IsDigit);
                var newFile =$"File: {directoryPath}  ThreadId:{Thread.CurrentThread.ManagedThreadId} Number of digits: {number}";
            }

        


    }
}
