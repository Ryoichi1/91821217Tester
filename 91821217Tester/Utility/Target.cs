using System.IO.Ports;
using System.Threading;
using System.Linq;

namespace _91821217Tester
{
    public static class Target
    {

        private static SerialPort port;

        public static string RecieveData { get; private set; }//披検査基板から受信した生データ

        //コンストラクタ
        static Target()
        {
            port = new SerialPort();
        }

        public static bool InitPort(string comName)
        {
            try
            {
                //Comポートの取得
                if (!port.IsOpen)
                {
                    //シリアルポート設定
                    port.PortName = comName;
                    port.BaudRate = 9600;
                    port.DataBits = 8;
                    port.Parity = System.IO.Ports.Parity.None;
                    port.StopBits = System.IO.Ports.StopBits.One;
                    port.RtsEnable = true;
                    port.NewLine = "\r\n";
                    port.ReadTimeout = 500;
                    //ポートオープン
                    port.Open();
                }
                return true;

            }
            catch
            {
                if (port.IsOpen) port.Close();
                return false;
            }
        }

        public static void Close()
        {
            if (port.IsOpen) port.Close();
        }


        //**************************************************************************
        //ターゲットにコマンドを送る
        //**************************************************************************
        public static bool SendData(string Data, int Wait = 3000, bool setLog = true, bool DoAnalysis = true)
        {
            //送信処理
            try
            {
                State.VmComm.TX_Target = "";
                State.VmComm.RX_Target = "";

                ClearBuff();//受信バッファのクリア

                port.WriteLine(Data);
                if (setLog)
                    State.VmComm.TX_Target = Data;

            }
            catch
            {
                State.VmComm.TX_Target = "TX_Error";
                return false;
            }

            //受信処理
            try
            {
                RecieveData = "";//初期化
                port.ReadTimeout = Wait;
                var RxData = port.ReadLine();

                if (!DoAnalysis)
                {
                    RecieveData = RxData;
                    return true;
                }
                return AnalysisData(RxData, setLog);
            }
            catch
            {
                if (setLog) State.VmComm.RX_Target = "TimeoutErr";
                return false;
            }
            finally
            {
                //Thread.Sleep(300);
            }
        }

        private static bool AnalysisData(string data, bool setLog = true)
        {
            bool result = false;

            try
            {
                RecieveData = data;
                return result = data.EndsWith("OK");
            }
            catch
            {
                RecieveData = "Error例外";
                return result = false;
            }
            finally
            {
                if (!result)
                {   //TODO：
                    //ラベルの色を赤くするなどの処理を追加する
                }
                if (setLog) State.VmComm.RX_Target = RecieveData; Thread.Sleep(40);
            }
        }

        public static bool SendDataForVerCheck(string ver)
        {
            //送信処理
            try
            {
                State.VmComm.TX_Target = "";
                State.VmComm.RX_Target = "";

                ClearBuff();//受信バッファのクリア

                var verCheckCmd = $"{(char)0x04}00SN{(char)0x05}";
                port.Write(verCheckCmd);
                State.VmComm.TX_Target = verCheckCmd;
            }
            catch
            {
                State.VmComm.TX_Target = "TX_Error";
                return false;
            }

            //受信処理
            try
            {
                RecieveData = "";//初期化
                port.ReadTimeout = 1000;
                RecieveData = port.ReadTo(((char)0x03).ToString());
                State.VmComm.RX_Target = RecieveData.Trim((char)0x02);//先頭のSTXを削除
                return RecieveData.Contains(ver);
            }
            catch
            {
                State.VmComm.RX_Target = "TimeoutErr";
                return false;
            }
        }

        public static bool SendForXportCheck(string data)
        {
            State.VmComm.TX_Target = "";
            State.VmComm.RX_Target = "";

            try
            {
                ClearBuff();//受信バッファのクリア

                port.WriteLine(data);
                State.VmComm.TX_Target = data;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ReadForXPortCheck()
        {
            //受信処理
            try
            {
                RecieveData = "";//初期化
                port.ReadTimeout = 2000;
                var RxData = port.ReadLine();

                return AnalysisData(RxData);
            }
            catch
            {
                State.VmComm.RX_Target = "TimeoutErr";
                return false;
            }
            finally
            {
                //Thread.Sleep(300);
            }
        }

        //**************************************************************************
        //受信バッファをクリアする
        //**************************************************************************
        private static void ClearBuff()
        {
            if (port.IsOpen)
                port.DiscardInBuffer();
        }
    }
}
