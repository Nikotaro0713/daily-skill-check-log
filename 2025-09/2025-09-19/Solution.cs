using System;
class Program
{
    static void Main()
    {
        int peopleCount = ReadInput();
        int[] ageArray = CreateAegArray(peopleCount);
        int orderNum = ReadInput();
        int[][] beansArray = CreateBeansArray(orderNum);
        int[] sumBeans = new int[peopleCount];
        
        // 豆の数を計算する
        for(int i = 0; i < orderNum; i++)
        {
            var tmpBeans = new int[peopleCount];
            tmpBeans = CountBeans(sumBeans,beansArray[i][0],beansArray[i][1],beansArray[i][2]);
            sumBeans = tmpBeans;
        }
        
        // 年齢と比較して、最終的な豆の数を出力する
        for(int i = 0; i < peopleCount; i++)
        {
            if(sumBeans[i] <= ageArray[i])
            {
                Console.WriteLine(sumBeans[i]);
            }
            else
            {
                Console.WriteLine(ageArray[i]);
            }
        }
    }
    
    // コンソールから1行読み込み、数値に変換する
    static int ReadInput()
    {
        string input = Console.ReadLine();
        
        try
        {
            int number = int.Parse(input);
            return number;
        }
        catch (FormatException)
        {
            Console.WriteLine("エラー: 数字以外が入力されました。");
            throw;
        }
    }
    
    // コンソールから1行読み込み、int型配列に格納する
    static int[] ReadInputArray()
    {
        try
        {
            int[] intArray = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
            return intArray;
        }
        catch (FormatException)
        {
            Console.WriteLine("エラー: 数字以外が入力されました。");
            throw;
        }
    }
    
    // 年齢格納配列を作成する
    static int[] CreateAegArray(int peopleCount)
    {
        int[] ageArray = new int[peopleCount];
        for(int i = 0;i < peopleCount; i++)
        {
            ageArray[i] = ReadInput();
        }
        
        return ageArray;
    }
    
    // 二重配列を作成する
    static int[][] CreateBeansArray(int orderNum)
    {
        int[][] beansArray = new int[orderNum][];
        
        for(int i = 0; i < orderNum; i++)
        {
            beansArray[i] = ReadInputArray();
        }
        
        return beansArray;
    }
    
    // 豆の数を計算する
    static int[] CountBeans(int[] beansArray, int startIndex, int endIndex, int addBeansNum)
    {
        for(int i = startIndex; i <= endIndex; i++)
        {
            beansArray[i - 1] += addBeansNum;
        }
        
        return beansArray;
    }
}