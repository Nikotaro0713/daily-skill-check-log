全体の評価

Linqをうまく使いこなしているので読みやすいです。

ビンゴの判定ロジックも正しく実装できています。

入力処理も整理されていて分かりやすいです。

ただし、やや変数名やメソッドに整理の余地があります。

処理をメソッドに分割するとさらに読みやすく保守性もアップします。

詳細レビュー
1. メソッド分割・命名

Mainメソッドがやや長く、役割が多いので以下のように分割すると良いです。

ReadInput() → 入力処理をまとめる

IsBingo() → ビンゴ判定ロジック（行・列・斜めの判定をまとめる）

CountBingo() → ビンゴ数のカウント

また変数名ももう少し意味を持たせると読みやすくなります。

int bingoSize = inputArray[0];
int drawCount = inputArray[1];
int[][] bingoCard;
int[] drawnNumbers;

2. パフォーマンス面の改善

drawnNums.Contains(num)が複数回呼ばれるので、HashSet<int>に変換して検索コストをO(1)にすると効率的です。

var drawnSet = new HashSet<int>(drawnNums);

3. コードのリファクタリング例
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var (bingoSize, drawCount, bingoCard, drawnNumbers) = ReadInput();

        var drawnSet = new HashSet<int>(drawnNumbers);

        int bingoCount = CountBingo(bingoCard, bingoSize, drawnSet);
        Console.WriteLine(bingoCount);
    }

    static (int bingoSize, int drawCount, int[][] bingoCard, int[] drawnNumbers) ReadInput()
    {
        int[] firstLine = ReadIntArray();
        int bingoSize = firstLine[0];
        int drawCount = firstLine[1];

        int[][] bingoCard = new int[bingoSize][];
        for (int i = 0; i < bingoSize; i++)
            bingoCard[i] = ReadIntArray();

        int[] drawnNumbers = ReadIntArray();

        return (bingoSize, drawCount, bingoCard, drawnNumbers);
    }

    static int CountBingo(int[][] bingoCard, int size, HashSet<int> drawnSet)
    {
        int bingoCount = 0;

        bool IsOpen(int num) => num == 0 || drawnSet.Contains(num);

        // 行のビンゴ判定
        bingoCount += bingoCard.Count(row => row.All(IsOpen));

        // 列のビンゴ判定
        bingoCount += Enumerable.Range(0, size)
            .Count(col => Enumerable.Range(0, size)
            .All(row => IsOpen(bingoCard[row][col])));

        // 斜め判定（左上→右下）
        if (Enumerable.Range(0, size).All(i => IsOpen(bingoCard[i][i])))
            bingoCount++;

        // 斜め判定（右上→左下）
        if (Enumerable.Range(0, size).All(i => IsOpen(bingoCard[i][size - 1 - i])))
            bingoCount++;

        return bingoCount;
    }

    static int[] ReadIntArray()
    {
        return Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
    }
}

まとめ

Linqを活かしたロジックは問題ないので自信を持って大丈夫です。

HashSetを使ってパフォーマンスを向上させましょう。

メソッド分割すると読みやすさ・保守性が格段にアップします。

変数名はできるだけ意味が伝わる名前に。

質問や補足あれば気軽にどうぞ！あなたの学びを応援しています。😊

あなた:
厳しめにレビューしてほしい
ChatGPT:

了解！じゃあ、ガッツリ厳しめにいくね。

厳しめコードレビュー
1. 命名の甘さ

inputArrayとかdrawnNumsとか雑すぎる。
変数名は「何を表しているか」「型ではなく意味が分かるもの」にしないと、読み手はコードの意図を探すのに時間がかかる。
→ 例えば bingoSize, drawCount, drawnNumbers のほうがずっとわかりやすい。

ReadIntArray()は汎用的すぎて何の配列を読んでるかわからない。
→ せめて ReadBingoCardRow()とかReadDrawNumbers()とか用途ごとにわけるか、引数で説明を入れるべき。

2. メソッドに分割していない

Main()に処理が全て詰め込まれてて読みにくい。
→ きちんと関心ごと（入力、判定、集計）で分けないと、バグの温床になるし修正も困難。
現時点では処理の羅列でしかない。

例えばビンゴ判定を別メソッドに切り出していないので、再利用性もテストもしにくい。

3. パフォーマンス無視の書き方

drawnNums.Contains(num)が配列に対して毎回呼ばれている。配列のContainsはO(n)の線形探索。
→ 何十回も使われているから計算量が爆発する可能性がある。
→ ここはHashSet<int>にしてO(1)検索にするのが基本。

4. リンキューの使い方が浅い・冗長

LINQを使っているとはいえ、冗長な書き方が多い。
例：Enumerable.Range(...).Where(...).Count()はCount(...)で済む。
→ こういう基本的な使い方のミスはLinq理解度の低さを露呈する。

「中央は0で開いてる」という条件を毎回num == 0 || drawnNums.Contains(num)と書いているが、これを関数に切り出していない。
→ 再利用性がなく、冗長。

5. コメントが役に立っていない

コメントは多いが、コードと同じことを言ってるだけで意味が薄い。
→ どうしてその処理が必要なのか、他者が見る上での意図を書くべき。
→ コメントは「なぜ」書くもので、「何をしているか」はコードで読み取れるレベルにすべき。

6. 入力チェックや例外処理がない

例えばNやKの範囲は？配列長と合っているか？数字の重複は？
→ 問題では保証されているかもしれないが、実務なら必ず検証コードを入れる。
→ こういう安全策を取らない癖があるとトラブルになる。

7. 可読性の低いネスト

LINQの中でさらにLINQを使う形になっていて、行・列判定のネストが深く、読みづらい。
→ 適宜一時変数に展開して分かりやすく書くべき。

総括

ただ書けてるだけ。

本質的なコードの読みやすさ、保守性、パフォーマンスの観点が完全に甘い。

LINQの使い方が理解不足のまま使っているだけ。

メソッド分割・命名・構造化が出来ていないので「きちんとしたコード」とは言えない。

これを改善しない限り、複雑な問題になると自力で拡張・修正できない。

厳しめだけど期待も込めて

ここまでできたのは素晴らしい出発点。
でも、仕事でコードを扱うなら今回のままでは通用しない。
「誰が見てもすぐ理解できるコード」を意識して書く練習をしないと、成長は止まるよ。

もし良ければ、改善版も作るから言ってね。