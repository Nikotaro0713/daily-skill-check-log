using System;
class Program
{
    static void Main()
    {
        int boardsPerSide = int.Parse(Console.ReadLine());
        int[][] stonesNum = new int[boardsPerSide][];
        
        // 石の数を配列に格納
        for(int i = 0;i < boardsPerSide;i++)
        {
            stonesNum[i] = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
        }
        
        // 基準となる配列
        int[][] basicStoneNum = new int[boardsPerSide][];
        for(int board = 0;board < boardsPerSide; board++)
        {
            // 格納配列の初期化
            basicStoneNum[board] = new int[boardsPerSide];
        }
        
        // 基準となる石の数を格納
        for(int j = 0;j < boardsPerSide; j++)
        {
            for(int k = j;k < boardsPerSide;k++)
            {
                if(basicStoneNum[j][k] == 0)
                {
                    basicStoneNum[j][k] = j + 1;
                }
                
                if(basicStoneNum[boardsPerSide - j - 1][k] == 0)
                {
                    basicStoneNum[boardsPerSide - j - 1][k] = j + 1;
                }
                
                if(basicStoneNum[k][j] == 0)
                {
                    basicStoneNum[k][j] = j + 1;
                    basicStoneNum[k][boardsPerSide - j - 1] = j + 1;
                }
                
                if(basicStoneNum[k][boardsPerSide - j - 1] == 0)
                {
                    basicStoneNum[k][boardsPerSide - j - 1] = j + 1;
                }
            }
        }
        
        // 取り除く石の数を求める
        int removeStoneNum = 0;
        for(int x = 0;x < boardsPerSide;x++)
        {
            for(int y = 0;y < boardsPerSide;y++)
            {
                // 入力された石の数と基準となる石の数の差分を出す
                removeStoneNum += stonesNum[x][y] - basicStoneNum[x][y];
            }
        }
        
        Console.WriteLine(removeStoneNum);
    }
}