

# AppBanco - Débito y Crédito (C# + JSON)

Aplicación bancaria desarrollada en C# que simula el manejo de cuentas de débito y crédito. Permite realizar depósitos, retiros, pagos, transferencias internas y consultar saldos. Incluye persistencia de datos mediante archivos JSON para mantener el historial entre ejecuciones.

---

## Funcionalidades

- Crear cuentas de débito y crédito  
- Consultar saldo actual  
- Depósitos y retiros (con validaciones)  
- Pagos a tarjeta de crédito  
- Límite de crédito configurable  
- Cargos automáticos y cálculo de deuda  
- Transferencias entre cuentas  
- Persistencia en archivos JSON  
- Carga de datos al iniciar y guardado automático al cerrar  

---

## Arquitectura del Proyecto

```

AppBanco-DebitoyCredito/
├── Program.cs
├── Models/
│    ├── CuentaDebito.cs
│    ├── CuentaCredito.cs
│    ├── Movimiento.cs
├── Services/
│    ├── BancoService.cs
│    └── JsonService.cs
├── Data/
│    └── cuentas.json
├── README.md
├── LICENSE
└── .gitignore

````

---

## Persistencia con JSON

El archivo `cuentas.json` almacena:

- Número de cuenta  
- Tipo de cuenta  
- Saldo actual  
- Límite de crédito (si aplica)  
- Movimientos registrados  

La aplicación carga este archivo al iniciar y escribe los nuevos cambios al finalizar las operaciones.

---

## Instalación y Ejecución

1. Clona el repositorio:

```bash
git clone https://github.com/Angie-Torres-M/AppBanco-Basico.git
````

> *Verifica que estés en la carpeta AppBanco-DebitoyCredito.*

2. Ejecuta con .NET:

```bash
dotnet run
```

---

## Conceptos Implementados

* Programación Orientada a Objetos (POO)
* Herencia (cuenta base → débito / crédito)
* Validaciones de flujo de dinero
* Manejo de archivos JSON
* Serialización y deserialización
* Lógica bancaria realista
* Separación por capas (Models, Services, Data)

---

## Mejoras Futuras

* Historial completo de movimientos por fecha
* Reportes PDF de estado de cuenta
* Interfaz gráfica (WPF)
* API REST en .NET para uso web
* Múltiples clientes y autenticación
* Sincronización con base de datos SQL

---


## Autora

**Angie Torres**
Desarrolladora Full Stack Jr.
Enfocada en lógica, POO, manejo de datos y prácticas de ingeniería de software.

```

