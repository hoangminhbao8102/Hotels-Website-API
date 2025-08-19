using HotelApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApi.Data.Configurations
{
    public class HotelMap : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Name).HasMaxLength(200).IsRequired();
            builder.Property(h => h.Domain).HasMaxLength(100).IsRequired();
            builder.HasIndex(h => h.Domain).IsUnique();
            builder.Property(h => h.Address).HasMaxLength(255).IsRequired();
            builder.Property(h => h.Description).IsRequired();
            builder.Property(h => h.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(h => h.Owner)
                .WithMany(u => u.Hotels)
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh vòng lặp khi xóa User
        }
    }
}
