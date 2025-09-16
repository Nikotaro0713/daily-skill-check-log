using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int productCount = int.Parse(Console.ReadLine());
        int[] products = new int[productCount];
        
        for(int i = 0;i < productCount; i++)
        {
            string input = Console.ReadLine();
            if(string.IsNullOrEmpty(input))
            {
                break;
            }
            products[i] = int.Parse(input);
        }
        
        int shortage = calculateShortage(productCount,products);
        
        Console.WriteLine(shortage);
    }
    
    // 不足している製品が何種類あるか算出するメソッド
    static int calculateShortage(int num,int[] products)
    {
        int[] numCheckArray = new int[num];
        
        for(int j = 0;j < num; j++)
        {
            numCheckArray[j] = j + 1;
        }
        
        int shortage = numCheckArray.Count(n => !products.Contains(n));
        
        return shortage;
    }
}