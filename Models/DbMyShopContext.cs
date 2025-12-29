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

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dealer> Dealers { get; set; }

    public virtual DbSet<GroupNews> GroupNews { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductSpec> ProductSpecs { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__advertis__3214EC079BF6AF2C");

            entity.ToTable("advertise");

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

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__branches__3213E83F82F95A1E");

            entity.ToTable("branches");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnName("phone");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F9AF502C7");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Slug, "UQ__categori__32DD1E4CEB8EB851").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fk_categories_parent");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__config__3214EC072A02F346");

            entity.ToTable("config");

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
            entity.HasKey(e => e.Id).HasName("PK__contacts__3213E83FFF2CA1B5");

            entity.ToTable("contacts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .HasColumnName("message");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB858C7A8236");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Dealer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__dealers__3213E83F447EEEE8");

            entity.ToTable("dealers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(255)
                .HasColumnName("contact_person");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnName("phone");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
        });

        modelBuilder.Entity<GroupNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GroupNew__3214EC070EDC241F");
            entity.ToTable("GroupNews", tb =>
            {
                tb.HasTrigger("tg_Update_Level_GroupNews"); // 👈 TÊN TRIGGER THẬT TRONG SQL
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
            entity.Property(e => e.Tag).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__news__3213E83F96034635");

            entity.ToTable("news");

            entity.HasIndex(e => e.Slug, "UQ__news__32DD1E4CE26D4397").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PostedById).HasColumnName("posted_by_id");
            entity.Property(e => e.Content)
                .HasColumnType("ntext")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Excerpt).HasColumnName("excerpt");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("datetime")
                .HasColumnName("published_at");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(512)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.AuthorName)
                            .HasMaxLength(255)
                            .HasColumnName("author_name");
            entity.Property(e => e.Hinhanh)
               .HasMaxLength(300);
            entity.HasOne(d => d.PostedBy).WithMany(p => p.News)
                .HasForeignKey(d => d.PostedById)
                .HasConstraintName("fk_news_author");

            entity.HasOne(d => d.Group).WithMany(p => p.News)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("fk_news_group");
        });



        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83F737BC3A5");

            entity.ToTable("orders");

            entity.HasIndex(e => e.OrderCode, "UQ__orders__99D12D3F5100E481").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("discount_amount");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.OrderCode)
                .HasMaxLength(50)
                .HasColumnName("order_code");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
            entity.Property(e => e.ShippingFee)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("shipping_fee");
            entity.Property(e => e.SubtotalAmount)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("subtotal_amount");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_orders_customer");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_de__3213E83F850EE356");

            entity.ToTable("order_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_orderdetails_order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_orderdetails_product");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id)
                  .HasName("PK__page__3214EC079E814B9E");

            entity.ToTable("page", tb =>
            {
                tb.HasTrigger("tg_Update_Level_page"); // 👈 TÊN TRIGGER THẬT TRONG SQL
            });

            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Detail).HasColumnType("ntext");
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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83F36771480");

            entity.ToTable("products");

            entity.HasIndex(e => e.Slug, "UQ__products__32DD1E4CA31D4908").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DealerId).HasColumnName("dealer_id");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("sale_price");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_products_category");

            entity.HasOne(d => d.Dealer).WithMany(p => p.Products)
                .HasForeignKey(d => d.DealerId)
                .HasConstraintName("fk_products_dealer");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FA2573AEF");

            entity.ToTable("product_images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_product_images_product");
        });

        modelBuilder.Entity<ProductSpec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FC2F42CC2");

            entity.ToTable("product_specs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SpecName)
                .HasMaxLength(255)
                .HasColumnName("spec_name");
            entity.Property(e => e.SpecValue)
                .HasMaxLength(255)
                .HasColumnName("spec_value");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSpecs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_productspec_product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F219CC15B");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164BAF9505E").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC5729D0D7C74").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
