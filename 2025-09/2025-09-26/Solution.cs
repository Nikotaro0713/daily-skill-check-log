using System;
class Program
{
    static void Main()
    {
        int[] parentCard = ReadIntArray();
        int childCount = ReadInt();
        int[][] childCard = new int[childCount][];
        for(int i = 0; i < childCount; i++)
        {
            childCard[i] = ReadIntArray();
        }
        
        for(int j = 0;j < childCount; j++)
        {
            if(CheckHighOrLow(parentCard,childCard[j]))
            {
                Console.WriteLine("High");
            }
            else
            {
                Console.WriteLine("Low");
            }
        }
    }
    
    static int ReadInt()
    {
        return int.Parse(Console.ReadLine());
    }
    
    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    }
    
    // 親の方が強い場合はtrue、そうでない場合はfalseを返す
    static bool CheckHighOrLow(int[] parent, int[] child)
    {
        if(parent[0] == child[0])
        {
            if(parent[1] < child[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(parent[0] > child[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}