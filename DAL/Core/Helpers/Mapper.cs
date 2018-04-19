using DAL.Core.Models;
using Elements.DTO;
using System.Collections.Generic;

namespace DAL.Core
{
    internal static class Mapper
    {
        public static IEnumerable<EventDTO> Map(IEnumerable<Event> models)
        {
            var result = new List<EventDTO>();
            foreach (var model in models)
            {
                result.Add(Map(model));
            }
            return result;
        }

        public static EventDTO Map(Event model)
        {
            return new EventDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StudentPrice = model.StudentPrice,
                Date = model.Date,
                Place = model.Place
            };
        }

        public static Event Map(EventDTO dto)
        {
            return new Event()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StudentPrice = dto.StudentPrice,
                Date = dto.Date,
                Place = dto.Place
            };
        }

        public static StudentDTO Map(Student model)
        {
            return new StudentDTO()
            {
                Id = model.Id,
                Email = model.Email
            };
        }

        public static Student Map(StudentDTO dto)
        {
            return new Student()
            {
                Id = dto.Id,
                Email = dto.Email
            };
        }

        public static TicketDTO Map(Ticket model)
        {
            return new TicketDTO()
            {
                Id = model.Id,
                EventId = model.EventId,
                IsUsed = model.IsUsed,
                StudentId = model.StudentId,
                TicketTypeId = model.TicketTypeId
            };
        }  
        
        public static Ticket Map(TicketDTO dto)
        {
            return new Ticket()
            {
                Id = dto.Id,
                EventId = dto.EventId,
                IsUsed = dto.IsUsed,
                StudentId = dto.StudentId,
                TicketTypeId = dto.TicketTypeId
            };
        }      
    }
}
