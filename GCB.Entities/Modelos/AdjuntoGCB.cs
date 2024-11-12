public class AdjuntoGCB:EntidadBase
{
  
    public Guid IdTransaccion { get; set; } // Transaccion
    public string TipoEntidad { get; set; } // ingreso/Gasto
    public string NombreArchivo { get; set; }   
    public byte[] ContenidoArchivo { get; set; } 

}