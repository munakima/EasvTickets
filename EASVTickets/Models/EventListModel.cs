using Elements.DTO;
using System.Collections.Generic;

namespace EASVTickets.Models
{
    public class EventListModel
    {
        public IEnumerable<EventDTO> Events { get; set; }
    }
}