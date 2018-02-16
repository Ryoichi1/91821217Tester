using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace _91821217Tester
{
    public static class TestPowerSupplyVol
    {
        public enum VOL_CH
        {
            P12V, P5V, P3_3V, M5V,
        }

        public static bool SetRelay(VOL_CH ch)
        {
            var cmd = "";
            switch (ch)
            {
                case VOL_CH.P12V:
                    cmd = "VCK01";
                    break;
                case VOL_CH.P5V:
                    cmd = "VCK02";
                    break;
                case VOL_CH.P3_3V:
                    cmd = "VCK03";
                    break;
                case VOL_CH.M5V:
                    cmd = "VCK04";
                    break;
            }
            Sleep(100);
            return Target.SendData(cmd);
        }

        public static async Task<bool> CheckVolt(VOL_CH ch)
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
                        SetRelay(ch);
                        Thread.Sleep(400);

                        switch (ch)
                        {
                            case VOL_CH.P12V:
                                Max = State.TestSpec.V12_HI;
                                Min = State.TestSpec.V12_LO;
                                break;
                            case VOL_CH.P5V:
                                Max = State.TestSpec.Vp5_HI;
                                Min = State.TestSpec.Vp5_LO;
                                break;
                            case VOL_CH.P3_3V:
                                Max = State.TestSpec.V3_3_HI;
                                Min = State.TestSpec.V3_3_LO;
                                break;
                            case VOL_CH.M5V:
                                Max = State.TestSpec.Vm5_HI;
                                Min = State.TestSpec.Vm5_LO;
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
                            Thread.Sleep(200);
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
                    case VOL_CH.P12V:
                        State.VmTestResults.Vol12v = measData.ToString("F2") + "V";
                        State.VmTestResults.ColVol12v = result ? General.OffBrush : General.NgBrush;
                        break;
                    case VOL_CH.P5V:
                        State.VmTestResults.VolP5v = measData.ToString("F2") + "V";
                        State.VmTestResults.ColVolP5v = result ? General.OffBrush : General.NgBrush;
                        break;
                    case VOL_CH.P3_3V:
                        State.VmTestResults.VolP33v = measData.ToString("F2") + "V";
                        State.VmTestResults.ColVolP33v = result ? General.OffBrush : General.NgBrush;
                        break;
                    case VOL_CH.M5V:
                        State.VmTestResults.VolM5v = measData.ToString("F2") + "V";
                        State.VmTestResults.ColVolM5v = result ? General.OffBrush : General.NgBrush;
                        break;

                }

                //NGだった場合、エラー詳細情報の規格値を更新する
                if (!result)
                {
                    //switch (ch)
                    //{
                    //    case VOL_CH.P12V:
                    //        State.VmTestStatus.Spec = $"規格値 : {State.TestSpec.V12_LO.ToString("F2")} ～ {State.TestSpec.V12_HI.ToString("F2")}V";
                    //        break;
                    //    case VOL_CH.P5V:
                    //        State.VmTestStatus.Spec = $"規格値 : {State.TestSpec.Vp5_LO.ToString("F2")} ～ {State.TestSpec.Vp5_HI.ToString("F2")}V";
                    //        break;
                    //    case VOL_CH.P3_3V:
                    //        State.VmTestStatus.Spec = $"規格値 : {State.TestSpec.V3_3_LO.ToString("F2")} ～ {State.TestSpec.V3_3_HI.ToString("F2")}V";
                    //        break;
                    //    case VOL_CH.M5V:
                    //        State.VmTestStatus.Spec = $"規格値 : {State.TestSpec.Vm5_LO.ToString("F2")} ～ {State.TestSpec.Vm5_HI.ToString("F2")}V";
                    //        break;
                    //}

                    State.VmTestStatus.Spec      = $"規格値 : {Min.ToString("F2")} ～ {Max.ToString("F2")}V";
                    State.VmTestStatus.MeasValue = "計測値 : " + measData.ToString("F2") + "V";

                }


            }
        }

















    }
}
