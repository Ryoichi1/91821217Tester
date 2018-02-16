using System;
using System.IO.Ports;
using static System.Threading.Thread;

namespace _91821217Tester
{

    public class Agilent34401A : Multimeter
    {

        //
        private const string ID_34401A = "HEWLETT-PACKARD,34401A";

        //変数の宣言（インスタンスメンバーになります）
        private SerialPort port;
        private string RecieveData;//34401Aから受信した生データ

        //変数の宣言（インスタンスメンバーになります）

        private double _VoltData;//計測したDC/AC電圧値
        public double VoltData
        {
            get { return _VoltData; }
        }

        private double _CurrData;//計測したDC/AC電流値
        public double CurrData
        {
            get { return _CurrData; }
        }

        private double _ResData;//計測した抵抗値
        public double ResData
        {
            get { return _ResData; }
        }

        private double _FreqData;//計測した周波数
        public double FreqData
        {
            get { return _FreqData; }
        }


        //コンストラクタ
        public Agilent34401A()
        {
            port = new SerialPort();
        }


        //**************************************************************************
        //34401Aの初期化
        //引数：なし
        //戻値：なし
        //**************************************************************************
        public bool Init(string comName)
        {
            bool result = false;

            try
            {
                if (!port.IsOpen)
                {
                    //Agilent34401A用のシリアルポート設定
                    port.PortName = comName; //この時点で既にポートが開いている場合COM番号は設定できず例外となる（イニシャライズは１回のみ有効）
                    port.BaudRate = 9600;
                    port.DataBits = 8;
                    port.Parity = System.IO.Ports.Parity.None;
                    port.StopBits = System.IO.Ports.StopBits.One;
                    port.DtrEnable = true;//これ設定しないとコマンド送るたびにErrorになります！
                    port.NewLine = ("\r\n");
                    port.Open();
                }
                //コマンド送信
                port.WriteLine(":SYST:REM");
                ReadRecieveData(1000);
                port.WriteLine("*CLS");
                ReadRecieveData(1000);
                port.WriteLine("*RST");
                ReadRecieveData(1000);

                //クエリ送信
                port.WriteLine("*IDN?");
                ReadRecieveData(1000);
                return result = RecieveData.Contains(ID_34401A);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (!result) 
                    ClosePort();
            }
        }

        public void SetRemote()
        {
            //コマンド送信
            port.WriteLine(":SYST:REM");
            ReadRecieveData(1000);
            port.WriteLine("*CLS");
            ReadRecieveData(1000);
            port.WriteLine("*RST");
            ReadRecieveData(1000);

        }

        //**************************************************************************
        //34401Aからの受信データを読み取る
        //引数：指定時間（ｍｓｅｃ）
        //戻値：ErrorCode
        //**************************************************************************
        private bool ReadRecieveData(int time)
        {

            RecieveData = null;//念のため初期化
            string buffer = null;
            port.ReadTimeout = time;
            try
            {
                buffer = port.ReadTo("\r\n");
            }
            catch (TimeoutException)
            {
                return false;
            }

            RecieveData = buffer;
            return true;
        }


        //**************************************************************************
        //抵抗値を取得する
        //引数：なし
        //戻値：bool
        //**************************************************************************
        public bool GetRes()
        {
            try
            {
                port.WriteLine(":MEAS:RES?");
                bool respons = ReadRecieveData(2000);
                if (!respons)
                {
                    return respons;//falseが返ります
                }

                _ResData = Double.Parse(RecieveData);

                return true;
            }
            catch
            {
                return false;

            }

        }

        //**************************************************************************
        //抵抗値を取得する
        //引数：なし
        //戻値：bool
        //**************************************************************************
        public bool GetFreq()
        {
            try
            {
                port.WriteLine(":MEAS:FREQ?");
                bool respons = ReadRecieveData(5000);
                if (!respons)
                {
                    return respons;//falseが返ります
                }

                _FreqData = Double.Parse(RecieveData);

                return true;
            }
            catch
            {
                return false;

            }

        }


        //**************************************************************************
        //直流電圧を取得する
        //引数：なし
        //戻値：bool
        //**************************************************************************
        public bool GetDcVoltage(bool sw = true)
        {
            try
            {
                port.WriteLine(":MEAS:VOLT:DC?");
                Sleep(500);

                bool respons = ReadRecieveData(1000);
                if (!respons)
                {
                    return respons;//falseが返ります
                }

                _VoltData = Double.Parse(RecieveData);

                return true;
            }
            catch
            {
                return false;

            }

        }

        //**************************************************************************
        //交流電圧を取得する
        //引数：なし
        //戻値：bool
        //**************************************************************************
        public bool GetAcVoltage()
        {
            try
            {
                port.WriteLine(":MEAS:VOLT:AC?");
                Sleep(500);

                bool respons = ReadRecieveData(1000);
                if (!respons)
                {
                    return respons;//falseが返ります
                }

                _VoltData = Double.Parse(RecieveData);

                return true;
            }
            catch
            {
                return false;

            }

        }

        //**************************************************************************
        //DC電流を取得する
        //引数：なし
        //戻値：bool
        //**************************************************************************
        public bool SetDcCurrent()
        {
            try
            {
                port.WriteLine(":CONF:CURR:DC");

                return ReadRecieveData(2000);

            }
            catch
            {
                return false;

            }
        }

        public bool GetDcCurrent()
        {
            try
            {
                port.WriteLine(":MEAS:CURR:DC?");


                bool respons = ReadRecieveData(2000);
                if (!respons)
                {
                    return respons;//falseが返ります
                }

                _CurrData = Double.Parse(RecieveData);
                return true;
            }
            catch
            {
                return false;

            }
        }

        //**************************************************************************
        //COMポートを閉じる
        //引数：なし
        //戻値：bool
        //**************************************************************************   
        public bool ClosePort()
        {
            try
            {
                //ca100用のポートが開いているかどうかの判定
                if (port.IsOpen == true)
                {
                    port.WriteLine("*RST");
                    port.WriteLine(":SYST:LOC");//ローカル設定に戻してからCOMポート閉じる
                    port.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SetLocal()
        {
            port.WriteLine(":SYST:LOC");//ローカル設定に戻してからCOMポート閉じる

        }


    }


}