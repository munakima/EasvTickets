using DAL.Common;
using Elements.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTester
{
    public static class DALTester
    {
        public static void MainDALTester()
        {
            var repository = SqlRepositoryFactory.CreateOrGet();
            var booleanvalue = repository.CanStudentBuyStudentTicket(1, 1);

            
            //var a = repository.GetEvent(1);

            //Console.WriteLine(a.Name);

            //var dto = new EventDTO()
            //{
            //    Name = "dto",
            //    Description = "dto",
            //    Price = 10,
            //    StudentPrice = 5,
            //    Date = DateTime.Now,
            //    Place = "dto"
            //};
            //var id = repository.CreateEvent(dto);
            Console.WriteLine(booleanvalue);
        }
    }
}
