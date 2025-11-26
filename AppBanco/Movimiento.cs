using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBanco.Models
{

          /// <summary>
        /// Representa un movimiento de cuenta (depósito, retiro o error).
        /// </summary>
        public class Movimiento
        {
            // PROPIEDADES PÚBLICAS (necesarias para que otros archivos puedan leerlas)
            public DateTime Fecha { get; private set; }
            public string Tipo { get; private set; }     // "Depósito" | "Retiro" | "Error"
            public decimal Monto { get; private set; }
            public string Mensaje { get; private set; }

            // Constructor de 3 argumentos (lo usan CuentaBancaria/Credito/Debito)
            public Movimiento(string tipo, decimal monto, string mensaje)
            {
                Fecha = DateTime.Now;
                Tipo = tipo ?? "Desconocido";
                Monto = monto;
                Mensaje = mensaje ?? "";
            }

            public override string ToString()
            {
                return string.Format("{0:yyyy-MM-dd HH:mm} - {1} ${2:F2} - {3}",
                                     Fecha, Tipo, Monto, Mensaje);
            }
        }
}



