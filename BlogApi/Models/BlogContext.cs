using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Posts> Posts { get; set; }

    public virtual DbSet<Tags> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B5A22E52D");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Posts>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA126018F8958BA7");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Posts_Categories");

            entity.HasMany(d => d.Tag).WithMany(p => p.Post)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTags",
                    r => r.HasOne<Tags>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK__PostTags__TagId__403A8C7D"),
                    l => l.HasOne<Posts>().WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK__PostTags__PostId__3F466844"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("PK__PostTags__7C45AF829910CCF4");
                    });
        });

        modelBuilder.Entity<Tags>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CF9ACB56F76EA");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
