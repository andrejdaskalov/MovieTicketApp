namespace Domain
{
    public class OrderItem : BaseEntity
    {
        public MovieTicket MovieTicket { get; set; }
        public int Quantity { get; set; }
    }
}