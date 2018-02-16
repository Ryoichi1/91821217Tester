using System;
using System.Threading.Tasks;
using static System.Threading.Thread;
using static _91821217Tester.General;

namespace _91821217Tester
{
    public static class TestDipSw
    {
        public enum SW1_BIT
        {
            B1, B2, B3, B4, 
        }

        public static async Task<bool> CheckSw1()
        {
            var result = false;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    PlaySound(soundNotice);
                    foreach (var n in Enum.GetValues(typeof(SW1_BIT)))
                    {
                        var mess = $"DSW1の {((SW1_BIT)n).ToString()} のみをONしてください！";
                        State.VmTestStatus.Message = mess;

                        var cmd = "";
                        switch ((SW1_BIT)n)
                        {
                            case SW1_BIT.B1:
                                cmd = "DSW01";
                                break;
                            case SW1_BIT.B2:
                                cmd = "DSW02";
                                break;
                            case SW1_BIT.B3:
                                cmd = "DSW03";
                                break;
                            case SW1_BIT.B4:
                                cmd = "DSW04";
                                break;
                        }

                        while (true)
                        {
                            if (Flags.ClickStopButton)
                                return false;

                            if (Target.SendData(cmd))
                            {
                                PlaySound2(soundSwCheck);
                                break;
                            }
                            Sleep(250);
                        }
                    }

                    return true;
                });

            }
            catch
            {
                return false;
            }
            finally
            {
                State.VmTestStatus.Message = Constants.MessWait;
            }

        }


    }
}
