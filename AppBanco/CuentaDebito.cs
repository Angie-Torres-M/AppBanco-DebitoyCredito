using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBanco.Models
{
    /// <summary>
    /// Cuenta de Débito: no permite retirar más que el saldo.
    /// </summary>
    public class CuentaDebito : CuentaBancaria
    {
        public decimal MontoMinimo { get; private set; }

        public CuentaDebito(
            int id, decimal saldo, int numeroCuenta,
            string numeroCliente, string nombreCliente, string fechaApertura,
            decimal montoMinimo
        ) : base(id, saldo, numeroCuenta, numeroCliente, nombreCliente, fechaApertura)
        {
            MontoMinimo = montoMinimo < 0 ? 0 : montoMinimo;
        }

        public override bool Retirar(decimal monto, out string mensaje)
        {
            if (monto <= 0)
            {
                mensaje = "El monto a retirar debe ser mayor que 0.";
                _movimientos.Add(new Movimiento("Error", monto, mensaje));
                return false;
            }

            if (Saldo - monto < MontoMinimo)
            {
                mensaje = string.Format("Saldo insuficiente. Saldo actual: ${0:F2}", Saldo);
                _movimientos.Add(new Movimiento("Error", monto, mensaje));
                return false;
            }

            Saldo -= monto;
            mensaje = string.Format("Retiro exitoso. Nuevo saldo: ${0:F2}", Saldo);
            _movimientos.Add(new Movimiento("Retiro", monto, mensaje));
            return true;
        }

        public override string Resumen()
        {
            return base.Resumen() +
                   string.Format("\nTipo: Débito\nMínimo requerido: ${0:F2}\n-------------------------------", MontoMinimo);
        }
    }
}
