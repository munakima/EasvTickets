using DAL.Common;
using Elements.DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace BLL.WebApi.Controllers
{
    [RoutePrefix("api/Event")]
    public class EventController : ApiController
    {
        [HttpGet]
        public EventDTO Get(int id)
        {
            return SqlRepositoryFactory.CreateOrGet().GetEvent(id);
        }

        [HttpGet]
        [Route("GetFutureEvents")]
        public IEnumerable<EventDTO> GetFutureEvents()
        {
            return SqlRepositoryFactory.CreateOrGet().GetFutureEvents();
        }

        [HttpPost]
        public int Create(EventDTO dto)
        {
            return SqlRepositoryFactory.CreateOrGet().CreateEvent(dto);
        }

        [HttpGet]
        [Route("CanStudentBuyStudentTicket")]
        public bool CanStudentBuyStudentTicket(int eventId, int studentId)
        {
            return SqlRepositoryFactory.CreateOrGet().CanStudentBuyStudentTicket(eventId, studentId);
        }
    }
}
