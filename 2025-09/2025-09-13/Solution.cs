using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int moveCount = input[0];
        int numRows = input[1];
        int numCols = input[2];
        
        // 1回あたりの移動数
        const int MoveNum = 1;
        
        int[] seat = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int seatX = seat[0];
        int seatY = seat[1];
        
        char[] moveDirection = Console.ReadLine().ToCharArray();
        char[] allowedDirection = {'F','B','L','R'};
        // エラーチェック 移動回数と移動方向の文字数が一致しているか
        if(moveCount != moveDirection.Length)
        {
            Console.WriteLine("移動回数が不正です");
            return;
        }
        // エラーチェック 移動方向が正しいか
        bool directionCheck = moveDirection.Any(c => !allowedDirection.Contains(c));
        if(directionCheck)
        {
            Console.WriteLine("移動方向が不正です");
            return;
        }
        
        // 座席ごとのチョコレート個数を格納
        int[][] chocolateNums = new int[numRows][];
        for(int i = 0;i < numRows; i++)
        {
            chocolateNums[i] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse); 
        }
        
        int currentSeatX = seatX - 1;
        int currentSeatY = seatY - 1;
        int currentChocolateNum = chocolateNums[currentSeatX][currentSeatY];
        
        // 移動先のチョコレートの数を出力
        for(int move = 0; move < moveCount; move++)
        {
            // エラーチェック 座席の無い場所に移動していないか
            if((currentSeatY == 0 && moveDirection[move] == 'L') ||
               (currentSeatY == numCols - 1 && moveDirection[move] == 'R') ||
               (currentSeatX == 0 && moveDirection[move] == 'F') ||
               (currentSeatX == numRows - 1 && moveDirection[move] == 'B'))
               {
                   Console.WriteLine("座席の無い場所には移動できません");
                   return;
               }
            
            // 席を移動し、チョコレートの個数を出力する
            switch(moveDirection[move])
            {
                case 'F':
                    currentSeatX -= MoveNum;
                    break;
                case 'B':
                    currentSeatX += MoveNum;
                    break;
                case 'L':
                    currentSeatY -= MoveNum;
                    break;
                case 'R':
                    currentSeatY += MoveNum;
                    break;
            }
            
            Console.WriteLine(chocolateNums[currentSeatX][currentSeatY]);
        }
    }
}