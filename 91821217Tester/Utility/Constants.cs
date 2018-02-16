
namespace _91821217Tester
{
    public static class Constants
    {
        //スタートボタンの表示
        public const string 開始 = "検査開始";
        public const string 停止 = "強制停止";
        public const string 確認 = "確認";

        //作業者へのメッセージ
        public const string MessOpecode  = "工番を入力してください";
        public const string MessOperator = "作業者名を選択してください";
        public const string MessSet      = "製品をセットして開始ボタンを押してください";
        public const string MessRemove   = "製品を取り外してください";
        public const string MessWait     = "検査中！　しばらくお待ちください・・・";
        public const string MessConnect  = "周辺機器の接続を確認してください！";

        public static readonly string fdtPath_TEST    = @"C:\91821217\FDT_WORK_SPACE\WriteTest\WriteTest.AWS";
        public static readonly string fdtPath_PRODUCT = @"C:\91821217\FDT_WORK_SPACE\WriteProduct\WriteProduct.AWS";

        public static readonly string filePath_TestSpec      = @"C:\91821217\ConfigData\TestSpec.config";
        public static readonly string filePath_Configuration = @"C:\91821217\ConfigData\Configuration.config";

        public static readonly string Path_Manual = @"C:\91821217\Manual.pdf";

        //検査データフォルダのパス
        public static readonly string PassDataFolderPath = @"C:\91821217\検査データ\合格品データ\";
        public static readonly string FailDataFolderPath = @"C:\91821217\検査データ\不良品データ\";


        //リトライ回数
        public static readonly int RetryCount = 0;












    }
}
