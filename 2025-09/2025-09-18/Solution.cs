using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        string baseStr = consoleReadString();
        string targetStr = consoleReadString();
        
        string result = ConvertBaseStr(baseStr,targetStr);
        Console.WriteLine(result);
    }
    
    static string consoleReadString()
    {
        string input = Console.ReadLine();
        
        if(string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("入力内容が不正です");
            return "";
        }
        else
        {
            return input;
        }
    }
    
    static string ConvertBaseStr(string baseStr, string targetStr)
    {
        var baseDict = new Dictionary<char, char>();
        
        foreach (char c in baseStr)
        {
            // baseStrを大文字に統一してbaseDictに格納(baseStrを大文字にしたもの,元のbaseStr　の形)
            char upper = char.ToUpperInvariant(c);
            
            baseDict[upper] = c;
        }
        
        string result = new string(targetStr.Select(c => 
        {
            // targetStrをすべて大文字に変換
            char upper = char.ToUpperInvariant(c);
            
            // baseDictから取得したbaseChar（元のbaseStr）が大文字なら大文字、小文字なら小文字に変換
            if(baseDict.TryGetValue(upper, out char baseChar))
            {
                if(char.IsUpper(baseChar))
                    return char.ToUpperInvariant(c);    
                else
                    return char.ToLowerInvariant(c);
            }
            return c;
        }).ToArray());
        
        return result;
    }
}