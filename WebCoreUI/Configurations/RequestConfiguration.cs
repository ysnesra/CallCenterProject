using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Concrete;

namespace WebCoreUI.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            //builder.HasOne(a => a.Customer)
            //   .WithMany(a => a.Requests)
            //   .HasForeignKey(a => a.CustomerId)
            //   .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(a => a.CustomerRep)
            //   .WithMany(a => a.Requests)
            //   .HasForeignKey(a => a.CustomerRepId)
            //   .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(a => a.RequestType)
            //   .WithMany(a => a.Requests)
            //   .HasForeignKey(a => a.RequestTypeId)
            //   .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(a => a.Status)
            //  .WithMany(a => a.Requests)
            //  .HasForeignKey(a => a.StatusId)
            //  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
