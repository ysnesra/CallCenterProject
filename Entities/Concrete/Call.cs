using Core.Entities;

namespace Entities.Concrete
{
    public class Call :IEntity
    {
        public int CallId { get; set; }
        public string CallRecord { get; set; }
        public DateTime CallDate { get; set; }
        public float CallTime { get; set; }
        public string? CallNote { get; set; }
        public int CustomerId { get; set; }
        public int CustomerRepId { get; set; }
        public int RequestId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerRep CustomerRep { get; set; }
        public virtual Request Request { get; set; }

    }
}


