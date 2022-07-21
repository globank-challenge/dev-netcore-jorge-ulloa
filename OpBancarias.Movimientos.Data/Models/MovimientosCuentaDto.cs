namespace OpBancarias.Data.Models
{
    public class MovimientosCuentaDto
    {
        public string? NombreCliente { get; set; }
        public string? Numero { get; set; }
        public int Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool? EstadoActivo { get; set; }
        public List<Movimiento>? Movimientos { get; set; }
    }
}