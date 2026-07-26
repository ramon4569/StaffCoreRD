namespace StaffCoreRD.Models;

public class ResumenDepartamentoViewModel
{
    public string Departamento { get; set; }
    public int CantidadEmpleados { get; set; }
    public decimal TotalSalarios { get; set; }
    public decimal PromedioSalario { get; set; }
}