
namespace _91821217Tester
{
    public static class TestXPort
    {

        public static bool Check()
        {
            try
            {
                Target.SendForXportCheck("RS403");

                var dialog = new DialogPic("黄、緑 ＬＥＤが点灯してる？？", DialogPic.NAME.その他);
                dialog.ShowDialog();
                if (!Flags.DialogReturn)
                    return false;

                dialog = new DialogPic("ＪＰ１を、１－３と４－６を短絡したら\r\nＳ１を押してね！", DialogPic.NAME.その他);
                dialog.ShowDialog();
                if (!Flags.DialogReturn)
                    return false;

                dialog = new DialogPic("黄、赤、緑 ＬＥＤが点灯してる？？", DialogPic.NAME.その他);
                dialog.ShowDialog();
                if (!Flags.DialogReturn)
                    return false;

                dialog = new DialogPic("ＪＰ１を、２－４と３－５を短絡したら\r\nＳ１を押してね！", DialogPic.NAME.その他);
                dialog.ShowDialog();
                if (!Flags.DialogReturn)
                    return false;

                return Target.ReadForXPortCheck();

            }
            catch
            {
                return false;
            }
            finally
            {
            }
        }

    }
}
