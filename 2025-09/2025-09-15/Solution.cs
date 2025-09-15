using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int flowerNum = int.Parse(Console.ReadLine());
        string flowers = Console.ReadLine();
        
        // 入力された文字列から重複を除いて数える
        int maxFlower = flowers
            .Where(c => !char.IsWhiteSpace(c))
            .Distinct()
            .Count();
        
        Console.WriteLine(maxFlower);
    }
}