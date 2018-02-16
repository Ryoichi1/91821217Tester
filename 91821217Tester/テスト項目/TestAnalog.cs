using System;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace _91821217Tester
{
    public static class TestAnalog
    {
        public enum OUTPUT_NAME
        {
            PWM1_I_50, PWM1_I_100, PWM2_I_50, PWM2_I_100,
            PWM1_V_50, PWM1_V_100, PWM2_V_50, PWM2_V_100,
        }

        public static string SetRelay(OUTPUT_NAME ch)
        {
            var cmd = "";
            switch (ch)
            {
                case OUTPUT_NAME.PWM1_I_50:
                    cmd = "VCK07";
                    break;
                case OUTPUT_NAME.PWM1_I_100:
                    cmd = "VCK08";
                    break;
                case OUTPUT_NAME.PWM2_I_50:
                    cmd = "VCK11";
                    break;
                case OUTPUT_NAME.PWM2_I_100:
                    cmd = "VCK12";
                    break;
                case OUTPUT_NAME.PWM1_V_50:
                    cmd = "VCK05";
                    break;
                case OUTPUT_NAME.PWM1_V_100:
                    cmd = "VCK06";
                    break;
                case OUTPUT_NAME.PWM2_V_50:
                    cmd = "VCK09";
                    break;
                case OUTPUT_NAME.PWM2_V_100:
                    cmd = "VCK10";
                    break;

            }

            return cmd;
        }

        public static async Task<bool> CheckPwmOut(OUTPUT_NAME ch)
        {
            bool result = false;
            Double measData = 0;
            double Max = 0;
            double Min = 0;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    try
                    {
                        //製品にコマンドを送ると、PWM出力させ、マルチメータに出力端子を接続します
                        if (!Target.SendData(SetRelay(ch), Wait: 10000)) return false;

                        switch (ch)
                        {
                            case OUTPUT_NAME.PWM1_I_50:
                            case OUTPUT_NAME.PWM2_I_50:
                                Max = State.TestSpec.PWM_I50_HI;
                                Min = State.TestSpec.PWM_I50_LO;
                                break;
                            case OUTPUT_NAME.PWM1_I_100:
                            case OUTPUT_NAME.PWM2_I_100:
                                Max = State.TestSpec.PWM_I100_HI;
                                Min = State.TestSpec.PWM_I100_LO;
                                break;
                            case OUTPUT_NAME.PWM1_V_50:
                            case OUTPUT_NAME.PWM2_V_50:
                                Max = State.TestSpec.PWM_V50_HI;
                                Min = State.TestSpec.PWM_V50_LO;
                                break;
                            case OUTPUT_NAME.PWM1_V_100:
                            case OUTPUT_NAME.PWM2_V_100:
                                Max = State.TestSpec.PWM_V100_HI;
                                Min = State.TestSpec.PWM_V100_LO;
                                break;
                        }

                        var tm = new GeneralTimer(5000);
                        tm.start();
                        while (true)
                        {
                            if (tm.FlagTimeout) return false;
                            if (!General._34401.GetDcVoltage()) continue;

                            measData = General._34401.VoltData;
                            if (Min < measData && measData < Max)
                                break;
                            Sleep(200);
                        }

                        if (!General._34401.GetDcVoltage()) return false;
                        measData = General._34401.VoltData;

                        return result = (Min < measData && measData < Max);
                    }
                    catch
                    {
                        return result = false;
                    }

                });
            }
            finally
            {

                //ビューモデルの更新
                switch (ch)
                {
                    case OUTPUT_NAME.PWM1_I_50:
                        State.VmTestResults.VolPwm1_I_50 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm1_I_50 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM1_I_100:
                        State.VmTestResults.VolPwm1_I_100 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm1_I_100 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM2_I_50:
                        State.VmTestResults.VolPwm2_I_50 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm2_I_50 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM2_I_100:
                        State.VmTestResults.VolPwm2_I_100 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm2_I_100 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM1_V_50:
                        State.VmTestResults.VolPwm1_V_50 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm1_V_50 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM1_V_100:
                        State.VmTestResults.VolPwm1_V_100 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm1_V_100 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM2_V_50:
                        State.VmTestResults.VolPwm2_V_50 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm2_V_50 = result ? General.OffBrush : General.NgBrush;
                        break;
                    case OUTPUT_NAME.PWM2_V_100:
                        State.VmTestResults.VolPwm2_V_100 = measData.ToString("F2") + "V";
                        State.VmTestResults.ColPwm2_V_100 = result ? General.OffBrush : General.NgBrush;
                        break;

                }

                //NGだった場合、エラー詳細情報の規格値を更新する
                if (!result)
                {
                    State.VmTestStatus.Spec      = $"規格値 : {Min.ToString("F2")} ～ {Max.ToString("F2")}V";
                    State.VmTestStatus.MeasValue = "計測値 : " + measData.ToString("F2") + "V";

                }


            }
        }

















    }
}
