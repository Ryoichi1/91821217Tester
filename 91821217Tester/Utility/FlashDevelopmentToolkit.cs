using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;
using static System.Threading.Thread;


namespace _91821217Tester
{
    ///<summary>
    ///●注意事項
    ///このクラスはＦＤＴをシンプルインターフェイスで立ち上げることを前提に作ってあります
    /// </summary>

    public static class FDT
    {
        //********************************************************************************************************
        // 外部プロセスのメイン・ウィンドウを起動するためのWin32 API
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, StringBuilder lParam);

        private const int WM_GETTEXT = 0x000D;

        // ShowWindowAsync関数のパラメータに渡す定義値
        private const int SW_RESTORE = 9;  // 画面を元の大きさに戻す
        //********************************************************************************************************


        //静的メンバの宣言
        public static string WorkSpacePath;  //FDTのワークスペースファイルのありか   
        private static System.Timers.Timer Tm;

        //フラグ
        private static bool FlagTimer;
        public static bool FlagWrite { get; private set; }
        public static bool FlagSum { get; private set; }

        public static string ReadSumValue { get; set; }

        //コンストラクタ
        static FDT()
        {
            //タイマー（ウィンドウハンドル取得用）の設定
            Tm = new System.Timers.Timer();
            Tm.Enabled = false;
            Tm.Interval = 5000;
            Tm.Elapsed += new ElapsedEventHandler(tm_Tick);
        }

        //イニシャライズ(外部からラムダ式を渡し、用途に応じてカスタマイズする)
        public static void InitFdt(Action method)
        {
            method();//用途に応じた処理
        }

        //タイマーイベントハンドラ
        private static void tm_Tick(object source, ElapsedEventArgs e)
        {
            Tm.Stop();
            FlagTimer = false;//タイムアウト

        }


        public static async Task<bool> WriteFirmware(string WorkSpacePath, string checkSum = null)
        {
            //フラグの初期化
            FlagWrite = false;
            FlagSum = false;
            ReadSumValue = "---";
            var re = await Task.Run<bool>(() =>
            {
                var Fdt = new Process();

                try
                {

                    //外部でE8aエミュレータを接続する（クラス外部で処理する）

                    //プロセスを作成してFDTを立ち上げる

                    Fdt.StartInfo.FileName = WorkSpacePath;
                    Fdt.Start();
                    Fdt.WaitForInputIdle();//指定されたプロセスで未処理の入力が存在せず、ユーザーからの入力を待っている状態になるまで、またはタイムアウト時間が経過するまで待機します。

                    //FDTのウィンドウハンドル取得
                    FlagTimer = true;
                    Tm.Start();
                    IntPtr hWnd = IntPtr.Zero;//メインウィンドウのハンドル
                    while (hWnd == IntPtr.Zero)
                    {
                        //Application.DoEvents();
                        if (!FlagTimer) return false;
                        hWnd = FindWindow(null, "FDT Simple Interface   (Supported Version)");
                    }

                    IntPtr ログテキストハンドル = FindWindowEx(hWnd, IntPtr.Zero, "RICHEDIT", "");

                    //FDTを最前面に表示してアクティブにする（センドキー送るため）
                    SetForegroundWindow(hWnd);
                    Sleep(1000);

                    SendKeys.SendWait("{TAB}");
                    Sleep(100);
                    SendKeys.SendWait("{ENTER}");
                    Sleep(400);
                    //E8aからの電源供給では不安定で書き込みできないことがある
                    //必ず試験機側から電源供給すること
                    ////SendKeys.SendWait("P");
                    ////Sleep(400);
                    ////SendKeys.SendWait("5");
                    ////Sleep(400);
                    SendKeys.SendWait("{ENTER}");
                    Sleep(2000);
                    SendKeys.SendWait("{ENTER}");
                    Sleep(3000);
                    SendKeys.SendWait("{ENTER}");//追加 ID入力画面が出ていたら押す

                    int MaxSize = 10000;
                    StringBuilder sb = new StringBuilder(MaxSize);
                    string LogBuff = "";

                    var tm = new GeneralTimer(60000);
                    tm.start();//念のため無限ループ防止
                    while (true)
                    {
                        if (tm.FlagTimeout) return false;
                        Sleep(1000);//インターバル1秒　※インターバル無しの場合FDTがこける
                        SendMessage(ログテキストハンドル, WM_GETTEXT, MaxSize - 1, sb);
                        LogBuff = sb.ToString();
                        if (LogBuff.Contains("IDコードが不一致です"))
                            return false;
                        if (LogBuff.Contains("Error"))
                            return false;
                        if (LogBuff.Contains("Disconnected"))
                            break;
                    }

                    FlagWrite = (LogBuff.IndexOf("書き込みが完了しました") >= 0);
                    if (!FlagWrite) return false;

                    //チェックサムがあっているかの判定
                    if (checkSum == null)
                        return true;
                    FlagSum = LogBuff.Contains("User Flash 1 = 0x" + checkSum);

                    //読み出したチェックサム値を求めておく（NG判定時に使用する）
                    var index = LogBuff.IndexOf("User Flash 1 = 0x");//17
                    ReadSumValue = LogBuff.Substring(index + 17, 8);

                    return FlagSum;

                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (Fdt != null)
                    {
                        Fdt.Kill();
                        Fdt.Close();
                        Fdt.Dispose();
                        Sleep(700);//FDT閉じてすぐにE8a外すとダメ！　ダメよぉ　ダメダメ！！！ ※2014年流行語大賞
                    }
                }
            });

            return re;
            //E1エミュレータを切り離すする（クラス外部で処理する）

        }








    }

}