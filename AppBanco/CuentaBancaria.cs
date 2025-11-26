using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace AppBanco.Models
{

    /// <summary>
    /// Clase base abstracta con operaciones comunes.
    /// </summary>
    public abstract class CuentaBancaria
    {
        public int Id { get; protected set; }
        public string NumeroCliente { get; protected set; }
        public string NombreCliente { get; protected set; }
        public int NumeroCuenta { get; protected set; }
        public string FechaApertura { get; protected set; }
        public decimal Saldo { get; protected set; }

        // IMPORTANTE: Movimiento debe ser PUBLIC o da "incoherencia de accesibilidad"
        protected readonly List<Movimiento> _movimientos = new List<Movimiento>();
        public ReadOnlyCollection<Movimiento> Movimientos
        {
            get { return _movimientos.AsReadOnly(); }
        }

        protected CuentaBancaria(
            int id, decimal saldo, int numeroCuenta,
            string numeroCliente, string nombreCliente, string fechaApertura)
        {
            Id = id;
            Saldo = saldo < 0 ? 0 : saldo;
            NumeroCuenta = numeroCuenta;
            NumeroCliente = numeroCliente ?? "";
            NombreCliente = nombreCliente ?? "";
            FechaApertura = fechaApertura ?? "";
        }

        public virtual bool Depositar(decimal monto, out string mensaje)
        {
            if (monto <= 0)
            {
                mensaje = "El monto a depositar debe ser mayor que 0.";
                _movimientos.Add(new Movimiento("Error", monto, mensaje));
                return false;
            }

            Saldo += monto;
            mensaje = string.Format("Depósito exitoso. Nuevo saldo: ${0:F2}", Saldo);
            _movimientos.Add(new Movimiento("Depósito", monto, mensaje));
            return true;
        }

        public abstract bool Retirar(decimal monto, out string mensaje);

        public virtual string Resumen()
        {
            return
            "-------------------------------\n" +
            "ID: " + Id + "\n" +
            "Cliente: " + NombreCliente + " (" + NumeroCliente + ")\n" +
            "Cuenta: " + NumeroCuenta + "\n" +
                string.Format("Saldo: ${0:F2}\n", Saldo) +
            "Apertura: " + FechaApertura;
        }
    }

}
