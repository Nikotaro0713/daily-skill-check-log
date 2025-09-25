using System;
class Program
{
    static void Main()
    {
        var input = ReadIntArray();
        int takeCount = input[0];
        
        string[] shiritoriArray = ReadStringArray();
        
        if(CheckShiritori(shiritoriArray,takeCount))
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }
    
    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    }
    
    static string[] ReadStringArray()
    {
        return Console.ReadLine().Split();
    }
    
    // しりとりが正常に行われているか判定する
    static bool CheckShiritori(string[] array,int takeCount)
    {
        for(int i = 1; i < array.Length; i++)
        {
            if(array[i - 1].Substring(array[i - 1].Length - takeCount) != array[i].Substring(0,takeCount))
            {
                return false;
            }
        }
        
        return true;
    }
}