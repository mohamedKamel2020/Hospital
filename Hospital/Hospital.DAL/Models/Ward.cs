using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.DAL.Models;

public partial class Ward
{
    public int Wardid { get; set; }

    public int Capacity { get; set; }

    public int Depatmentno { get; set; }

    public virtual Depatment DepatmentnoNavigation { get; set; } = null!;

    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    [NotMapped]
    public int Availability { get { return Capacity - (Patients?.Count ?? 0); } }
}
