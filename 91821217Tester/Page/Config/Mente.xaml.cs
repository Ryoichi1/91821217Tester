using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static _91821217Tester.General;

namespace _91821217Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class Mente
    {
        private SolidColorBrush ButtonOnBrush = new SolidColorBrush();
        private SolidColorBrush ButtonOffBrush = new SolidColorBrush();
        private const double ButtonOpacity = 0.4;

        public Mente()
        {
            InitializeComponent();

            ButtonOnBrush.Color = Colors.DodgerBlue;
            ButtonOffBrush.Color = Colors.Transparent;
            ButtonOnBrush.Opacity = ButtonOpacity;
            ButtonOffBrush.Opacity = ButtonOpacity;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            buttonPow.Background = Brushes.Transparent;
            General.PowSupply(false);

            if (!FlagConnect)
            {
                Flags.StateTarget = Target.InitPort(State.Setting.ComTarget);
            }
        }


        bool FlagPow;
        private void buttonPow_Click(object sender, RoutedEventArgs e)
        {
            if (FlagPow)
            {
                General.PowSupply(false);
                buttonPow.Background = ButtonOffBrush;
            }
            else
            {
                General.PowSupply(true);
                buttonPow.Background = ButtonOnBrush;
            }

            FlagPow = !FlagPow;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            State.VmComm.TX = "";
            State.VmComm.RX = "";
        }

        bool FlagConnect = true;
        private void buttonComDisconnect_Click(object sender, RoutedEventArgs e)
        {
            FlagConnect = !FlagConnect;

            if (FlagConnect)
            {
                Flags.StateTarget = Target.InitPort(State.Setting.ComTarget);
                buttonComDisconnect.Content = "ターゲット通信切断";
            }
            else
            {
                Target.Close();
                buttonComDisconnect.Content = "ターゲット通信接続";
            }


        }




    }

}
