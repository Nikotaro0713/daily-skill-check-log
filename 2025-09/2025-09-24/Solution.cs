using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        int studentsCount = ReadInt();
        int[] scoreArray = ReadIntArray(studentsCount);
        
        SortByRanking(scoreArray);
    }
    
    static int ReadInt()
    {
        return int.Parse(Console.ReadLine());
    }
    
    static int[] ReadIntArray(int num)
    {
        int[] array = new int[num];
        for(int i = 0;i < num;i++)
        {
            array[i] = ReadInt();
        }
        return array;
    }
    
    static void SortByRanking(int[] score)
    {
        int scoreCount = score.Length;
        var sortedScore = score.OrderByDescending(n => n).ToArray();
        Dictionary<int,int> rankDictionary = new Dictionary<int,int>();
        
        // 1位のスコアは最初に登録
        int rank = 1;
        rankDictionary[sortedScore[0]] = rank;
        rank++;
        // 2位以降のスコアを辞書に登録
        for(int i = 1;i < scoreCount; i++)
        {
            if(sortedScore[i] != sortedScore[i - 1])
            {
                rankDictionary[sortedScore[i]] = rank;
            }
            rank++;
        }

        for(int j = 0;j < scoreCount; j++)
        {
            if(rankDictionary.TryGetValue(score[j], out int value))
            {
                Console.WriteLine(value);
            }
        }
        
    }
}