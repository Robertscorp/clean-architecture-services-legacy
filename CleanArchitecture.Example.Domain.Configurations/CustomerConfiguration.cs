using CleanArchitecture.Example.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Example.Domain.Configurations
{

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasEntityID<Customer, long>();

            //_ = builder.Property(e => e.Date);
            //_ = builder.Property(e => e.ImageDataFormat);
            //_ = builder.Property(e => e.Item);
            //_ = builder.Property(e => e.Note);
            //_ = builder.Property(e => e.Value);
        }

    }

}
