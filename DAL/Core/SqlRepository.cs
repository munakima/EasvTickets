using DAL.Common;
using Elements.DTO;
using DAL.Core.Models;
using System.Collections.Generic;

namespace DAL.Core
{
    internal class SqlRepository : ISqlRepository
    {
        #region Event
        public EventDTO GetEvent(int id)
        {
            var sqlModel = CRUDRepositoryHelper.Get<Event>(id);
            return Mapper.Map(sqlModel);
        }

        public int CreateEvent(EventDTO dto)
        {
            var sqlModel = Mapper.Map(dto);
            return CRUDRepositoryHelper.Create(sqlModel);
        }

        public EventDTO UpdateEvent(EventDTO dto)
        {
            var sqlModel = Mapper.Map(dto);
            var updatedSqlModel = CRUDRepositoryHelper.Update<Event>(sqlModel);
            return Mapper.Map(updatedSqlModel);
        }

        public bool DeleteEvent(int id)
        {
            return CRUDRepositoryHelper.Delete<Event>(id);
        }

        public IEnumerable<EventDTO> GetFutureEvents()
        {
            var result = new List<Event>();
            SqlHelper.UseSqlConnection((sqlConnection) =>
            {
                var query = SqlHelper.GetFutureEventsQuery();
                var sqlCommand = SqlHelper.SqlCommandWithQueryBuilder(query, sqlConnection);
                var sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    var currentRow = SqlHelper.SqlReaderToModelBuilder<Event>(sqlReader);
                    result.Add(currentRow);
                }
            });
            return Mapper.Map(result);
        }

        public bool CanStudentBuyStudentTicket(int eventId, int studentId)
        {
            if (eventId <= 0 || studentId <= 0)
            {
                return true;
            }
            var result = default(bool);
            SqlHelper.UseSqlConnection((sqlConnection) =>
            {
                var query = SqlHelper.CanStudentBuyStudentTicketQuery();
                var sqlCommand = SqlHelper.SqlCommandWithEventIdAndStudentId(query, sqlConnection, eventId, studentId);
                var sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    result = sqlReader.GetBoolean(0);
                }

            });
            return result;
        }
        #endregion

        #region Student
        public StudentDTO GetStudent(int id)
        {
            var sqlModel = CRUDRepositoryHelper.Get<Student>(id);
            return Mapper.Map(sqlModel);
        }

        public int CreateStudent(StudentDTO dto)
        {
            var sqlModel = Mapper.Map(dto);
            return CRUDRepositoryHelper.Create(sqlModel);
        }

        public StudentDTO UpdateStudent(StudentDTO dto)
        {
            var sqlModel = Mapper.Map(dto);
            var updatedSqlModel = CRUDRepositoryHelper.Update<Student>(sqlModel);
            return Mapper.Map(updatedSqlModel);
        }

        public bool DeleteStudent(int id)
        {
            return CRUDRepositoryHelper.Delete<Student>(id);
        }
        #endregion

        #region Ticket
        public TicketDTO GetTicket(int id)
        {
            var sqlModel = CRUDRepositoryHelper.Get<Ticket>(id);
            return Mapper.Map(sqlModel);
        }

        public int CreateTicket(TicketDTO dto)
        {
            var sqlModel = Mapper.Map(dto);
            return CRUDRepositoryHelper.Create(sqlModel);
        }

        public TicketDTO UpdateTicket(TicketDTO dto)
        {
            var sqlModel = Mapper.Map(dto);
            var updatedSqlModel = CRUDRepositoryHelper.Update<Ticket>(sqlModel);
            return Mapper.Map(updatedSqlModel);
        }

        public bool DeleteTicket(int id)
        {
            return CRUDRepositoryHelper.Delete<Ticket>(id);
        }
        #endregion
    }
}
