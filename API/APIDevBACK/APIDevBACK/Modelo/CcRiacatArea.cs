using System;
using System.Collections.Generic;

namespace APIDevBACK.Modelo;

public partial class CcRiacatArea
{
    public int Idarea { get; set; }

    public string? AreaName { get; set; }

    public bool? StatusArea { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<CcUser> CcUsers { get; set; } = new List<CcUser>();
}
