namespace Hospital.DAL.Models;

public partial class Accountant
{

    public int Accountantid { get; set; }

    public string Phone { get; set; } = null!;

    public decimal Salary { get; set; }

    public char Sex { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string Shift { get; set; } = null!;

    public int Age { get; set; }

    public string Accountantname { get; set; } = null!;

    public int? ApartmentBlockNo { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
