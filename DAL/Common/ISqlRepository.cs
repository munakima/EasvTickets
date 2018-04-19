using Elements.DTO;
using System.Collections.Generic;

namespace DAL.Common
{
    public interface ISqlRepository
    {
        EventDTO GetEvent(int id);
        int CreateEvent(EventDTO dto);
        EventDTO UpdateEvent(EventDTO dto);
        //bool DeleteEvent(int id);
        IEnumerable<EventDTO> GetFutureEvents();
        bool CanStudentBuyStudentTicket(int eventId, int studentId);

        StudentDTO GetStudent(int id);
        int CreateStudent(StudentDTO dto);
        StudentDTO UpdateStudent(StudentDTO dto);
        //bool DeleteStudent(int id);

        TicketDTO GetTicket(int id);
        int CreateTicket(TicketDTO dto);
        //TicketDTO UpdateTicket(TicketDTO dto);
        //bool DeleteTicket(int id);
    }
}
