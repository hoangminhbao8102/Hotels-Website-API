using HotelManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.Configurations
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Position).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Phone).HasMaxLength(20);

            builder.HasOne(e => e.Hotel)
                .WithMany(h => h.Employees)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Hotel xóa Employee
        }
    }
}
