namespace Hospital.DAL.Models;

public partial class Recordtreatment
{
    public int Recid { get; set; }

    public string Treatment { get; set; } = null!;

    public virtual Medicalrecord Rec { get; set; } = null!;
}
