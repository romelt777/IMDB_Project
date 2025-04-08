using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[PrimaryKey("TitleId", "NameId")]
public partial class Director
{
    [Key]
    [Column("titleID")]
    [StringLength(10)]
    [Unicode(false)]
    public string TitleId { get; set; } = null!;

    [Key]
    [Column("nameID")]
    [StringLength(10)]
    [Unicode(false)]
    public string NameId { get; set; } = null!;

    [ForeignKey("TitleId")]
    [InverseProperty("Directors")]
    public virtual Title Title { get; set; } = null!;
}
