using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class DataDbContext : DbContext
    {
        public DataDbContext()
        {
        }

        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<QRTable> QRTables { get; set; }
        public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=spsp_db; user=sa; Password=test");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Bosnian_Latin_100_CI_AI");

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.ContactInfo).HasMaxLength(255);


                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);


                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK_Customer_UserAccountId");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_UserAccountId");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_BusinessId");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItem");


                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_MenuItem_CategoryId");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItem_MenuId");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");


                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");


                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_CustomerId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Order_EmployeeId");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");


                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MenuItemId)
                    .HasConstraintName("FK_OrderItem_MenuItemId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItem_OrderId");
            });

            modelBuilder.Entity<QRTable>(entity =>
            {
                entity.ToTable("QRTable");



                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.QRTables)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QRTable_BusinessId");
            });

            modelBuilder.Entity<SchemaVersion>(entity =>
            {
                entity.Property(e => e.Applied).HasColumnType("datetime");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");



                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
