using BLL.WebApi.Gateway.Common;
using Elements.DTO;
using System;

namespace ConsoleTester
{
    static class BLLGatewayTester
    {
        public static void MainBLLGatewayTester()
        {
            Console.WriteLine("Press any key when the web api app has started....");
            Console.ReadKey();

            var newEvent = new EventDTO()
            {
                Name = "BLLName",
                Description = "BLLDesc",
                Price = 1000,
                StudentPrice = 500,
                Date = DateTime.Now.AddYears(1),
                Place = "BLLPlace"
            };

            var gateway = BLLGatewayFactory.CreateOrGetIgnoreDebug();
            var canStudentBuy = gateway.CanStudentBuyStudentTicket(1, 1);
            var a = "asd";

        }
    }
}
