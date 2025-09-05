using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsCatalog.Domain.Models;

[Table("cities", Schema = "catalog")]
public class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("province_id")]
    public int ProvinceId { get; set; }

    [Required]
    [Column("code")]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("status")]
    public bool Status { get; set; } = true;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProvinceId")]
    public virtual Province Province { get; set; } = null!;
}