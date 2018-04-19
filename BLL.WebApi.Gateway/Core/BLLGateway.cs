using BLL.WebApi.Gateway.Common;
using Elements.DTO;
using System.Collections.Generic;
using System;

namespace BLL.WebApi.Gateway.Core
{
    internal class BLLGateway : IBLLGateway
    {
        public bool CanStudentBuyStudentTicket(int eventId, int studentId)
        {
            var nameIdPairs = new Dictionary<string, int>();
            nameIdPairs.Add("EventId", eventId);
            nameIdPairs.Add("StudentId", studentId);
            return HTTPRequestHelper.CreateGetRequest<bool>(ApiResources.CanStudentBuyStudentTicket, nameIdPairs);
        }

        public int CreateEvent(EventDTO dto)
        {
            return HTTPRequestHelper.CreatePostRequest<EventDTO>(ApiResources.CreateEvent, dto);
        }

        public EventDTO GetEvent(int id)
        {
            return HTTPRequestHelper.CreateGetRequest<EventDTO>(ApiResources.GetEvent, id);
        }

        public IEnumerable<EventDTO> GetFutureEvents()
        {
            return HTTPRequestHelper.CreateGetRequest<List<EventDTO>>(ApiResources.GetFutureEvents);
        }

    }
}
