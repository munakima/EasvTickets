namespace DAL.Core.Models
{
    internal class Ticket 
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public bool IsUsed { get; set; }
        public int StudentId { get; set; }
        public int TicketTypeId { get; set; }
    }
}
