using GCB.Entities.Modelos;

public class CuentaBanco:EntidadBase
{

    public string NumeroCuenta { get; set; }
    public Guid BancoId { get; set; }
    public string Moneda { get; set; }
    public decimal BalanceInicial { get; set; }
    public decimal BalanceActual { get; set; }
    public string Titular { get; set; }
    public DateTime UltimaFechaConciliacion { get; set; }
    public DateTime FechaBalanceInicial { get; set; }
    // Navigation property
    public List<Transaccion> Transacciones { get; set; }
    public Banco Banco { get; set; }
}