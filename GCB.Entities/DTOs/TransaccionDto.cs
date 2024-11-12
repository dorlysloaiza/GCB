public class TransaccionDto
{
    public DateTimeOffset FechaCreacion { get; set; }
    public DateTimeOffset FechaActualizacion { get; set; }
    public string NombreUsuario { get; set; }
    public bool Activo { get; set; } = false;

    public Guid IdCuentaBancaria { get; set; } // Foreign key to CuentaBancaria
    public string NumeroCuenta { get; set; }
    public string NombreBanco { get; set; }
      
    public string Referencia { get; set; }
    public string Descripcion { get; set; }
    public double Monto { get; set; }
    public string Moneda { get; set; }
    public double TipoCambio { get; set; }

    public Guid IdCategoria { get; set; } // Foreign key to Categoria
    public string DescripcionCategoria { get; set; }
    public string TipoCategoria { get; set; }
    public List<AdjuntoGCB> Adjuntos { get; set; } // List of attachments

}