﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class Name
{
    [Key]
    [Column("nameID")]
    [StringLength(10)]
    [Unicode(false)]
    public string NameId { get; set; } = null!;

    [Column("primaryName")]
    [StringLength(125)]
    [Unicode(false)]
    public string? PrimaryName { get; set; }

    [Column("birthYear")]
    public int? BirthYear { get; set; }

    [Column("deathYear")]
    public int? DeathYear { get; set; }

    [Column("primaryProfession")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PrimaryProfession { get; set; }

    [ForeignKey("NameId")]
    [InverseProperty("Names")]
    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();

    [ForeignKey("NameId")]
    [InverseProperty("NamesNavigation")]
    public virtual ICollection<Title> TitlesNavigation { get; set; } = new List<Title>();
}
