using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public partial class ImdbContext : DbContext
{
    public ImdbContext()
    {
    }

    public ImdbContext(DbContextOptions<ImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=IMDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasOne(d => d.ParentTitle).WithMany(p => p.EpisodeParentTitles).HasConstraintName("FK_Episodes_Titles1");

            entity.HasOne(d => d.Title).WithOne(p => p.EpisodeTitle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Episodes_Titles");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_Title_Genres");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasOne(d => d.Title).WithOne(p => p.Rating)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Titles");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasMany(d => d.Names).WithMany(p => p.Titles)
                .UsingEntity<Dictionary<string, object>>(
                    "Director",
                    r => r.HasOne<Name>().WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Directors_Names"),
                    l => l.HasOne<Title>().WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Directors_Titles1"),
                    j =>
                    {
                        j.HasKey("TitleId", "NameId");
                        j.ToTable("Directors");
                        j.IndexerProperty<string>("TitleId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("titleID");
                        j.IndexerProperty<string>("NameId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("nameID");
                    });

            entity.HasMany(d => d.NamesNavigation).WithMany(p => p.TitlesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Writer",
                    r => r.HasOne<Name>().WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Writers_Names"),
                    l => l.HasOne<Title>().WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Writers_Titles"),
                    j =>
                    {
                        j.HasKey("TitleId", "NameId");
                        j.ToTable("Writers");
                        j.IndexerProperty<string>("TitleId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("titleID");
                        j.IndexerProperty<string>("NameId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("nameID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
