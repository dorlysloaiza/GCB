public class Transaccion:EntidadBase
{
  
    public Guid IdCuentaBancaria { get; set; } // Foreign key to CuentaBancaria
    public string Referencia { get; set; }
    public string Descripción { get; set; }
    public double Monto { get; set; }
    public string Moneda { get; set; }
    public double TipoCambio { get; set; }
    public Guid IdCategoria  { get; set; } // Foreign key to Categoria
    public Categoria Categoria { get; set; } // Navigation property
    // Navigation property
    public CuentaBanco CuentaBancaria { get; set; }
    public List<AdjuntoGCB> Adjuntos { get; set; } // List of attachments
}