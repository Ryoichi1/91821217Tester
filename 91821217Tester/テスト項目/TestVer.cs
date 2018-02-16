using System.Threading.Tasks;
using static System.Threading.Thread;

namespace _91821217Tester
{
    public static class TestVer
    {
        public static async Task<bool> CheckVer()
        {
            var result = false;

            try
            {
                Sleep(1000);
                General.PowSupply(true);
                return result = await Task<bool>.Run(() =>
                {
                    Sleep(1000);
                    var tm = new GeneralTimer(5000);
                    tm.start();
                    while (true)
                    {
                        if (tm.FlagTimeout || Flags.ClickStopButton) return false;
                        if (Target.SendDataForVerCheck(State.TestSpec.Ver))
                            return true;
                        Sleep(100);
                    }
                });
            }
            catch
            {
                return false;
            }
            finally
            {
                General.PowSupply(false);
            }

        }

      

    }
}
