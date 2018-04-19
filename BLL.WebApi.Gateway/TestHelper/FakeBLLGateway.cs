using System;
using System.Collections.Generic;
using BLL.WebApi.Gateway.Common;
using Elements.DTO;

namespace BLL.WebApi.Gateway.TestHelper
{
    /// <summary>
    /// This class exists only for the purpose of testing. It's designed to mock a Web API calls.
    /// This class should only be used in DEBUG mode.
    /// </summary>
    internal class FakeBLLGateway : IBLLGateway
    {
        public bool CanStudentBuyStudentTicket(int eventId, int studentId)
        {
            return true;
        }

        public int CreateEvent(EventDTO dto)
        {
            return 1;
        }

        public EventDTO GetEvent(int id)
        {
            switch (id)
            {
                case 1:
                    return DtoWithId1();
                case 2:
                    return DtoWithId2();
                case 3:
                    return DtoWithId3();
                case 4:
                    return DtoWithId4();
                case 5:
                    return DtoWithId5();
                default:
                    return DtoWithId1();
            }
        }

        public IEnumerable<EventDTO> GetFutureEvents()
        {
            return new List<EventDTO>()
            {
                DtoWithId1(),
                DtoWithId2(),
                DtoWithId3(),
                DtoWithId4(),
                DtoWithId5()
            };
        }

        internal EventDTO CreateFakeDTO(int id)
        {
            return new EventDTO()
            {
   
            };
        }

        internal EventDTO DtoWithId1()
        {
            return new EventDTO()
            {
                Id = 1,
                Name = "Christmas Party",
                Description = "Come and Celebrate Christmas Eve with us!!",
                Price = 50,
                StudentPrice = 0,
                Date = new DateTime(2017, 12, 24),
                Place = "EASV C101"
            };
        }

        internal EventDTO DtoWithId2()
        {
            return new EventDTO()
            {
                Id = 2,
                Name = "New Year's Eve",
                Description = "You like beautiful fireworks? We have a planned show with experts when the clock hits. After that, you will buy able to buy your own rockets for very cheap and enjoy the evening with us",
                Price = 100,
                StudentPrice = 100,
                Date = new DateTime(2017, 12, 31, 20, 0, 0),
                Place = "EASV Cafeteria"
            };
        }

        internal EventDTO DtoWithId3()
        {
            return new EventDTO()
            {
                Id = 3,
                Name = "Graduation Party",
                Description = "Come and celebrate student who finished their education and will move into the real world",
                Price = 200,
                StudentPrice = 0,
                Date = new DateTime(2018, 1, 27),
                Place = "Spangsbjerg Kirkevej 103"
            };
        }

        internal EventDTO DtoWithId4()
        {
            return new EventDTO()
            {
                Id = 4,
                Name = "Easter Holiday",
                Description = "We'll paint eggs!!!!",
                Price = 500,
                StudentPrice = 250,
                Date = new DateTime(2018, 2, 15),
                Place = "Midtown Esbjerg",
            };
        }

        internal EventDTO DtoWithId5()
        {
            return new EventDTO()
            {
                Id = 5,
                Name = "Second Halloween",
                Description = "We like halloween, so let's celebrate it twice a year",
                Price = 100,
                StudentPrice = 100,
                Date = new DateTime(2018, 4, 27),
                Place = "Spooky town"
            };
        }
    }
}
