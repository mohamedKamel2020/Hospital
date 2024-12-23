using System;
using System.Collections.Generic;

namespace Hospital.DAL.Models;

public partial class Doctorwritetestreport
{
    public int Docid { get; set; }

    public int Patid { get; set; }

    public string Diagnosis { get; set; } = null!;

    public DateOnly? ReportDate { get; set; }

    public virtual Doctor Doc { get; set; } = null!;

    public virtual Patient Pat { get; set; } = null!;

    public virtual Testreporttreatment? Testreporttreatment { get; set; }
}
