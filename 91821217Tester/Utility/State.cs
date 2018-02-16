using System;
using System.Collections.Generic;

namespace _91821217Tester
{
    public class TestSpecs
    {
        public int Key;
        public string Value;
        public bool PowSw;

        public TestSpecs(int key, string value, bool powSW = true)
        {
            this.Key = key;
            this.Value = value;
            this.PowSw = powSW;

        }
    }

    public static class State
    {

        //データソース（バインディング用）
        public static ViewModelMainWindow VmMainWindow = new ViewModelMainWindow();
        public static ViewModelTestStatus VmTestStatus = new ViewModelTestStatus();
        public static ViewModelTestResult VmTestResults = new ViewModelTestResult();
        public static ViewModelCommunication VmComm = new ViewModelCommunication();
        public static TestCommand testCommand = new TestCommand();

        //パブリックメンバ
        public static Configuration Setting { get; set; }
        public static TestSpec TestSpec { get; set; }

        public static string CurrDir { get; set; }

        public static string AssemblyInfo { get; set; }

        public static Uri uriOtherInfoPage { get; set; }


        public static List<TestSpecs> テスト項目 = new List<TestSpecs>()
        {
            new TestSpecs(100, "検査ソフト書き込み", false),

            new TestSpecs(200, "電源電圧チェック +12V",  true),
            new TestSpecs(201, "電源電圧チェック +5V",   true),
            new TestSpecs(202, "電源電圧チェック +3.3V", true),
            new TestSpecs(203, "電源電圧チェック -5V",   true),

            new TestSpecs(300, "PWM1電流チェック50",  false),
            new TestSpecs(301, "PWM1電流チェック100", true),
            new TestSpecs(302, "PWM2電流チェック50",  true),
            new TestSpecs(303, "PWM2電流チェック100", true),

            new TestSpecs(400, "PWM1電圧チェック50",  false),
            new TestSpecs(401, "PWM1電圧チェック100", true),
            new TestSpecs(402, "PWM2電圧チェック50",  true),
            new TestSpecs(403, "PWM2電圧チェック100", true),

            new TestSpecs(500, "S1 押しボタンチェック", true),
            new TestSpecs(501, "S2 押しボタンチェック", true),

            new TestSpecs(600, "DSW1 チェック", true),

            new TestSpecs(700, "X-Port動作 チェック", true),

            new TestSpecs(800, "CN1動作 チェック", true),

            new TestSpecs(900, "CN2動作 チェック", true),

            new TestSpecs(1000, "E2PROM チェック", true),

            new TestSpecs(1100, "RS422/485通信 チェック", true),

            new TestSpecs(1200, "製品プログラム書き込み", false),

            new TestSpecs(1300, "Verチェック", false),

            new TestSpecs(1400, "初期化", false),


        };

        //個別設定のロード
        public static void LoadConfigData()
        {
            //Configファイルのロード
            Setting = Deserialize<Configuration>(Constants.filePath_Configuration);


            VmMainWindow.ListOperator = Setting.作業者リスト;
            VmMainWindow.Theme = Setting.PathTheme;
            VmMainWindow.ThemeOpacity = Setting.OpacityTheme;

            if (Setting.日付 != DateTime.Now.ToString("yyyyMMdd"))
            {
                Setting.日付 = DateTime.Now.ToString("yyyyMMdd");
                Setting.TodayOkCount = 0;
                Setting.TodayNgCount = 0;
            }

            VmTestStatus.OkCount = Setting.TodayOkCount.ToString() + "台";
            VmTestStatus.NgCount = Setting.TodayNgCount.ToString() + "台";

            //TestSpecファイルのロード
            TestSpec = Deserialize<TestSpec>(Constants.filePath_TestSpec);
        }


        //インスタンスをXMLデータに変換する
        public static bool Serialization<T>(T obj, string xmlFilePath)
        {
            try
            {
                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                //書き込むファイルを開く（UTF-8 BOM無し）
                System.IO.StreamWriter sw = new System.IO.StreamWriter(xmlFilePath, false, new System.Text.UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, obj);
                //ファイルを閉じる
                sw.Close();

                return true;

            }
            catch
            {
                return false;
            }

        }

        //XMLデータからインスタンスを生成する
        public static T Deserialize<T>(string xmlFilePath)
        {
            System.Xml.Serialization.XmlSerializer serializer;
            using (var sr = new System.IO.StreamReader(xmlFilePath, new System.Text.UTF8Encoding(false)))
            {
                serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        //********************************************************
        //個別設定データの保存
        //********************************************************
        public static bool Save個別データ()
        {
            try
            {
                //Configファイルの保存
                Setting.作業者リスト = VmMainWindow.ListOperator;
                Setting.PathTheme = VmMainWindow.Theme;
                Setting.OpacityTheme = VmMainWindow.ThemeOpacity;

                Serialization<Configuration>(Setting, Constants.filePath_Configuration);

                return true;
            }
            catch
            {
                return false;

            }

        }




    }

}
