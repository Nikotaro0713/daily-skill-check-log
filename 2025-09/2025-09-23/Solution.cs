using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] inputIntArray = ReadInputArray();
        int heigth = inputIntArray[0];
        int width = inputIntArray[1];
        
        string[][] geoglyph = new string[heigth][];
        for(int i = 0;i < heigth; i++)
        {
            geoglyph[i] = ReadStringArray();
        }
        
        string[][] donutArray = new string[][]
        {
            new string[] {"#","#","#"},
            new string[] {"#",".","#"},
            new string[] {"#","#","#"}
        };
        
        int donutCount = CountMatchingArrays(donutArray,geoglyph);
        Console.WriteLine(donutCount);
    }
    
    static int[] ReadInputArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    }
    
    static string[] ReadStringArray()
    {
        string input = Console.ReadLine();
        return input.Select(c => c.ToString()).ToArray();
    }
    
    // 第一引数に与えた文字列配列が、第二引数に与えた文字列配列に何個含まれているかカウントする
    static int CountMatchingArrays(string[][] pattern, string[][] source)
    {
        int matchCount = 0;
        int patternRows = pattern.Length;
        int patternCols = pattern[0].Length;
        int sourceRows = source.Length;
        int sourceCols = source[0].Length;
        
        for(int i = 0;i <= sourceRows - patternRows;i++)
        {
            for(int j = 0;j <= sourceCols - patternCols;j++)
            {
                bool match = Enumerable.Range(0,patternRows).All(row =>
                    source[i + row].Skip(j).Take(patternCols).SequenceEqual(pattern[row])
                );
                
                if(match) matchCount++;
            }
        }
        
        return matchCount;
    }
} 