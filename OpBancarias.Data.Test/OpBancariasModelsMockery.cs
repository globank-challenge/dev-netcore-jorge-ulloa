namespace OpBancarias.Data.Test
{
    public static class OpBancariasModelsMockery
    {
        public static Models.Cuenta GetMockedCuenta()
        {
            Models.Cuenta cuenta = new Models.Cuenta()
            {
                ClienteId = 1,
                Numero = "123456",
                Tipo = 1,
                SaldoInicial = 100,
                EstadoActivo = true,
             };

            return cuenta;
        }

        public static Models.Cliente GetMockedCliente()
        {
            Models.Cliente cliente = new Models.Cliente()
            {
                Identificacion = "1783456789",
                Apellido = "Doe",
                Nombre = "John",
                Direccion = "No Number Street",
                Edad = 23,
                UserName = "john.doe",
                Password = "MyP@ssword",
                Telefono = "999999999",
                EstadoActivo = true,
            };

            return cliente;
        }

        public static Models.Movimiento GetMockedMovimiento()
        {
            Models.Movimiento movimiento = new Models.Movimiento()
            {
                CuentaId = 4,
                Saldo  = 10,
                Tipo = 1,
                Valor = 10,
                Fecha = DateTime.Now,
            };

            return movimiento;
        }
    }
}
