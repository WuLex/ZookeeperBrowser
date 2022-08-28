using AllModel.MyOrm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllModel.Code
{
    public class myDbContext : BaseDbContext
    {
        public myDbContext()
        {
        }

        public myDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<AccountEntity> Accounts { get; set; }


        //public DbSet<AuthInfoEntity> AuthInfos { get; set; }


        //public DbSet<ConfigEntity> Configs    { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseInMemoryDatabase("MyDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AuthInfoConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigConfiguration());
        }

        internal sealed class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
        {
            public void Configure(EntityTypeBuilder<AccountEntity> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(b => b.UserName);
                builder.Property(b => b.PassWord);
                builder.Property(b => b.Type);
                builder.Property(x => x.Name);
                builder.Property(b => b.Status);

                builder.Property(x => x.ModifiedTime);
                builder.Ignore(x => x.CreatedTime);
                builder.Ignore(x => x.OperatorName);
                builder.Ignore(x => x.Deleted);
            }
        }

        internal sealed class AuthInfoConfiguration : IEntityTypeConfiguration<AuthInfoEntity>
        {
            public void Configure(EntityTypeBuilder<AuthInfoEntity> builder)
            {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.AccountId);
                builder.Property(x => x.Platform);
                builder.Property(x => x.RefreshToken);

                builder.Property(x => x.RefreshTokenExpiredTime);

                builder.Property(x => x.LoginTime);

                builder.Property(x => x.LoginIP);

                builder.Property(x => x.ModifiedTime);
                builder.Ignore(x => x.CreatedTime);
                builder.Ignore(x => x.OperatorName);
                builder.Ignore(x => x.Deleted);
            }
        }

        internal sealed class ConfigConfiguration : IEntityTypeConfiguration<ConfigEntity>
        {
            public void Configure(EntityTypeBuilder<ConfigEntity> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Code);
                builder.Property(x => x.Value);

                builder.Property(x => x.ModifiedTime);

                builder.Ignore(x => x.CreatedTime);
                builder.Ignore(x => x.OperatorName);
                builder.Ignore(x => x.Deleted);
            }
        }
    }
}