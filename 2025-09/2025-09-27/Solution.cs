using System;
class Program
{
    static void Main()
    {
        var input = ReadIntArray();
        int houseCount = input[0];
        int maxDistance = input[1];
        
        int[] ownHousePos = ReadIntArray();
        
        int[][] targetHousePos = new int[houseCount][];
        for(int i = 0; i < houseCount; i++)
        {
            targetHousePos[i] = ReadIntArray();
        }
        
        Console.WriteLine(CountToGreet(ownHousePos,targetHousePos,maxDistance));
    }
    
    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    }
    
    static int CountToGreet(int[] basePos,int[][] targetPos,int maxDistance)
    {
        int greetCount = 0;
        for(int i = 0; i < targetPos.Length; i++)
        {
            var dist = Math.Abs(basePos[0] - targetPos[i][0]) + Math.Abs(basePos[1] - targetPos[i][1]);
            if(dist <= maxDistance)
            {
                greetCount++;
            }
        }
        return greetCount;
    }
}