﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProFind.Models.Generated;

[Table("__efmigrationshistory")]
public partial class Efmigrationshistory
{
    [Key]
    [StringLength(150)]
    public string MigrationId { get; set; } = null!;
    [StringLength(32)]
    public string ProductVersion { get; set; } = null!;
}