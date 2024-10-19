using System;
using System.Collections.Generic;

namespace APIDevBACK.Modelo;

public partial class Ccloglogin
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public int? Extension { get; set; }

    public int? TipoMov { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual CcUser? User { get; set; }
}
