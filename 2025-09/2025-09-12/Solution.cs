using System;
class Program
{
    static void Main()
    {
        string [] str = Console.ReadLine().Split();
        
        int productsCount = int.Parse(str[0]);
        int lowestAmount = int.Parse(str[1]);
        
        string [] amountStr = Console.ReadLine().Split();
        
        int totalAmount = 0;
        int maxAmount = 0;
        
        // 商品の合計金額と最高金額を求める
        for(int i = 0; i < productsCount; i++)
        {
            var productAmount = int.Parse(amountStr[i]);
            
            if(maxAmount < productAmount)
            {
                maxAmount = productAmount;
            }
            
            totalAmount += productAmount;
        }
        
        // 最高金額が基準金額より多ければ合計金額から最高金額の半額を引く
        if(maxAmount >= lowestAmount)
        {
            totalAmount -= maxAmount / 2;
        }
        
        Console.WriteLine(totalAmount);
    }
}