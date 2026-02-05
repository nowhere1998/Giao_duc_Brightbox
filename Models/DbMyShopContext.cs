using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Models;

public partial class DbMyShopContext : DbContext
{
    public DbMyShopContext()
    {
    }

    public DbMyShopContext(DbContextOptions<DbMyShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advertise> Advertises { get; set; }

    public virtual DbSet<CateRss> CateRsses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentPro> CommentPros { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<DocumentTypeUser> DocumentTypeUsers { get; set; }
    public virtual DbSet<Enrollment> Enrollments  { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }
    public virtual DbSet<Faculty> Faculties { get; set; }
    public virtual DbSet<ProFaculty> ProFaculties { get; set; }

    public virtual DbSet<GroupLibrary> GroupLibraries { get; set; }

    public virtual DbSet<GroupLibraryUser> GroupLibraryUsers { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<GroupNews> GroupNews { get; set; }

    public virtual DbSet<GroupNewsUser> GroupNewsUsers { get; set; }

    public virtual DbSet<GroupSupport> GroupSupports { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<ListDangkyhocCackhoa> ListDangkyhocCackhoas { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PhieuDk> PhieuDks { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Support> Supports { get; set; }

    public virtual DbSet<TbCounter> TbCounters { get; set; }

    public virtual DbSet<TbGiatriDangky> TbGiatriDangkies { get; set; }

    public virtual DbSet<TbSukien> TbSukiens { get; set; }

    public virtual DbSet<TbTtdangky> TbTtdangkies { get; set; }

    public virtual DbSet<TbValueCombo> TbValueCombos { get; set; }

    public virtual DbSet<Toggle> Toggles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<VoteDetail> VoteDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Advertise_Id");

            entity.ToTable("Advertise");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Target)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CateRss>(entity =>
        {
            entity.ToTable("CateRSS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Beginstring)
                .HasMaxLength(255)
                .HasColumnName("beginstring");
            entity.Property(e => e.Cateid).HasColumnName("cateid");
            entity.Property(e => e.Endstring)
                .HasMaxLength(255)
                .HasColumnName("endstring");
            entity.Property(e => e.Imgfolderpath)
                .HasMaxLength(255)
                .HasColumnName("imgfolderpath");
            entity.Property(e => e.Rsslink).HasMaxLength(255);
            entity.Property(e => e.Source)
                .HasMaxLength(255)
                .HasColumnName("source");
            entity.Property(e => e.Ulrimgnew)
                .HasMaxLength(255)
                .HasColumnName("ulrimgnew");
            entity.Property(e => e.Urlimg)
                .HasMaxLength(255)
                .HasColumnName("urlimg");
            entity.Property(e => e.Urlimgold)
                .HasMaxLength(255)
                .HasColumnName("urlimgold");
            entity.Property(e => e.Urlimgupdate)
                .HasMaxLength(255)
                .HasColumnName("urlimgupdate");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Category_Id");

            entity.ToTable("Category", tb => tb.HasTrigger("tg_Update_Level_Category"));

            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Image)
                .HasMaxLength(256)
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tag).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Comment_Id");

            entity.ToTable("Comment");

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.News).WithMany(p => p.Comments)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Comment_NewsId");
        });

        modelBuilder.Entity<CommentPro>(entity =>
        {
            entity.ToTable("commentPro"); // đúng tên bảng mới

            entity.Property(e => e.Comment1)
                .HasColumnName("Comment");

            entity.Property(e => e.Date)
                .HasColumnType("datetime");

            entity.Property(e => e.Email)
                .HasMaxLength(250);

            entity.Property(e => e.Name)
                .HasMaxLength(250);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.CommentPros)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_commentPro_Product");

            entity.HasOne(d => d.Customer)
                .WithMany(c => c.CommentPros) // hoặc .WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_commentPro_Customer");
        });


        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Config_Id");

            entity.ToTable("Config");

            entity.Property(e => e.Contact).HasColumnType("ntext");
            entity.Property(e => e.Copyright).HasColumnType("ntext");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.FlickrLink)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("flickrLink");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HotLine).HasMaxLength(250);
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.MailInfo)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Mail_Info");
            entity.Property(e => e.MailNoreply)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Mail_Noreply");
            entity.Property(e => e.MailPassword)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Mail_Password");
            entity.Property(e => e.MailPort).HasColumnName("Mail_Port");
            entity.Property(e => e.MailSmtp)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Mail_Smtp");
            entity.Property(e => e.PicasaLink)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("picasaLink");
            entity.Property(e => e.PlaceBody).HasMaxLength(500);
            entity.Property(e => e.PlaceHead).HasMaxLength(500);
            entity.Property(e => e.SocialLink1)
                .HasMaxLength(250)
                .HasColumnName("socialLink1");
            entity.Property(e => e.SocialLink2)
                .HasMaxLength(250)
                .HasColumnName("socialLink2");
            entity.Property(e => e.SocialLink3)
                .HasMaxLength(250)
                .HasColumnName("socialLink3");
            entity.Property(e => e.SocialLink4)
                .HasMaxLength(250)
                .HasColumnName("socialLink4");
            entity.Property(e => e.SocialLink5)
                .HasMaxLength(250)
                .HasColumnName("socialLink5");
            entity.Property(e => e.SocialLink6)
                .HasMaxLength(250)
                .HasColumnName("socialLink6");
            entity.Property(e => e.SocialLink7)
                .HasMaxLength(250)
                .HasColumnName("socialLink7");
            entity.Property(e => e.SocialLink8)
                .HasMaxLength(250)
                .HasColumnName("socialLink8");
            entity.Property(e => e.SocialLink9)
                .HasMaxLength(250)
                .HasColumnName("socialLink9");
            entity.Property(e => e.Title).HasMaxLength(256);
            entity.Property(e => e.YoutubeLink)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("youtubeLink");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Contact_Id");

            entity.ToTable("Contact");

            entity.Property(e => e.Address).HasMaxLength(512);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Company).HasMaxLength(512);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tel)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id");

            entity.Property(e => e.Name)
                .HasMaxLength(250);

            entity.Property(e => e.Email)
                .HasMaxLength(250);

            entity.Property(e => e.Address)
                .HasMaxLength(250);

            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.UserName)
                .HasMaxLength(100);

            entity.Property(e => e.Active);
        });


        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Document_Id");

            entity.ToTable("Document");

            entity.Property(e => e.Code)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.File).HasMaxLength(512);
            entity.Property(e => e.Info).HasColumnType("ntext");
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Member).WithMany(p => p.Documents)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FRK_Document_MemberId");

            entity.HasOne(d => d.Type).WithMany(p => p.Documents)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Document_TypeId");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_DocumentType_Id");

            entity.ToTable("DocumentType", tb => tb.HasTrigger("tg_Update_DocumentTypeUser_DocumentType"));

            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<DocumentTypeUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_DocumentTypeUser_Id");

            entity.ToTable("DocumentTypeUser");

            entity.Property(e => e.Check).HasDefaultValue(true);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentTypeUsers)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_DocumentTypeUser_DocumentTypeId");

            entity.HasOne(d => d.User).WithMany(p => p.DocumentTypeUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_DocumentTypeUser_UserId");
        });

        modelBuilder.Entity<Enrollment>()
    .HasIndex(e => new { e.CustomerId, e.ProductId })
    .IsUnique();

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Customer)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CustomerId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Product)
            .WithMany(p => p.Enrollments)
            .HasForeignKey(e => e.ProductId);

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback");

            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Coso).HasMaxLength(250);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Lop).HasMaxLength(512);
            entity.Property(e => e.Mail)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Ngaysinh).HasMaxLength(512);
            entity.Property(e => e.Tel)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Ykien).HasMaxLength(400);
        });
         modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.HasKey(e => e.FacultyID);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Description)
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.Image)
                      .HasMaxLength(255);

                entity.Property(e => e.Active)
                      .HasDefaultValue(1);
            });
        modelBuilder.Entity<ProFaculty>(entity =>
        {
            entity.ToTable("ProFaculty");
            entity.HasKey(x => x.ProFacultyID);

            // FK -> Faculty
            entity.HasOne(x => x.Faculty)
                  .WithMany(f => f.ProFaculties)
                  .HasForeignKey(x => x.FacultyID)
                  .OnDelete(DeleteBehavior.Cascade);

            // FK -> Product
            entity.HasOne(x => x.Product)
                  .WithMany(p => p.ProFaculties)
                  .HasForeignKey(x => x.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Không cho trùng giảng viên + khóa học
            entity.HasIndex(x => new { x.ProductId, x.FacultyID })
                  .IsUnique();
        });
        modelBuilder.Entity<GroupLibrary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_GroupLibrary_Id");

            entity.ToTable("GroupLibrary", tb =>
                {
                    tb.HasTrigger("tg_Update_GroupLibraryUser_GroupLibrary");
                    tb.HasTrigger("tg_Update_Level_GroupLibrary");
                });

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(256);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tag).HasMaxLength(256);
        });

        modelBuilder.Entity<GroupLibraryUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_GroupLibraryUser_Id");

            entity.ToTable("GroupLibraryUser");

            entity.Property(e => e.Check).HasDefaultValue(true);

            entity.HasOne(d => d.GroupLibrary).WithMany(p => p.GroupLibraryUsers)
                .HasForeignKey(d => d.GroupLibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_GroupLibraryUser_GroupLibraryId");

            entity.HasOne(d => d.User).WithMany(p => p.GroupLibraryUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_GroupLibraryUser_UserId");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_GroupMember_Id");

            entity.ToTable("GroupMember");

            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<GroupNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_GroupNews_Id");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tg_Update_GroupNewsUser_GroupNews");
                    tb.HasTrigger("tg_Update_Level_GroupNews");
                });

            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Priority).HasDefaultValue(0);
            entity.Property(e => e.Tag).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<GroupNewsUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_GroupNewsUser_Id");

            entity.ToTable("GroupNewsUser");

            entity.Property(e => e.Check).HasDefaultValue(true);

            entity.HasOne(d => d.GroupNews).WithMany(p => p.GroupNewsUsers)
                .HasForeignKey(d => d.GroupNewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_GroupNewsUser_GroupNewsId");

            entity.HasOne(d => d.User).WithMany(p => p.GroupNewsUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_GroupNewsUser_UserId");
        });

        modelBuilder.Entity<GroupSupport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_GroupSupport_Id");

            entity.ToTable("GroupSupport");

            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Language_Id");

            entity.ToTable("Language");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Folder)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Library_Id");

            entity.ToTable("Library");

            entity.Property(e => e.File).HasMaxLength(512);
            entity.Property(e => e.Image).HasMaxLength(256);
            entity.Property(e => e.Info).HasMaxLength(100);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tag).HasMaxLength(256);

            entity.HasOne(d => d.GroupLibrary).WithMany(p => p.Libraries)
                .HasForeignKey(d => d.GroupLibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Library_GroupLibraryId");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Link_Id");

            entity.ToTable("Link");

            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Line1).HasMaxLength(512);
            entity.Property(e => e.Line2).HasMaxLength(512);
            entity.Property(e => e.Link1)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Link2)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<ListDangkyhocCackhoa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("List_dangkyhoc_cackhoa");

            entity.Property(e => e.Dienthoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Hoten).HasMaxLength(200);
            entity.Property(e => e.Khoa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Khoahoc)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Namsinh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ngaydangky).HasColumnType("datetime");
            entity.Property(e => e.Nguon)
                .HasMaxLength(300)
                .HasColumnName("nguon");
            entity.Property(e => e.Sinhvientruong).HasMaxLength(400);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Member_Id");

            entity.ToTable("Member");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.GroupMember).WithMany(p => p.Members)
                .HasForeignKey(d => d.GroupMemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Member_GroupMemberId");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Module_Id");

            entity.ToTable("Module", tb =>
                {
                    tb.HasTrigger("tg_Update_Level_Module");
                    tb.HasTrigger("tg_Update_Permission_Module");
                });

            entity.Property(e => e.Code).HasMaxLength(32);
            entity.Property(e => e.Image)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Page).HasMaxLength(256);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_News_Id");

            entity.Property(e => e.Content).HasMaxLength(912);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(556);
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.File)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.RegisterLink)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Tag).HasMaxLength(556);
            entity.Property(e => e.Tags).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(556);
            entity.Property(e => e.Video)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.GroupNews).WithMany(p => p.News)
                .HasForeignKey(d => d.GroupNewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_News_GroupNewsId");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Page_Id");

            entity.ToTable("Page", tb => tb.HasTrigger("tg_Update_Level_Page"));

            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Index).HasDefaultValue(0);
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Link)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tag).HasMaxLength(256);
            entity.Property(e => e.Target)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Permission_Id");

            entity.ToTable("Permission");

            entity.Property(e => e.Right).HasMaxLength(50);

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Permission_ModuleId");

            entity.HasOne(d => d.User).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Permission_UserId");
        });

        modelBuilder.Entity<PhieuDk>(entity =>
        {
            entity.ToTable("PhieuDK");

            entity.Property(e => e.Cmnd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CMND");
            entity.Property(e => e.Diachi).HasMaxLength(400);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Ghichu).HasMaxLength(200);
            entity.Property(e => e.Hoten).HasMaxLength(200);
            entity.Property(e => e.NganhDk)
                .HasMaxLength(200)
                .HasColumnName("NganhDK");
            entity.Property(e => e.NgayDk)
                .HasColumnType("datetime")
                .HasColumnName("NgayDK");
            entity.Property(e => e.Ngaycap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ngaysinh).HasColumnType("datetime");
            entity.Property(e => e.Noicap).HasMaxLength(200);
            entity.Property(e => e.PhuongthucTs)
                .HasMaxLength(300)
                .HasColumnName("PhuongthucTS");
            entity.Property(e => e.SoDt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SoDT");
            entity.Property(e => e.Tentruong).HasMaxLength(200);
            entity.Property(e => e.Tongdiem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Trinhdo).HasMaxLength(200);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Post_Id");

            entity.ToTable("Post");

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.Content).HasMaxLength(512);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(256);
            entity.Property(e => e.Website).HasMaxLength(256);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Product_Id");

            entity.ToTable("Product");

            entity.Property(e => e.Content).HasMaxLength(512);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Image)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Keyword).HasMaxLength(512);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tag).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(256);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Product_CategoryId");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Staff_Id");

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.BirthDate).HasMaxLength(50);
            entity.Property(e => e.Department).HasMaxLength(256);
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Fname)
                .HasMaxLength(128)
                .HasColumnName("FName");
            entity.Property(e => e.Job).HasMaxLength(128);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Letter).HasMaxLength(10);
            entity.Property(e => e.Lname)
                .HasMaxLength(128)
                .HasColumnName("LName");
            entity.Property(e => e.Mobile)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Position).HasMaxLength(128);
            entity.Property(e => e.Sex).HasMaxLength(20);
            entity.Property(e => e.Tel)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Support>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Support_Id");

            entity.ToTable("Support");

            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Nick)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Priority).HasDefaultValue(0);
            entity.Property(e => e.Tel).HasMaxLength(256);

            entity.HasOne(d => d.GroupSupport).WithMany(p => p.Supports)
                .HasForeignKey(d => d.GroupSupportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Support_GroupSupportId");
        });

        modelBuilder.Entity<TbCounter>(entity =>
        {
            entity.ToTable("tbCounter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ngaytao).HasColumnType("smalldatetime");
            entity.Property(e => e.Tenweb).HasMaxLength(200);
        });

        modelBuilder.Entity<TbGiatriDangky>(entity =>
        {
            entity.HasKey(e => e.Idgt);

            entity.ToTable("tbGiatriDangky");

            entity.Property(e => e.Idgt).HasColumnName("IDGT");
            entity.Property(e => e.Giatri).HasMaxLength(500);
            entity.Property(e => e.NgayDk)
                .HasColumnType("smalldatetime")
                .HasColumnName("NgayDK");

            entity.HasOne(d => d.IdttNavigation).WithMany(p => p.TbGiatriDangkies)
                .HasForeignKey(d => d.Idtt)
                .HasConstraintName("FK_tbGiatriDangky_tbTTdangky");
        });

        modelBuilder.Entity<TbSukien>(entity =>
        {
            entity.HasKey(e => e.MaSk);

            entity.ToTable("tbSukien");

            entity.Property(e => e.MaSk).HasColumnName("MaSK");
            entity.Property(e => e.Accid).HasColumnName("accid");
            entity.Property(e => e.Iviews).HasColumnName("iviews");
            entity.Property(e => e.Keyname)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("keyname");
            entity.Property(e => e.Metadescription)
                .HasMaxLength(1000)
                .HasColumnName("metadescription");
            entity.Property(e => e.Metakeyword)
                .HasMaxLength(1000)
                .HasColumnName("metakeyword");
            entity.Property(e => e.Metatitle)
                .HasMaxLength(1000)
                .HasColumnName("metatitle");
            entity.Property(e => e.NguonLink).HasMaxLength(300);
            entity.Property(e => e.Noidung).HasColumnType("ntext");
            entity.Property(e => e.Tag)
                .HasMaxLength(1000)
                .HasColumnName("tag");
            entity.Property(e => e.Tensukien).HasMaxLength(300);
        });

        modelBuilder.Entity<TbTtdangky>(entity =>
        {
            entity.HasKey(e => e.Idtt);

            entity.ToTable("tbTTdangky");

            entity.Property(e => e.MaSk).HasColumnName("MaSK");
            entity.Property(e => e.Nhan).HasMaxLength(300);

            entity.HasOne(d => d.MaSkNavigation).WithMany(p => p.TbTtdangkies)
                .HasForeignKey(d => d.MaSk)
                .HasConstraintName("FK_tbTTdangky_tbSukien");
        });

        modelBuilder.Entity<TbValueCombo>(entity =>
        {
            entity.ToTable("tbValueCombo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Giatri).HasMaxLength(400);

            entity.HasOne(d => d.IdttNavigation).WithMany(p => p.TbValueCombos)
                .HasForeignKey(d => d.Idtt)
                .HasConstraintName("FK_tbValueCombo_tbTTdangky");
        });

        modelBuilder.Entity<Toggle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Toggle_Id");

            entity.ToTable("Toggle");

            entity.Property(e => e.Detail).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.News).WithMany(p => p.Toggles)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_Toggle_NewsId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_User_ID");

            entity.ToTable("User", tb =>
                {
                    tb.HasTrigger("tg_Update_DocumentTypeUser_User");
                    tb.HasTrigger("tg_Update_GroupLibraryUser_User");
                    tb.HasTrigger("tg_Update_GroupNewsUser_User");
                    tb.HasTrigger("tg_Update_Level_User");
                    tb.HasTrigger("tg_Update_Permission_User");
                });

            entity.Property(e => e.Level)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.ToTable("UserGroup");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_Vote_Id");

            entity.ToTable("Vote");

            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<VoteDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRK_VoteDetail_Id");

            entity.ToTable("VoteDetail");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Ip)
                .HasMaxLength(256)
                .HasColumnName("IP");

            entity.HasOne(d => d.News).WithMany(p => p.VoteDetails)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_VoteDetail_NewsId");

            entity.HasOne(d => d.Vote).WithMany(p => p.VoteDetails)
                .HasForeignKey(d => d.VoteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FRK_VoteDetail_VoteId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
