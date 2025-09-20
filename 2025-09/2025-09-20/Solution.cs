using System;
using System.Linq;
class Program
{
    static void Main()
    {
        var input = ReadIntArray();
        int shopCount = input[0];
        int typesOfVegetables = input[1];
        
        int[][] vegetableArray = new int[shopCount][];
        for(int i = 0; i < shopCount; i++)
        {
            vegetableArray[i] = ReadIntArray();
        }
        
        int purchaseShop = 0;
        int[] purchaseShopArray = new int[typesOfVegetables];
        int minValue = 0;
        
        for(int j = 0;j < typesOfVegetables; j++)
        {
            minValue = 0;
            purchaseShop = 0;
            for(int k = 0; k < shopCount; k++)
            {
                if(k == 0)
                {
                    minValue = vegetableArray[0][j];
                    purchaseShop = 0;
                }
                else if(vegetableArray[k][j] < minValue)
                {
                    minValue = vegetableArray[k][j];
                    purchaseShop = k;
                }
            }
            purchaseShopArray[j] = purchaseShop;
        }
        
        Console.WriteLine(purchaseShopArray.Distinct().Count());
    }
    
    static int ReadInt()
    {
        int inputInt = int.Parse(Console.ReadLine());
        return inputInt;
    }
    
    static int[] ReadIntArray()
    {
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
        return arr;
    }
    
}