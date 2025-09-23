レビューありがとうございます！
全体的に読みやすくて、ロジックもしっかりしていると思います。
特に CountMatchingArrays の使い方や Enumerable.Range でのパターン照合は上手いですね。

以下、細かい点も含めてフィードバックしますね。

良い点

メソッド分割
入力読み込み (ReadInputArray、ReadStringArray) とパターン照合 (CountMatchingArrays) がちゃんと分かれていて見通しが良い

ロジックの明確さ
3×3のパターンを文字列配列で表現し、それを地上絵の2次元配列に重ね合わせて照合する方法は正しい

C#のLINQ活用
Enumerable.Range と SequenceEqual を使って比較しているのは簡潔で良い

変数名
heigthはheightのスペルミスがありますが、その他はわかりやすい名前を使えている

改善点
1. スペルミスの修正
int heigth = inputIntArray[0]; // → int height = inputIntArray[0];


スペルミスはコードの信用を下げるので注意。IDEの警告も活用しましょう。

2. string[][] ではなく char[][] にする

string[]で1文字ずつ配列にするより、charで直接扱った方が自然です。

char[][] geoglyph = new char[height][];
for (int i = 0; i < height; i++)
{
    geoglyph[i] = Console.ReadLine().ToCharArray();
}


これにより、ReadStringArray のメソッドも不要になりますし、コードもスッキリ。

3. メソッド名をより具体的に

CountMatchingArraysは何をマッチングしているかが分かりづらいです。
CountPatternOccurrencesやCountDonutPatternsなどにすると良いです。

4. CountMatchingArrays の中でのLINQの簡潔さと効率化

source[i + row].Skip(j).Take(patternCols).SequenceEqual(pattern[row])は読みやすいですが、パフォーマンス面を考えるとループを使う方が良いかもしれません（とはいえ、この問題サイズなら問題なし）。

もしパフォーマンスを気にしないなら現状維持でOK。

5. 変数名の統一と小さい改善
int height = inputIntArray[0];
int width = inputIntArray[1];


widthとheightは入れ替わっていないか確認を。問題文の通りであればOKです。

6. コメントの改善
// 第一引数に与えた文字列配列が、第二引数に与えた文字列配列に何個含まれているかカウントする


コメントはもう少し具体的に「パターンがソース内に何回現れるかを数える」と書くと分かりやすいです。

改善例コード
using System;

class Program
{
    static void Main()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int height = input[0];
        int width = input[1];

        char[][] geoglyph = new char[height][];
        for (int i = 0; i < height; i++)
        {
            geoglyph[i] = Console.ReadLine().ToCharArray();
        }

        char[][] donutPattern = new char[][]
        {
            new char[] {'#','#','#'},
            new char[] {'#','.','#'},
            new char[] {'#','#','#'}
        };

        int donutCount = CountPatternOccurrences(donutPattern, geoglyph);
        Console.WriteLine(donutCount);
    }

    // pattern が source に何回含まれるかカウントする
    static int CountPatternOccurrences(char[][] pattern, char[][] source)
    {
        int patternRows = pattern.Length;
        int patternCols = pattern[0].Length;
        int sourceRows = source.Length;
        int sourceCols = source[0].Length;

        int count = 0;

        for (int i = 0; i <= sourceRows - patternRows; i++)
        {
            for (int j = 0; j <= sourceCols - patternCols; j++)
            {
                bool match = true;
                for (int r = 0; r < patternRows; r++)
                {
                    for (int c = 0; c < patternCols; c++)
                    {
                        if (source[i + r][j + c] != pattern[r][c])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (!match) break;
                }

                if (match) count++;
            }
        }

        return count;
    }
}

まとめ

ロジックは問題なく合っているし、ちゃんと動きそう

char[][] にしてコードをシンプルにできる

変数名のスペルは直す

コメントをもう少しわかりやすく

メソッド名を目的に沿ったものにする