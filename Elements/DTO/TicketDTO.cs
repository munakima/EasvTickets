namespace Elements.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public bool IsUsed { get; set; }
        public int StudentId { get; set; }
        public int TicketTypeId { get; set; }
    }
}
