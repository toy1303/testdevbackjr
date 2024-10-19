using System;
using System.Collections.Generic;

namespace APIDevBACK.Modelo;

public partial class CcUser
{
    public int UserId { get; set; }

    public string? Login { get; set; }

    public string? Nombres { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Password { get; set; }

    public int? TipoUserId { get; set; }

    public int? Status { get; set; }

    public DateTime? FCreate { get; set; }

    public int? Idarea { get; set; }

    public DateTime? LastLoginAttempt { get; set; }

    public virtual ICollection<Ccloglogin> Ccloglogins { get; set; } = new List<Ccloglogin>();

    public virtual CcRiacatArea? IdareaNavigation { get; set; }
}
