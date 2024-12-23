namespace Hospital.DAL.Models;

public partial class Depatment
{

    public int Departmentid { get; set; }

    public string Departmentname { get; set; } = null!;
    public string Departmentlocation { get; set; } = null!;
    public string fileicon { get; set; } = null;
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
