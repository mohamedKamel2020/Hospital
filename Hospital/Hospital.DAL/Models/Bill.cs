namespace Hospital.DAL.Models;

public partial class Bill
{

    public int Billid { get; set; }

    public int Patid { get; set; }

    public int Accid { get; set; }

    public decimal Totalbill { get; set; }

    public bool? Hasinsurance { get; set; }

    public virtual Accountant Acc { get; set; } = null!;

    public virtual Patient Pat { get; set; } = null!;
}
