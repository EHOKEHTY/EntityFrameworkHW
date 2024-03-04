using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContextLibrery;

public partial class LibreryContext : DbContext
{
    public LibreryContext()
    {
    }

    public LibreryContext(DbContextOptions<LibreryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Librerian> Librerians { get; set; }

    public virtual DbSet<PublisherType> PublisherTypes { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<RentBook> RentBooks { get; set; }

    public virtual DbSet<RentHistory> RentHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6DGIP52\\SQLEXPRESS;Initial Catalog=Librery;Integrated Security=True; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.PublisherType).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_PublisherType");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.AuthorId });

            entity.ToTable("BookAuthor");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookAuthor_Author");

            entity.HasOne(d => d.BookNavigation).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookAuthor_Book");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.ToTable("DocumentType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Librerian>(entity =>
        {
            entity.ToTable("Librerian");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<PublisherType>(entity =>
        {
            entity.ToTable("PublisherType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.ToTable("Reader");

            entity.Property(e => e.DocumentNumber).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Readers)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reader_DocumentType");
        });

        modelBuilder.Entity<RentBook>(entity =>
        {
            entity.HasKey(e => e.RentId);

            entity.ToTable("RentBook");

            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.RentDate).HasColumnType("date");
            entity.Property(e => e.ReturnDate).HasColumnType("date");

            entity.HasOne(d => d.Book).WithMany(p => p.RentBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentBook_Book");

            entity.HasOne(d => d.Reader).WithMany(p => p.RentBooks)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentBook_Reader");
        });

        modelBuilder.Entity<RentHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId);

            entity.ToTable("RentHistory");

            entity.HasOne(d => d.Reader).WithMany(p => p.RentHistories)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentHistory_Reader");

            entity.HasOne(d => d.Rent).WithMany(p => p.RentHistories)
                .HasForeignKey(d => d.RentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentHistory_RentBook");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
