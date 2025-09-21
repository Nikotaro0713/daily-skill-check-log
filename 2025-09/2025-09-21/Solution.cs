using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] inputArray = ReadIntArray();
        int bingoSize = inputArray[0];  // ビンゴカードのサイズ
        int drawNum = inputArray[1];    // 抽選回数
        
        int[][] bingoCard = new int[bingoSize][];   // ビンゴカードの数字を格納する配列
        for(int i = 0;i < bingoSize; i++)
        {
            bingoCard[i] = ReadIntArray();
        }
        
        int[] drawnNums = ReadIntArray(); // 抽選された数字を格納する配列
        
        int bingoCount = 0;

        int rowBingoCount = bingoCard
                            .Where(row => row.All(num => num == 0 || drawnNums.Contains(num)))
                            .Count();    // 行のビンゴ数
        int colBingoCount = Enumerable.Range(0,bingoSize)
                            .Where(col => Enumerable.Range(0,bingoSize)
                            .All(row => bingoCard[row][col] == 0 || drawnNums.Contains(bingoCard[row][col])))
                            .Count();    // 列のビンゴ数
        // 斜めのビンゴがあるか判定する
        if(Enumerable.Range(0, bingoSize).All(i => bingoCard[i][i] == 0 || drawnNums.Contains(bingoCard[i][i])))
        {
            bingoCount++;
        }
        
        if(Enumerable.Range(0, bingoSize).All(i => bingoCard[i][bingoSize - 1 - i] == 0 || drawnNums.Contains(bingoCard[i][bingoSize - 1 - i])))
        {
            bingoCount++;
        }
        // 合計のビンゴ数を加算
        bingoCount += rowBingoCount + colBingoCount;
        Console.WriteLine(bingoCount);
    }
    
    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    }
}