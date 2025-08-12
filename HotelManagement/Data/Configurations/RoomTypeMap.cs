using HotelManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.Configurations
{
    public class RoomTypeMap : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes");
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Name).HasMaxLength(100).IsRequired();
            builder.Property(rt => rt.Description).HasMaxLength(255);
            builder.Property(rt => rt.Price).HasColumnType("decimal(18,2)");
        }
    }
}
