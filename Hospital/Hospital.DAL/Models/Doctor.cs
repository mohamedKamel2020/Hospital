namespace Hospital.DAL.Models;

public partial class Doctor
{

    public int Doctorid { get; set; }

    public string Doctorname { get; set; } = null!;

    public int Depid { get; set; }

    public decimal Salary { get; set; }

    public char Sex { get; set; }

    public int? Apartmentblockno { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public int Age { get; set; }

    public string Shift { get; set; } = null!;

    public string MedicalSpecialization { get; set; } = null!;

    public List<string>? Phone { get; set; }

    public virtual Depatment Dep { get; set; } = null!;

    public virtual ICollection<Doctorwritetestreport> Doctorwritetestreports { get; set; } = new List<Doctorwritetestreport>();
}
