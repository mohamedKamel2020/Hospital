using System;
using System.Collections.Generic;

namespace Hospital.DAL.Models;

public partial class Testreporttreatment
{
    public int Docid { get; set; }

    public int Patid { get; set; }

    public string Treatments { get; set; } = null!;

    public virtual Doctorwritetestreport Doctorwritetestreport { get; set; } = null!;
}
