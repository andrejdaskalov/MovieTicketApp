namespace Domain
{
    public class OrderItem : BaseEntity
    {
        public virtual MovieTicket MovieTicket { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
    }
}