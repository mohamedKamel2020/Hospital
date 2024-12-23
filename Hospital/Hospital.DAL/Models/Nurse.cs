namespace Hospital.DAL.Models;

public partial class Nurse
{

    public int Nurseid { get; set; }

    public string Nursename { get; set; } = null!;

    public int Wardid { get; set; }

    public decimal Salary { get; set; }

    public char Sex { get; set; }

    public int? Apartmentblockno { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public int Age { get; set; }

    public string Shift { get; set; } = null!;

    public int? SupervisorId { get; set; }

    public List<string>? Phone { get; set; }

    public virtual ICollection<Nurse> InverseSupervisor { get; set; } = new List<Nurse>();

    public virtual Nurse? Supervisor { get; set; }

    public virtual Ward Ward { get; set; } = null!;
}
