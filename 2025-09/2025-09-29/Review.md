コードをレビューさせていただきますね。全体的に可読性が高く、メソッドを適切に分割している点が良い印象です。ただ、ロジックや命名、設計の観点でいくつか改善点がありますので、詳しくフィードバックしていきます。

✅ 良い点

メソッド分割：CalcCharge や CalcPoint を分けることで、処理が整理されていて良いです。

コメントの利用：最低限のコメントは入っており、意図もわかりやすいです。

変数名が直感的：currentCharge, fareArray など、役割が明確です。

❌ 改善点と提案
1. CalcPoint の責務が不明確でわかりにくい
static int CalcPoint(int point, int fare, int isSave)


isSave の意味がわかりにくく、0 と 1 のマジックナンバーも不親切です。

メソッドの名前も「ポイントを使う」場合と「貯める」場合の両方を扱っているため、責務が曖昧になっています。

🔧 改善案：

使う処理と貯める処理は別のメソッドに分けるのがベターです。

static int UsePoints(int point, int fare) => point - fare;

static int AddPoints(int point, int fare) => point + fare * 10 / 100;

2. CalcCharge のメソッドは冗長
static int CalcCharge(int charge, int fare) => charge - fare;


このレベルの単純な処理は、メソッド化するよりインラインで書いた方が読みやすいです。

🔧 改善案：削除して直接 currentCharge -= fare; と書く。

3. OutputChargeAndPoint の責務が多い

入出力と計算を一手に引き受けており、責務過多。

処理の切り分けが甘いため、テストや拡張性に弱い構造です。

🔧 改善案：ProcessFare のような処理ロジックだけの関数を作り、結果を出力する部分と分ける。

4. 命名の改善案

OutputChargeAndPoint → ProcessFaresAndPrintResults

CalcPoint → UpdatePoints

isSave → usePoints（bool型に変更）

5. 不要な初期変数
int currentCharge = charge;
int currentPoint = point;


呼び出し元でも currentCharge / currentPoint を使ってるため、名前を揃えるか、渡された変数名のまま使ってもOK。

✅ 改善後コード例
using System;

class Program
{
    static void Main()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int chargedAmount = input[0];
        int rideCount = input[1];

        int[] fareArray = new int[rideCount];
        for (int i = 0; i < rideCount; i++)
        {
            fareArray[i] = int.Parse(Console.ReadLine());
        }

        ProcessFaresAndPrintResults(chargedAmount, fareArray);
    }

    static void ProcessFaresAndPrintResults(int charge, int[] fares)
    {
        int currentCharge = charge;
        int currentPoints = 0;

        foreach (var fare in fares)
        {
            if (currentPoints >= fare)
            {
                currentPoints = UsePoints(currentPoints, fare);
            }
            else
            {
                currentCharge -= fare;
                currentPoints = AddPoints(currentPoints, fare);
            }

            Console.WriteLine($"{currentCharge} {currentPoints}");
        }
    }

    static int UsePoints(int points, int fare) => points - fare;

    static int AddPoints(int points, int fare) => points + (fare * 10 / 100);
}

⏱ 所要時間について

ロジックを間違えていたため、普段の倍ほど時間がかかってしまった

これは自然なことです。特に「ポイント優先で使う」「使った場合はポイントがつかない」など、条件分岐が複雑で間違いやすい問題です。

それでもメソッド分割に取り組んでいたのはとても良い姿勢です！ミスの発見もしやすくなります。

総評（10点満点）

可読性：7

設計（責務分割）：6

ロジック正確性：8（ロジックとしては正しく動作する）

拡張性：6

👉 総合：7 / 10