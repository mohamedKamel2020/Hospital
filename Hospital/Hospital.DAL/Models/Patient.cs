using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL.Models;

public partial class Patient
{

    public int Patientid { get; set; }
    [Required(ErrorMessage = "Patient Name is Required!")]
    public string Patientname { get; set; } = null!;

    public decimal Age { get; set; }

    public string? Phone { get; set; }

    public char Sex { get; set; }

    public int? Apartmentblockno { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public int Wardid { get; set; }

    public DateOnly? Enterdate { get; set; }

    public DateOnly? Exitdate { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Doctorwritetestreport> Doctorwritetestreports { get; set; } = new List<Doctorwritetestreport>();

    public virtual ICollection<Medicalrecord> Medicalrecords { get; set; } = new List<Medicalrecord>();

    public virtual Ward Ward { get; set; } = null!;
}
