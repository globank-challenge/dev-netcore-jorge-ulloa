namespace OpBancarias.Data.Test
{
    public static class OpBancariasModelsMockery
    {
        public static Models.Cuenta GetMockedCuenta()
        {
            var randomGenerator =  new Random();

            Models.Cuenta cuenta = new Models.Cuenta()
            {
                ClienteId = 1,
                Numero = randomGenerator.Next(1000000, 9999999).ToString(),
                Tipo = 1,
                SaldoInicial = 100,
                EstadoActivo = true,
             };

            return cuenta;
        }

        public static Models.Cliente GetMockedCliente()
        {
            var randomGenerator = new Random();

            Models.Cliente cliente = new()
            {
                Identificacion = randomGenerator.Next(1000000, 9999999).ToString(),
                Apellido = "Doe",
                Nombre = "John",
                Direccion = "No Number Street",
                Edad = 23,
                UserName = "john.doe." + randomGenerator.Next(1000000, 9999999).ToString(),
                Password = "MyP@ssword",
                Telefono = "999999999",
                EstadoActivo = true,
            };

            return cliente;
        }

        public static Models.Movimiento GetMockedMovimiento()
        {
            Models.Movimiento movimiento = new()
            {
                CuentaId = 1,
                Saldo  = 10,
                Tipo = 1,
                Valor = 10,
                Fecha = DateTime.Now,
            };

            return movimiento;
        }
    }
}
