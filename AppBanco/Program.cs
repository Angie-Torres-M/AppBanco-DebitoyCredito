using System;
using AppBanco.Models;
using AppBanco.Services;

namespace AppBanco
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=== AppBanco ===");
            Console.WriteLine("1) Cargar cuenta guardada");
            Console.WriteLine("2) Crear cuenta nueva");
            Console.Write("Elija una opción: ");
            var opcion = Console.ReadLine();

            CuentaBancaria cuenta = null;

            if (opcion == "1")
            {
                cuenta = BancoStore.Cargar();
                if (cuenta == null)
                {
                    Console.WriteLine("\nNo hay cuenta guardada. Cree una nueva.\n");
                    cuenta = CrearCuentaInteractiva();
                    BancoStore.Guardar(cuenta);
                    Console.WriteLine("\n✅ Cuenta guardada para próximas ejecuciones.\n");
                }
                else
                {
                    Console.WriteLine("\n✅ Cuenta cargada desde archivo.\n");
                }
            }
            else if (opcion == "2")
            {
                cuenta = CrearCuentaInteractiva();
                BancoStore.Guardar(cuenta);
                Console.WriteLine("\n✅ Cuenta guardada para próximas ejecuciones.\n");
            }
            else
            {
                Console.WriteLine("\nOpción no válida. Saliendo...");
                return;
            }

            // PIN por defecto. Si quieres, también lo pedimos y guardamos.
            var cajero = new Cajero(cuenta, "1234", 3);
            cajero.Iniciar();

            // Al terminar, podríamos volver a guardar por si hubo movimientos/saldo
            BancoStore.Guardar(cuenta);
        }

        /// <summary>
        /// Pide datos por consola y crea una cuenta Débito o Crédito.
        /// </summary>
        private static CuentaBancaria CrearCuentaInteractiva()
        {
            Console.WriteLine("\n=== Crear cuenta ===");
            Console.WriteLine("Tipo de cuenta:");
            Console.WriteLine("1) Débito");
            Console.WriteLine("2) Crédito");
            Console.Write("Opción: ");
            var tipo = Console.ReadLine();

            // Datos comunes
            Console.Write("ID (número): ");
            int id = LeerEnteroPositivo();

            Console.Write("Número de cuenta (número): ");
            int numeroCuenta = LeerEnteroPositivo();

            Console.Write("Número de cliente (texto): ");
            var numeroCliente = Console.ReadLine();
            if (string.IsNullOrEmpty(numeroCliente)) numeroCliente = "C001";

            Console.Write("Nombre del cliente: ");
            var nombreCliente = Console.ReadLine();
            if (string.IsNullOrEmpty(nombreCliente)) nombreCliente = "Cliente Demo";

            Console.Write("Fecha de apertura (texto, ej. 10/01/2025): ");
            var fechaApertura = Console.ReadLine();
            if (string.IsNullOrEmpty(fechaApertura)) fechaApertura = "10/01/2025";

            if (tipo == "1")
            {
                Console.Write("Saldo inicial (decimal): ");
                decimal saldo = LeerDecimalPositivoOCero();

                Console.Write("Monto mínimo requerido (decimal, ej. 0): ");
                decimal minimo = LeerDecimalPositivoOCero();

                return new CuentaDebito(
                    id: id, saldo: saldo, numeroCuenta: numeroCuenta,
                    numeroCliente: numeroCliente, nombreCliente: nombreCliente,
                    fechaApertura: fechaApertura, montoMinimo: minimo
                );
            }
            else if (tipo == "2")
            {
                Console.Write("Saldo inicial (decimal, puede ser 0): ");
                decimal saldo = LeerDecimalPositivoOCero();

                Console.Write("Fecha de corte (texto, ej. 28/10/2025): ");
                var fechaCorte = Console.ReadLine();
                if (string.IsNullOrEmpty(fechaCorte)) fechaCorte = "28/10/2025";

                Console.Write("Tasa (decimal, ej. 0.035): ");
                decimal tasa = LeerDecimalPositivoOCero();

                Console.Write("Límite de crédito (decimal, ej. 10000): ");
                decimal limite = LeerDecimalPositivoOCero();

                return new CuentaCredito(
                    id: id, saldo: saldo, numeroCuenta: numeroCuenta,
                    numeroCliente: numeroCliente, nombreCliente: nombreCliente,
                    fechaApertura: fechaApertura, fechaCorte: fechaCorte,
                    tasa: tasa, limite: limite
                );
            }
            else
            {
                Console.WriteLine("Tipo inválido. Se creará una cuenta Débito por defecto.");
                return new CuentaDebito(
                    id: id, saldo: 0m, numeroCuenta: numeroCuenta,
                    numeroCliente: numeroCliente, nombreCliente: nombreCliente,
                    fechaApertura: fechaApertura, montoMinimo: 0m
                );
            }
        }

        private static int LeerEnteroPositivo()
        {
            while (true)
            {
                var s = Console.ReadLine();
                int n;
                if (int.TryParse(s, out n) && n > 0) return n;
                Console.Write("Valor inválido. Intente otra vez: ");
            }
        }

        private static decimal LeerDecimalPositivoOCero()
        {
            while (true)
            {
                var s = Console.ReadLine();
                decimal d;
                if (decimal.TryParse(s, out d) && d >= 0) return d;
                Console.Write("Valor inválido. Intente otra vez: ");
            }
        }
    }
}
