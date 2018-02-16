using System.Windows.Media;

namespace _91821217Tester
{
    public static class Flags
    {
        public static bool OtherPage { get; set; }

        //試験開始時に初期化が必要なフラグ
        public static bool StopInit周辺機器 { get; set; }
        public static bool Initializing周辺機器 { get; set; }
        public static bool EnableTestStart { get; set; }
        public static bool Testing { get; set; }
        public static bool PowOn { get; set; }//メイン電源ON/OFF
        public static bool ShowErrInfo { get; set; }
        public static bool AddDecision { get; set; }

        public static bool DialogReturn { get; set; }

        public static bool ClickStopButton { get; set; }
        public static bool Click確認Button { get; set; }

        public static bool PressOpenCheckBeforeTest { get; set; }


        private static SolidColorBrush RetryPanelBrush = new SolidColorBrush();
        private static SolidColorBrush StatePanelOkBrush = new SolidColorBrush();
        private static SolidColorBrush StatePanelNgBrush = new SolidColorBrush();
        private const double StatePanelOpacity = 0.3;

        static Flags()//コンストラクタ
        {
            RetryPanelBrush.Color = Colors.DodgerBlue;
            RetryPanelBrush.Opacity = StatePanelOpacity;

            StatePanelOkBrush.Color = Colors.DodgerBlue;
            StatePanelOkBrush.Opacity = StatePanelOpacity;

            StatePanelNgBrush.Color = Colors.DeepPink;
            StatePanelNgBrush.Opacity = StatePanelOpacity;
        }

        //例外ステータス
        private static bool _throwException;
        public static bool ThrowException
        {
            get { return _throwException; }
            set
            {
                _throwException = value;
                State.VmTestStatus.ColorTarget = value ? StatePanelNgBrush : Brushes.Transparent;
            }
        }

        //周辺機器ステータス
        private static bool _StateTarget;
        public static bool StateTarget
        {
            get { return _StateTarget; }
            set
            {
                _StateTarget = value;
                State.VmTestStatus.ColorTarget = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        private static bool _State34401;
        public static bool State34401
        {
            get { return _State34401; }
            set
            {
                _State34401 = value;
                State.VmTestStatus.Color34401 = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        //周辺機器ステータス
        private static bool _StateIo;
        public static bool StateIo
        {
            get { return _StateIo; }
            set
            {
                _StateIo = value;
                State.VmTestStatus.ColorIo = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }


        private static bool _Retry;
        public static bool Retry
        {
            get { return _Retry; }
            set
            {
                _Retry = value;
                State.VmTestStatus.RetryLabelVis = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
        }


        public static bool AllOk周辺機器接続 { get; set; }

        //フラグ
        private static bool _SetOperator;
        public static bool SetOperator
        {
            get { return _SetOperator; }
            set
            {
                _SetOperator = value;
                if (value)
                {
                    if (State.VmMainWindow.Operator == "畔上" || State.VmMainWindow.Operator == "畔上2")
                    {
                        State.VmTestStatus.UnitTestEnable = true;
                    }
                    else
                    {
                        State.VmTestStatus.UnitTestEnable = false;
                        State.VmTestStatus.CheckUnitTest = false;
                    }
                }
                else
                {
                    State.VmMainWindow.Operator = "";
                    State.VmTestStatus.UnitTestEnable = false;
                    State.VmTestStatus.CheckUnitTest = false;
                    State.VmMainWindow.SelectIndex = -1;


                }
            }
        }


        private static bool _SetOpecode;
        public static bool SetOpecode
        {
            get { return _SetOpecode; }

            set
            {
                _SetOpecode = value;

                if (value)
                {
                    State.VmMainWindow.ReadOnlyOpecode = true;
                }
                else
                {
                    State.VmMainWindow.ReadOnlyOpecode = false;
                    State.VmMainWindow.Opecode = "";
                }

            }
        }



    }
}
