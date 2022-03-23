using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SmartAC.Models.Common;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.Data.Sensors;
using System;

namespace SmartAC.Context.Sql
{
    public class SmartACDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public SmartACDbContext()
        {
            
        }

        public SmartACDbContext(DbContextOptions<SmartACDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetSection("AppSettings")["DbConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.BaseType == typeof(Enum))
                    {
                        var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                        var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                        property.SetValueConverter(converter);
                    }
                }
            }

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModifiedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.Serial).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Secret).IsRequired();

                entity.HasData(Device.GetSeedingData());
            });

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModifiedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Code).IsRequired();
                entity.Property(e => e.AlertDate).IsRequired();
                entity.Property(e => e.ViewStatus).IsRequired();
                entity.Property(e => e.ResolutionStatus).IsRequired();

                entity.HasOne(c => c.Device).WithMany(c => c.Alerts).HasForeignKey(c => c.DeviceId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.SensorsReading).WithMany(c => c.Alerts).HasForeignKey(c => c.SensorReadingId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DeviceRegisteration>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModifiedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.FirmwareVersion).IsRequired();

                entity.HasOne(c => c.Device).WithMany(c => c.DeviceRegisterations).HasForeignKey(c => c.DeviceId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SensorsReading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModifiedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.Temperature).IsRequired();
                entity.Property(e => e.Humidity).IsRequired();
                entity.Property(e => e.CarbonMonoxide).IsRequired();
                entity.Property(e => e.HealthStatus).IsRequired();

                entity.HasOne(c => c.Device).WithMany(c => c.DeviceSensorReadings).HasForeignKey(c => c.DeviceId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InvalidSensorReading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModifiedOn).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.Data).IsRequired().HasMaxLength(500);

                entity.HasOne(c => c.Device).WithMany(c => c.DeviceInvalidReadings).HasForeignKey(c => c.DeviceId).OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceRegisteration> DeviceRegisterations { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<SensorsReading> SensorReadings { get; set; }
        public DbSet<InvalidSensorReading> InvalidSensorReadings { get; set; }
    }
}
