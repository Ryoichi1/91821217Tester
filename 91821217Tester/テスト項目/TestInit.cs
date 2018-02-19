using static System.Threading.Thread;

namespace _91821217Tester
{
    public static class TestInit
    {
        public static bool Init()
        {
            //            製品プログラムを書き込み後

            //１．検査中止ボタンを押す。
            //２．「１項目だけ検査する」をﾁｪｯｸして「ﾊﾞｰｼﾞｮﾝﾁｪｯｸ」を選択する。
            //３．試験機の電源を一度ＯＦＦにして、再度ＯＮにしてから「開始」をクリックする。
            //４．OKだったら初期化を行う。

            //－－－初期化方法－－－
            //　①”S2”スイッチを押したまま、電源を投入してください。
            //　②電源投入後”LED1”が点灯、”LED3”が点滅します。
            //　③”LED3”が点滅している状態で、”S1”スイッチを押します。
            //　④EEPROMの初期化されます。
            //－－－－－－－－－－－
            //！！！！注意！！！！
            //　　※③で”S1”スイッチ押してから11秒程度経過すると、表示操作延長基板は
            //　　　通信エラーと判定し”LED2”を点灯し、エラー履歴が記録されてしまいます。
            //　　　出荷時にエラー履歴を残すとNGなので、必ず10秒以内に確認を実施し電源
            //　　　OFFしてください。
            //　　　”LED2”が点灯した場合は、必ず①から作業のやり直しをお願い致します。
            //！！！！！！！！！！

            try
            {

                var dialog = new DialogPic("S2（ひだり側)を押したままOKボタンを押してね！", DialogPic.NAME.その他);
                dialog.ShowDialog();
                Sleep(400);
                General.PowSupply(true);

                dialog = new DialogPic("黄色LEDが点滅してるかな？？", DialogPic.NAME.その他);
                dialog.ShowDialog();
                if (!Flags.DialogReturn)
                    return false;

                bool FlagTimeOver = false;
                int countDown = 20; //30秒でエラーになるので、余裕を持って20秒以内に初期化を完了させる
                var tm = new System.Timers.Timer();
                tm.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
                {
                    countDown = countDown - 1;
                    State.VmTestStatus.DialogMess = $"S1（みぎ側）を押して、黄色が消えるのを確認してね\r\n※残り {countDown}秒";
                    if (countDown == 0)
                    {
                        tm.Stop();
                        FlagTimeOver = true;
                    }
                };
                tm.Interval = 1000;
                tm.Start();

                    dialog = new DialogPic("S1（みぎ側）を押して、黄色が消えるのを確認してね\r\n※残り 20秒", DialogPic.NAME.その他);
                    dialog.ShowDialog();
                    General.PowSupply(false);
                    return !FlagTimeOver;
                }
            catch
            {
                return false;
            }

        }



    }
}
