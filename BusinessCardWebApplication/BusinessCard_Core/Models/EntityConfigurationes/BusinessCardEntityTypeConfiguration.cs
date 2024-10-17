using BusinessCard_Core.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BusinessCard_Core.Models.EntityConfigurationes
{
    public class BusinessCardEntityTypeConfiguration : IEntityTypeConfiguration<BusinessCard>
    {
        public void Configure(EntityTypeBuilder<BusinessCard> builder)
        {
            #region BusinessCard Table
            builder.ToTable("BusinessCards");
            #endregion

            #region Id 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            #endregion

            #region IsActive 
            builder.Property(x => x.IsActive)
                   .IsRequired(true)
                   .HasDefaultValue(true);
            #endregion

            #region CreationDateTime 
            builder.Property(x => x.CreationDateTime)
                   .IsRequired(true)
                   .HasDefaultValue(DateTime.Now);
            #endregion

            #region Name 
            builder.Property(x => x.Name)
                   .IsRequired(true);
            #endregion

            #region Gendear 
            builder.Property(x => x.Gendear)
                   .IsRequired(true);
            #endregion

            #region Email 
            builder.Property(x => x.Email)
                   .IsRequired(true);
            #endregion

            #region Phone 
            builder.Property(x => x.Phone)
                   .IsRequired(true);
            #endregion

            #region PhotoPath 
            builder.Property(x => x.PhotoPath)
                   .IsRequired(true);
            #endregion

            #region DateOfBirth 
            builder.Property(x => x.DateOfBirth)
                   .IsRequired(true)
                   .HasConversion(v => v.ToDateTime(TimeOnly.MinValue),  // Convert DateOnly to DateTime for the database
                                  v => DateOnly.FromDateTime(v));
            #endregion
        }
    }
}
