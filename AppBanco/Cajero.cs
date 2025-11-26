using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppBanco.Models;


namespace AppBanco.Services
{
    /// <summary>
    /// Orquesta sesión: valida PIN, muestra menú y ejecuta operaciones.
    /// </summary>
    public class Cajero
    {
        private readonly string _pinCorrecto;
        private readonly int _intentosMaximos;
        private readonly CuentaBancaria _cuenta;

        public Cajero(CuentaBancaria cuenta, string pinCorrecto, int intentosMaximos)
        {
            _cuenta = cuenta;
            _pinCorrecto = string.IsNullOrEmpty(pinCorrecto) ? "1234" : pinCorrecto;
            _intentosMaximos = intentosMaximos < 1 ? 3 : intentosMaximos;
        }

        public void Iniciar()
        {
            if (!ValidarPin())
            {
                Console.WriteLine("⚠️ Acceso bloqueado por intentos fallidos.");
                return;
            }
            MenuPrincipal();
        }

        private bool ValidarPin()
        {
            int intentos = 0;
            while (intentos < _intentosMaximos)
            {
                Console.Write("Ingrese su PIN: ");
                var pin = Console.ReadLine();
                if (pin == _pinCorrecto)
                {
                    Console.WriteLine("✅ Acceso concedido.\n");
                    return true;
                }
                intentos++;
                Console.WriteLine("PIN incorrecto. Intento " + intentos + "/" + _intentosMaximos + ".\n");
            }
            return false;
        }

        private void MenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("====== MENÚ PRINCIPAL ======");
                Console.WriteLine("1) Consultar saldo");
                Console.WriteLine("2) Depositar dinero");
                Console.WriteLine("3) Retirar dinero");
                Console.WriteLine("4) Ver movimientos");
                Console.WriteLine("5) Ver resumen de cuenta");
                Console.WriteLine("6) Salir");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine(string.Format("Saldo actual: ${0:F2}\n", _cuenta.Saldo));
                        break;

                    case "2":
                        decimal montoDep;
                        if (LeerMonto("monto a depositar", out montoDep))
                        {
                            string msgDep;
                            _cuenta.Depositar(montoDep, out msgDep);
                            Console.WriteLine(msgDep + "\n");
                        }
                        break;

                    case "3":
                        decimal montoRet;
                        if (LeerMonto("monto a retirar", out montoRet))
                        {
                            string msgRet;
                            _cuenta.Retirar(montoRet, out msgRet);
                            Console.WriteLine(msgRet + "\n");
                        }
                        break;

                    case "4":
                        if (_cuenta.Movimientos.Count == 0)
                        {
                            Console.WriteLine("No hay movimientos registrados.\n");
                        }
                        else
                        {
                            foreach (var m in _cuenta.Movimientos)
                                Console.WriteLine(m);
                            Console.WriteLine();
                        }
                        break;

                    case "5":
                        Console.WriteLine(_cuenta.Resumen());
                        Console.WriteLine();
                        break;

                    case "6":
                        Console.WriteLine("Gracias por usar AppBanco. ¡Hasta luego!");
                        return;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.\n");
                        break;
                }
            }
        }

        private bool LeerMonto(string etiqueta, out decimal monto)
        {
            Console.Write("Ingrese " + etiqueta + ": ");
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out monto) && monto > 0)
                return true;

            Console.WriteLine("Monto inválido.\n");
            return false;
        }
    }
}
