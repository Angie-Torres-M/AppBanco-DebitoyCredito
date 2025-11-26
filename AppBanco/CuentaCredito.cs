using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace AppBanco.Models
    {
        /// <summary>
        /// Cuenta de Crédito con límite de sobregiro (saldo puede ser negativo).
        /// Regla: se permite retirar si (Saldo - monto) >= -Límite.
        /// </summary>
        public class CuentaCredito : CuentaBancaria
        {
            public string FechaCorte { get; private set; }
            public decimal Tasa { get; private set; }
            public decimal Limite { get; private set; }

            public CuentaCredito(
                int id, decimal saldo, int numeroCuenta,
                string numeroCliente, string nombreCliente, string fechaApertura,
                string fechaCorte, decimal tasa, decimal limite
            ) : base(id, saldo, numeroCuenta, numeroCliente, nombreCliente, fechaApertura)
            {
                FechaCorte = fechaCorte ?? "";
                Tasa = tasa < 0 ? 0 : tasa;
                Limite = limite < 0 ? 0 : limite;
            }

            public override bool Retirar(decimal monto, out string mensaje)
            {
                // Validación de monto
                if (monto <= 0)
                {
                    mensaje = "El monto a retirar debe ser mayor que 0.";
                    _movimientos.Add(new Movimiento("Error", monto, mensaje)); // <-- 3 argumentos
                    return false;
                }

                // Regla de límite de crédito
                if (Saldo - monto < -Limite)
                {
                    decimal disponible = Saldo + Limite;
                    mensaje = string.Format("Límite de crédito excedido. Disponible: ${0:F2}", disponible);
                    _movimientos.Add(new Movimiento("Error", monto, mensaje)); // <-- 3 argumentos
                    return false;
                }

                // Aplicar retiro (puede dejar saldo negativo hasta -Limite)
                Saldo -= monto;
                mensaje = string.Format("Retiro exitoso. Nuevo saldo: ${0:F2}", Saldo);
                _movimientos.Add(new Movimiento("Retiro", monto, mensaje)); // <-- 3 argumentos
                return true;
            }

            public override string Resumen()
            {
                return base.Resumen()
                       + "\nTipo: Crédito"
                       + "\nCorte: " + FechaCorte
                       + string.Format("\nTasa: {0:P2}", Tasa)
                       + string.Format("\nLímite: ${0:F2}\n-------------------------------", Limite);
            }
        }
    }
