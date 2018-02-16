using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;


namespace _91821217Tester
{
    public class ViewModelTestResult : BindableBase
    {
        //電源電圧
        private string _Vol12v;
        public string Vol12v { get { return _Vol12v; } set { SetProperty(ref _Vol12v, value); } }

        private Brush _ColVol12v;
        public Brush ColVol12v { get { return _ColVol12v; } set { SetProperty(ref _ColVol12v, value); } }

        private string _VolP5v;
        public string VolP5v { get { return _VolP5v; } set { SetProperty(ref _VolP5v, value); } }

        private Brush _ColVolP5v;
        public Brush ColVolP5v { get { return _ColVolP5v; } set { SetProperty(ref _ColVolP5v, value); } }

        private string _VolP33v;
        public string VolP33v { get { return _VolP33v; } set { SetProperty(ref _VolP33v, value); } }

        private Brush _ColVolP33v;
        public Brush ColVolP33v { get { return _ColVolP33v; } set { SetProperty(ref _ColVolP33v, value); } }

        private string _VolM5v;
        public string VolM5v { get { return _VolM5v; } set { SetProperty(ref _VolM5v, value); } }

        private Brush _ColVolM5v;
        public Brush ColVolM5v { get { return _ColVolM5v; } set { SetProperty(ref _ColVolM5v, value); } }


        //PWM出力 電流
        private string _VolPwm1_I_50;
        public string VolPwm1_I_50 { get { return _VolPwm1_I_50; } set { SetProperty(ref _VolPwm1_I_50, value); } }

        private Brush _ColPwm1_I_50;
        public Brush ColPwm1_I_50 { get { return _ColPwm1_I_50; } set { SetProperty(ref _ColPwm1_I_50, value); } }

        private string _VolPwm1_I_100;
        public string VolPwm1_I_100 { get { return _VolPwm1_I_100; } set { SetProperty(ref _VolPwm1_I_100, value); } }

        private Brush _ColPwm1_I_100;
        public Brush ColPwm1_I_100 { get { return _ColPwm1_I_100; } set { SetProperty(ref _ColPwm1_I_100, value); } }


        private string _VolPwm2_I_50;
        public string VolPwm2_I_50 { get { return _VolPwm2_I_50; } set { SetProperty(ref _VolPwm2_I_50, value); } }

        private Brush _ColPwm2_I_50;
        public Brush ColPwm2_I_50 { get { return _ColPwm2_I_50; } set { SetProperty(ref _ColPwm2_I_50, value); } }

        private string _VolPwm2_I_100;
        public string VolPwm2_I_100 { get { return _VolPwm2_I_100; } set { SetProperty(ref _VolPwm2_I_100, value); } }

        private Brush _ColPwm2_I_100;
        public Brush ColPwm2_I_100 { get { return _ColPwm2_I_100; } set { SetProperty(ref _ColPwm2_I_100, value); } }

        //PWM出力 電圧
        private string _VolPwm1_V_50;
        public string VolPwm1_V_50 { get { return _VolPwm1_V_50; } set { SetProperty(ref _VolPwm1_V_50, value); } }

        private Brush _ColPwm1_V_50;
        public Brush ColPwm1_V_50 { get { return _ColPwm1_V_50; } set { SetProperty(ref _ColPwm1_V_50, value); } }

        private string _VolPwm1_V_100;
        public string VolPwm1_V_100 { get { return _VolPwm1_V_100; } set { SetProperty(ref _VolPwm1_V_100, value); } }

        private Brush _ColPwm1_V_100;
        public Brush ColPwm1_V_100 { get { return _ColPwm1_V_100; } set { SetProperty(ref _ColPwm1_V_100, value); } }


        private string _VolPwm2_V_50;
        public string VolPwm2_V_50 { get { return _VolPwm2_V_50; } set { SetProperty(ref _VolPwm2_V_50, value); } }

        private Brush _ColPwm2_V_50;
        public Brush ColPwm2_V_50 { get { return _ColPwm2_V_50; } set { SetProperty(ref _ColPwm2_V_50, value); } }

        private string _VolPwm2_V_100;
        public string VolPwm2_V_100 { get { return _VolPwm2_V_100; } set { SetProperty(ref _VolPwm2_V_100, value); } }

        private Brush _ColPwm2_V_100;
        public Brush ColPwm2_V_100 { get { return _ColPwm2_V_100; } set { SetProperty(ref _ColPwm2_V_100, value); } }


    }

}








