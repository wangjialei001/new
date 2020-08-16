using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuthService.Models.DBTest
{
    public partial class db_testContext : DbContext
    {
        public db_testContext()
        {
        }

        public db_testContext(DbContextOptions<db_testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Account1> Account1 { get; set; }
        public virtual DbSet<CstCustomer> CstCustomer { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<TbStudent> TbStudent { get; set; }
        public virtual DbSet<Test01> Test01 { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user id=root;password=123456;database=db_test", x => x.ServerVersion("8.0.16-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.Uid)
                    .HasName("PK_UID_UserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)")
                    .HasComment("主键");

                entity.Property(e => e.Money)
                    .HasColumnName("MONEY")
                    .HasColumnType("decimal(16,0)");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_UID_UserID");
            });

            modelBuilder.Entity<Account1>(entity =>
            {
                entity.ToTable("account1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("float(255,0)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CstCustomer>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PRIMARY");

                entity.ToTable("cst_customer");

                entity.Property(e => e.CustId)
                    .HasColumnName("cust_id")
                    .HasColumnType("bigint(32)")
                    .HasComment("客户编号(主键)");

                entity.Property(e => e.CustAddress)
                    .HasColumnName("cust_address")
                    .HasColumnType("varchar(128)")
                    .HasComment("客户联系地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustIndustry)
                    .HasColumnName("cust_industry")
                    .HasColumnType("varchar(32)")
                    .HasComment("客户所属行业")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustLevel)
                    .HasColumnName("cust_level")
                    .HasColumnType("varchar(32)")
                    .HasComment("客户级别")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustName)
                    .IsRequired()
                    .HasColumnName("cust_name")
                    .HasColumnType("varchar(32)")
                    .HasComment("客户名称(公司名称)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustPhone)
                    .HasColumnName("cust_phone")
                    .HasColumnType("varchar(64)")
                    .HasComment("客户联系电话")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CustSource)
                    .HasColumnName("cust_source")
                    .HasColumnType("varchar(32)")
                    .HasComment("客户信息来源")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleDesc)
                    .HasColumnName("ROLE_DESC")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RoleName)
                    .HasColumnName("ROLE_NAME")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TbStudent>(entity =>
            {
                entity.ToTable("tb_student");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Test01>(entity =>
            {
                entity.ToTable("test01");

                entity.HasIndex(e => new { e.A1, e.A2, e.A3, e.A4 })
                    .HasName("inx_a1_a2_a3_a4");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.A1)
                    .HasColumnName("a1")
                    .HasColumnType("int(4)");

                entity.Property(e => e.A2)
                    .HasColumnName("a2")
                    .HasColumnType("int(4)");

                entity.Property(e => e.A3)
                    .HasColumnName("a3")
                    .HasColumnType("int(4)");

                entity.Property(e => e.A4)
                    .HasColumnName("a4")
                    .HasColumnType("int(4)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("float(255,0)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_role");

                entity.HasIndex(e => e.Rid)
                    .HasName("FK_Role_RID");

                entity.HasIndex(e => e.Uid)
                    .HasName("FK_User_UID");

                entity.Property(e => e.Rid)
                    .HasColumnName("RID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.R)
                    .WithMany()
                    .HasForeignKey(d => d.Rid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_RID");

                entity.HasOne(d => d.U)
                    .WithMany()
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
