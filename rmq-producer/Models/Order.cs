namespace rmq_producer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
