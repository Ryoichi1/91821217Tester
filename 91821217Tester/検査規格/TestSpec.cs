
namespace _91821217Tester
{
    public class TestSpec
    {
        //テストスペックVER
        public string TestSpecVer { get; set; }

        //製品FW
        public string Ver { get; set; }
        public string Sum { get; set; }

        //電源電圧
        public double V12_HI { get; set; }   // +12Vの上限の規格値
        public double V12_LO { get; set; }   // +12Vの下限の規格値
        public double Vp5_HI { get; set; }   // +5Vの上限の規格値
        public double Vp5_LO { get; set; }   // +5Vの下限の規格値
        public double V3_3_HI { get; set; }   // +3.3Vの上限の規格値
        public double V3_3_LO { get; set; }   // +3.3Vの下限の規格値
        public double Vm5_HI { get; set; }   // -5Vの上限の規格値
        public double Vm5_LO { get; set; }   // -5Vの下限の規格値

        //アナログ出力
        public double PWM_V50_HI { get; set; }   // PWM_V 50% の上限の規格値
        public double PWM_V50_LO { get; set; }   // PWM_V 50%の下限の規格値

        public double PWM_V100_HI { get; set; }   // PWM_V 100% の上限の規格値
        public double PWM_V100_LO { get; set; }   // PWM_V 100%の下限の規格値

        public double PWM_I50_HI { get; set; }   // PWM_I 50% の上限の規格値
        public double PWM_I50_LO { get; set; }   // PWM_I 50%の下限の規格値

        public double PWM_I100_HI { get; set; }   // PWM_I 100% の上限の規格値
        public double PWM_I100_LO { get; set; }   // PWM_I 100%の下限の規格値

    }
}
