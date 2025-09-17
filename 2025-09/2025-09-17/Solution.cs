using System;
class Program
{
    static void Main()
    {
        int researchDays = int.Parse(Console.ReadLine());
        TimeSpan totalTime = new TimeSpan();
        
        for(int i = 0; i < researchDays; i++)
        {
            var input = Console.ReadLine().Split();
            
            totalTime += calcTimeDiff(input[0],input[1]);
        }
        
        Console.WriteLine($"{(int)totalTime.TotalHours} {totalTime.Minutes}");
    }
    
    static TimeSpan calcTimeDiff(string startTime, string endTime)
    {
        TimeSpan t1 = TimeSpan.Parse(startTime);
        TimeSpan t2 = TimeSpan.Parse(endTime);
            
        return t2 - t1;
    }
}