using System;
class Program
{
    static void Main()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
        int chargedAmount = input[0];
        int rideCount = input[1];
        
        int[] fareArray = new int[rideCount];
        for(int i = 0; i < rideCount; i++)
        {
            fareArray[i] = ReadInt();
        }
        
        int currentCharge = chargedAmount;
        int currentPoint = 0;
        
        OutputChargeAndPoint(currentCharge,currentPoint,fareArray);
    }
    
    static int ReadInt()
    {
        return int.Parse(Console.ReadLine());
    }
    
    static void OutputChargeAndPoint(int charge,int point,int[] fareArray)
    {
        int currentCharge = charge;
        int currentPoint = point;
        
        for(int i = 0; i < fareArray.Length; i++)
        {
            if(currentPoint >= fareArray[i])
            {
                currentPoint = CalcPoint(currentPoint,fareArray[i],1);
            }
            else
            {
                currentCharge = CalcCharge(currentCharge,fareArray[i]);
                currentPoint = CalcPoint(currentPoint,fareArray[i],0);
            }
            Console.WriteLine(currentCharge + " " + currentPoint);
        }
    }
    
    static int CalcCharge(int charge,int fare)
    {
        return charge - fare;
    }
    
    static int CalcPoint(int point,int fare,int isSave)
    {
        // isSaveが0の場合ポイントを貯める、1の場合使う
        if(isSave == 0)
        {
            point += fare * 10 / 100;
        }
        else
        {
            point -= fare;
        }
        return point;
    }
}