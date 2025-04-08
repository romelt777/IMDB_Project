using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class Title
{
    [Key]
    [Column("titleID")]
    [StringLength(10)]
    [Unicode(false)]
    public string TitleId { get; set; } = null!;

    [Column("titleType")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TitleType { get; set; }

    [Column("primaryTitle")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PrimaryTitle { get; set; }

    [Column("originalTitle")]
    [StringLength(255)]
    [Unicode(false)]
    public string? OriginalTitle { get; set; }

    [Column("isAdult")]
    public bool? IsAdult { get; set; }

    [Column("startYear")]
    public short? StartYear { get; set; }

    [Column("endYear")]
    public short? EndYear { get; set; }

    [Column("runtimeMinutes")]
    public short? RuntimeMinutes { get; set; }

    [InverseProperty("Title")]
    public virtual ICollection<Director> Directors { get; set; } = new List<Director>();

    [InverseProperty("ParentTitle")]
    public virtual ICollection<Episode> EpisodeParentTitles { get; set; } = new List<Episode>();

    [InverseProperty("Title")]
    public virtual Episode? EpisodeTitle { get; set; }

    [InverseProperty("Title")]
    public virtual Rating? Rating { get; set; }

    [InverseProperty("Title")]
    public virtual ICollection<Writer> Writers { get; set; } = new List<Writer>();
}
