using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestcontainersExample.Data.Entities;

public record Book : IEntityTypeConfiguration<Book>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    
    public virtual Author Author { get; set; }
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
    }
}