using AppBanco.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppBanco.Services
{
    /// <summary>
    /// Persiste la cuenta activa en un archivo JSON.
    /// Usa TypeNameHandling para serializar herencia (Débito/Crédito).
    /// </summary>
    public static class BancoStore
    {
        private const string FilePath = "datos_banco.json";

        public static void Guardar(CuentaBancaria cuenta)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(cuenta, settings);
            File.WriteAllText(FilePath, json);
        }

        public static CuentaBancaria Cargar()
        {
            if (!File.Exists(FilePath))
                return null;

            var json = File.ReadAllText(FilePath);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            return JsonConvert.DeserializeObject<CuentaBancaria>(json, settings);
        }
    }
}
