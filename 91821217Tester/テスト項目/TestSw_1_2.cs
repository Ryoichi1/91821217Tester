using System.Threading.Tasks;
using static System.Threading.Thread;

namespace _91821217Tester
{
    public static class TestSw_1_2
    {
        public enum SW_NAME
        {
            S1, S2,
        }

        public static bool CheckSw(SW_NAME name)
        {
            var IsChecking = true;
            try
            {
                void Check()
                {
                    Task.Run(() =>
                    {
                        while (IsChecking)
                        {
                            Target.SendData(name == SW_NAME.S1 ? "SSW01" : "SSW02", DoAnalysis: false);
                        }
                    });
                }

                Check();
                var mess = name == SW_NAME.S1 ? "S1（みぎ）を押すと、LED2（赤）が点灯する？？" : "S2（ひだり）を押すと、LED3（黄）が点灯する？？";
                var dialog = new DialogPic($"{mess}\r\n何回押してもいいよ！", DialogPic.NAME.その他);
                dialog.ShowDialog();
                IsChecking = false;
                return Flags.DialogReturn;
            }
            catch
            {
                return false;
            }
            finally
            {
                //いったん電源を落とす そのままS2のチェックに行くとS2が反応しないため ※うめきバグ
                General.PowSupply(false);
                Sleep(200);
            }
        }

    }
}
