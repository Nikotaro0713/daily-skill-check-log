using System;
class Program
{
    static void Main()
    {
        int playerNum = ReadInt();
        int[] ballCount = new int[playerNum];
        for(int i = 0; i < playerNum; i++)
        {
            ballCount[i] = ReadInt();
        }
        int passCount = ReadInt();
        
        // 1行読み込み、パスを行う
        for(int j = 0;j < passCount;j++)
        {
            var passInfo = ReadIntArray();
            PassBall(ballCount,passInfo[0],passInfo[1],passInfo[2]);
        }
        
        // 各自が持っているボールの数を出力
        foreach(var num in ballCount)
        {
            Console.WriteLine(num);    
        }
        
    }
    
    // 1行読み込んでint型に変換
    static int ReadInt()
    {
        return int.Parse(Console.ReadLine());
    }
    
    // 1行読み込んでint型配列に変換
    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    }
    
    // 引数に応じてパスを行う
    static void PassBall(int[] ballArray,int kicker,int receiver, int ballCount)
    {
        // インデックスを調整
        int kickerIndex = kicker - 1;
        int receiverIndex = receiver - 1;
        
        if(ballArray[kickerIndex] < ballCount)
        {
            ballArray[receiverIndex] += ballArray[kickerIndex];
            ballArray[kickerIndex] = 0;
        }
        else
        {
            ballArray[receiverIndex] += ballCount;
            ballArray[kickerIndex] -= ballCount;
        }
    }
}