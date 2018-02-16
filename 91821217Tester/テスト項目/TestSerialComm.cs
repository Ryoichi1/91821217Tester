using System.Threading.Tasks;

namespace _91821217Tester
{
    public static class TestSerialComm
    {
        public static async Task<bool> CheckSerial()
        {
            bool result;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    var tm = new GeneralTimer(15000);
                    tm.start();
                    while (true)
                    {
                        //うめきのバグ
                        //通信が不安定なのでなんかいかリトライする
                        if (tm.FlagTimeout) return false;
                        if (!Target.SendData("RS401", Wait: 500, DoAnalysis: false))
                            continue;
                        if (!Target.RecieveData.Contains("U2TXMSG"))
                            continue;
                        if (Target.SendData("u2msg"))
                            break;
                    }
                    return true;
                });

            }
            finally
            {
            }

        }


    }
}
