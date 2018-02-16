using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;

namespace _91821217Tester
{

    public class ViewModelCommunication : BindableBase
    {
        //LPC1768通信ログ
        private string _TX;
        public string TX
        {
            get { return _TX; }
            set { SetProperty(ref _TX, value); }
        }

        private string _RX;
        public string RX
        {
            get { return _RX; }
            set { SetProperty(ref _RX, value); }
        }

        //Target通信ログ
        private string _TX_Target;
        public string TX_Target
        {
            get { return _TX_Target; }
            set { SetProperty(ref _TX_Target, value); }
        }

        private string _RX_Target;
        public string RX_Target
        {
            get { return _RX_Target; }
            set { SetProperty(ref _RX_Target, value); }
        }


    }
}
