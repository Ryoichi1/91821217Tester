using System;
using static System.Threading.Thread;
using System.Threading.Tasks;

namespace _91821217Tester
{
    public static class 書き込み
    {
        public static async Task<bool> WriteFw(string path, string sum = null)
        {
            var result = false;
            try
            {
                //E8aからの電源供給では不安定で書き込みできないことがある
                //必ず試験機側から電源供給すること
                General.PowSupply(true);
                Sleep(500);
                return result = await FDT.WriteFirmware(path, sum);

            }
            catch
            {
                return false;
            }
            finally
            {
                General.PowSupply(false);
                await Task.Delay(1000);

                if (!result)
                {
                    State.VmTestStatus.Spec = $"規格値 : 0x{State.TestSpec.Sum}";
                    State.VmTestStatus.MeasValue = $"計測値 : 0x{FDT.ReadSumValue}";
                }
            }
        }



    }
}
