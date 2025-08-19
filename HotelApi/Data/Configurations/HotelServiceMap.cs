using HotelApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApi.Data.Configurations
{
    public class HotelServiceMap : IEntityTypeConfiguration<HotelService>
    {
        public void Configure(EntityTypeBuilder<HotelService> builder)
        {
            builder.ToTable("HotelServices");
            builder.HasKey(hs => new { hs.HotelId, hs.ServiceId });

            builder.HasOne(hs => hs.Hotel)
                .WithMany(h => h.HotelServices)
                .HasForeignKey(hs => hs.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(hs => hs.Service)
                .WithMany(s => s.HotelServices)
                .HasForeignKey(hs => hs.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
