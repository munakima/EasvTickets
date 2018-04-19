using System;

namespace ConsoleTester
{

    class Program
    {
        static void Main(string[] args)
        {
            Loop();
            Console.ReadKey();
        }

        static void DebugOrNotTest()
        {
#if DEBUG
            Console.WriteLine("I am debugging");
#else
            Console.WriteLine("Iam not debugging");
#endif
        }

        static void Loop()
        {
            Console.WriteLine("Write 1 to test DAL, write 2 to Test BLL");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    TestDal();
                    break;
                case "2":
                    TestBll();
                    break;
                default:
                    Loop();
                    break;
            }
        }

        static void TestBll()
        {
            BLLGatewayTester.MainBLLGatewayTester();
        }

        static void TestDal()
        {
            DALTester.DALTester.MainDALTester();
        }
    }
}
