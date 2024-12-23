namespace Hospital.DAL.Models;

public partial class Medicalrecord
{

    public int Recordid { get; set; }

    public DateOnly Recorddate { get; set; }

    public int Patid { get; set; }

    public string? Diagnosis { get; set; }

    public virtual Patient Pat { get; set; } = null!;


}
