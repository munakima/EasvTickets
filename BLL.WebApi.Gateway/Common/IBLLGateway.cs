using Elements.DTO;
using System.Collections.Generic;

namespace BLL.WebApi.Gateway.Common
{
    public interface IBLLGateway
    {
        IEnumerable<EventDTO> GetFutureEvents();
        EventDTO GetEvent(int id);
        int CreateEvent(EventDTO dto);
        bool CanStudentBuyStudentTicket(int eventId, int studentId);
    }
}
