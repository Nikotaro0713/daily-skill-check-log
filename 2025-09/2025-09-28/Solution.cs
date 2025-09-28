using System;
class Program
{
    static void Main()
    {
        int flowerCount = ReadInt();
        string baseFlower = ReadString();
        string judgeFlower = ReadString();
        
        if(isRotation(baseFlower,judgeFlower))
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
    }
    
    static int ReadInt()
    {
        return int.Parse(Console.ReadLine());
    }
    
    static string ReadString()
    {
        return Console.ReadLine();
    }
    
    static bool isRotation(string baseFlower, string judgeFlower)
    {
        if(baseFlower.Length != judgeFlower.Length)
        {
            return false;
        }
        
        // ベースとなる文字列を結合し、その中に判定対象の文字列が含まれるか判定する
        return (baseFlower + baseFlower).Contains(judgeFlower);
    }
}