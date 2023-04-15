using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Concrete;

namespace WebCoreUI.Configurations
{
    public class CallConfiguration : IEntityTypeConfiguration<Call>
    {
        public void Configure(EntityTypeBuilder<Call> builder)
        {
            builder.HasOne(a => a.Customer)
               .WithMany(a => a.Calls)
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.CustomerRep)
               .WithMany(a => a.Calls)
               .HasForeignKey(a => a.CustomerRepId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Request)
               .WithMany(a => a.Calls)
               .HasForeignKey(a => a.RequestId)
               .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
