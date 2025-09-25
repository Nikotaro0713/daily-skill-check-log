コードと問題文を確認しました。
このコードは N-しりとり の判定をしようとしていますが、バグが1つ明確にあります。

❗バグの内容
static bool CheckShiritori(string[] array,int takeCount)
{
    for(int i = 1; i < takeCount; i++)
    {
        if(array[i - 1].Substring(array[i - 1].Length - takeCount) != array[i].Substring(0,takeCount))
        {
            return false;
        }
    }
    
    return true;
}


このループ条件：

for(int i = 1; i < takeCount; i++)


が間違っています。

🔍 問題の読み解き

入力形式は以下のようになっているはずです：

N M
word1 word2 ... wordM


N：しりとりで使う「末尾と先頭の文字数」

M：単語の数（≠ N）

❌ 現在のコードの誤解
var input = ReadIntArray();
int takeCount = input[0]; // ← N


このあと、

string[] shiritoriArray = ReadStringArray();


ここで shiritoriArray.Length == M になるはずです。

しかし、CheckShiritori メソッド内で

for(int i = 1; i < takeCount; i++) // ← i < N になっている


とやってしまっているので、「M 回のチェックが必要なのに、最大で N-1 回しかチェックしていない」ことになります。

✅ 修正案

CheckShiritori のループは「M 個の単語のうち、2個目以降の単語と、直前の単語を比較」するので、次のように修正する必要があります。

修正後のコード：
static bool CheckShiritori(string[] array,int n)
{
    for(int i = 1; i < array.Length; i++)
    {
        string prev = array[i - 1];
        string current = array[i];

        // 単語の長さが n 未満の場合のチェック（防御的プログラミング）
        if (prev.Length < n || current.Length < n)
        {
            return false;
        }

        string prevEnd = prev.Substring(prev.Length - n);
        string currStart = current.Substring(0, n);

        if(prevEnd != currStart)
        {
            return false;
        }
    }
    
    return true;
}

🧪 テストケース例（バグが起こるパターン）

例えば、以下のような入力：

2 4
しりとり りんご ごりら らっぱ


これを N=2 の N-しりとりで判定すると：

「しりとり」→末尾「とり」

「りんご」→先頭「りん」
→ 「とり」≠「りん」 → ❌

正しく NO と判定される必要がありますが、今のコードでは i < 2（N=2）までしか比較しないため、比較が漏れてしまうケースがあり、不正解になります。

✅ 最終修正まとめ

以下のように Main と CheckShiritori の関係を整理しましょう：

static void Main()
{
    var input = ReadIntArray();
    int n = input[0];
    int m = input[1];

    string[] words = ReadStringArray();

    if (CheckShiritori(words, n))
    {
        Console.WriteLine("YES");
    }
    else
    {
        Console.WriteLine("NO");
    }
}

✅ 提案（任意）

ReadIntArray() と ReadStringArray() のように分けて処理するのは読みやすくて◎です。

エラーチェック（単語の長さが N 未満）を加えるとさらに堅牢になります。

✅ 修正後のコード全体（完全版）
using System;

class Program
{
    static void Main()
    {
        var input = ReadIntArray();
        int n = input[0];
        int m = input[1];

        string[] words = ReadStringArray();

        if (CheckShiritori(words, n))
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }

    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    }

    static string[] ReadStringArray()
    {
        return Console.ReadLine().Split();
    }

    static bool CheckShiritori(string[] array, int n)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1].Length < n || array[i].Length < n)
            {
                return false;
            }

            string prevEnd = array[i - 1].Substring(array[i - 1].Length - n);
            string currStart = array[i].Substring(0, n);

            if (prevEnd != currStart)
            {
                return false;
            }
        }

        return true;
    }
}


何か他のケースでもテストしたい場合は、ぜひ入力例を教えてください。テストケース設計のアドバイスもできます。

あなた:
修正後のコード
using System;
class Program
{
    static void Main()
    {
        var input = ReadIntArray();
        int takeCount = input[0];
        
        string[] shiritoriArray = ReadStringArray();
        
        if(CheckShiritori(shiritoriArray,takeCount))
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }
    
    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    }
    
    static string[] ReadStringArray()
    {
        return Console.ReadLine().Split();
    }
    
    // しりとりが正常に行われているか判定する
    static bool CheckShiritori(string[] array,int takeCount)
    {
        for(int i = 1; i < array.Length; i++)
        {
            if(array[i - 1].Substring(array[i - 1].Length - takeCount) != array[i].Substring(0,takeCount))
            {
                return false;
            }
        }
        
        return true;
    }
}

これで全問正解になったが、takeCountというわかりにくい変数名にしているためループ条件を誤ったと思う。
ChatGPT:

おっしゃる通りで、最初のコードで takeCount という変数名を使っていたことで、「ループ回数 = takeCount」と誤解してしまったのが、バグの根本原因でした。

✅ 指摘の通り：takeCount は意味が曖昧
なぜ問題か？

takeCount は英語的には「取得数」や「取り出す数」といった意味で、配列の長さや処理回数を連想させやすい名前です。

実際には takeCount は 「しりとりに使う文字数（N）」 であり、ループ回数とは関係がありません。

それにより、for (int i = 1; i < takeCount; i++) という ロジックのミスを誘発してしまった。

✅ 改善案：より適切な変数名にする

変数名を意味が明確なものにすれば、同様のバグを防ぎやすくなります。

改善前：
int takeCount = input[0];

改善後：
int n = input[0]; // N-しりとりのN


あるいは：

int matchLength = input[0]; // 最後・最初にマッチさせる文字数

✅ リファクタリング例（変数名のみ整理）
using System;

class Program
{
    static void Main()
    {
        var input = ReadIntArray();
        int n = input[0]; // しりとりで使う文字数
        
        string[] words = ReadStringArray();
        
        if(CheckShiritori(words, n))
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }

    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    }

    static string[] ReadStringArray()
    {
        return Console.ReadLine().Split();
    }

    // N-しりとりのルールを判定する
    static bool CheckShiritori(string[] words, int n)
    {
        for(int i = 1; i < words.Length; i++)
        {
            string prevEnd = words[i - 1].Substring(words[i - 1].Length - n);
            string currStart = words[i].Substring(0, n);
            
            if(prevEnd != currStart)
            {
                return false;
            }
        }
        
        return true;
    }
}

✅ 補足：読みやすいコードを書くためのポイント
ポイント	説明
変数名に意味を持たせる	n, matchLength, shiritoriWords など、役割を明確に
インデックスを扱うループには特に注意	array.Length をループ条件に使うことでミスを防ぎやすい
コメントは補助的に使う	正しい変数名があると、コメントは最小限で済む
「何の数」なのか？を意識する	count, length, index, size の使い分け
💬 まとめ

takeCount は誤解を招く命名でした（「取り出す回数・数」と思いやすい）。

より明確な名前 (n や matchLength) にすることで、コードの意図が明確になり、バグが起きにくくなります。

コードの動作は正しくなったので、今後はこうした命名の工夫で、さらに良いコードになります。