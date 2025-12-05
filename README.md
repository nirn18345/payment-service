# 📦 PaymentService — API para Gestión de Pagos de Servicios Básicos

## 📄 Descripción General

**PaymentService** es una API RESTful desarrollada con **.NET 8** que permite registrar y consultar pagos de servicios básicos como electricidad, agua y telecomunicaciones.  
El sistema incluye validaciones de negocio, manejo global de errores, arquitectura modular y soporte para publicar eventos (simulados) mediante un *event producer* compatible con Kafka.

La solución sigue principios **SOLID**, arquitectura limpia, y está preparada para escalar y extenderse fácilmente.

---

## 🧩 Arquitectura General

```
┌─────────────────────────────────────────────┐
│                PaymentsService               │
├─────────────────────────────────────────────┤
│  API Layer (Controllers)                     │
│      - Validación de entrada                 │
│      - Respuestas estandarizadas             │
├─────────────────────────────────────────────┤
│  Application Layer                           │
│      - PaymentService (Reglas de negocio)    │
│      - RequestException (errores controlados)│
│      - Interfaces (IPaymentService, etc.)    │
├─────────────────────────────────────────────┤
│  Infrastructure Layer                        │
│      - Entity Framework (SQL Server)         │
│      - Event Producer (Kafka-ready)          │
│      - DbContext                             │
├─────────────────────────────────────────────┤
│  Domain Layer                                │
│      - Entidades: Customer, Payment          │
└─────────────────────────────────────────────┘
```

---

## 🚀 Tecnologías Utilizadas

- **.NET Core 8** – Framework principal de desarrollo
- **Entity Framework Core** – ORM para acceso a la base de datos
- **SQL Server** – Motor de base de datos relacional
- **Docker** (opcional)
- **Kafka-ready** mediante `IEventProducer`
- **Serilog** para logging estructurado

---

## 🛠 Requisitos Previos

- .NET SDK 8  
- SQL Server (local o remoto)  
- Docker (opcional para Kafka)  

---

## ⚙️ Configuración del Proyecto

### 1️⃣ Crear la Base de Datos

Ejecutar el script:

📄 `PaymentsService.Api/Database/ScripDB.sql`

Que contiene:

- Creación de tablas `Customer` y `Payment`
- Inserción de datos de prueba

---

### 2️⃣ Restaurar Dependencias

```bash
dotnet restore
```

---

### 3️⃣ Configurar la Cadena de Conexión

En `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=PaymentsDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 4️⃣ (Opcional) Aplicar Migraciones

```bash
dotnet ef database update
```

---

### 5️⃣ Ejecutar la API

```bash
dotnet run
```

La API quedará disponible en:

🔗 **https://localhost:7030**

---

## 📡 Endpoints Principales

### **1. Registrar un Pago**
`POST /api/payments`

#### Request:
```json
{
  "customerId": "cfe8b150-2f84-4a1a-bdf4-923b20e34973",
  "serviceProvider": "Servicios Eléctricos S.A.",
  "amount": 120.50,
  "currency": "BS"
}
```

#### Reglas de Negocio:
- ❌ Monto debe ser mayor que 0  
- ❌ No se permiten montos mayores a **1500 Bs**  
- ❌ Se rechazan montos en dólares  
- ❌ Customer debe existir  
- Estado inicial: **pending**

---

### **2. Consultar Pagos por Cliente**
`GET /api/payments?customerId=...&pageNumber=1&pageSize=10`

#### Respuesta:
```json
{
  "totalRecords": 1,
  "pageNumber": 1,
  "pageSize": 10,
  "items": [
    {
      "paymentId": "04dbef2a-e4da-4473-849a-7cf9b24ce531",
      "serviceProvider": "Servicios Eléctricos S.A.",
      "amount": 120.5,
      "currency": "BS",
      "status": "pending",
      "createdAt": "2025-07-17T08:30:00Z"
    }
  ]
}
```

---

## 🔐 Autenticación

> ⚠️ **Esta API no utiliza JWT ni cookies**, ya que la prueba técnica no lo requiere.  
Sin embargo, la arquitectura está preparada para habilitar autenticación en el futuro sin romper el diseño.

---

## ⚠️ Manejo Global de Errores (Middleware)

La API devuelve errores estandarizados:

```json
{
  "code": 400,
  "message": "Amount exceeds the allowed limit.",
  "traceid": "c0f03bb6-ba7e-4f36-a886-8c19d390c54b",
  "errors": null
}
```

El middleware:
- Captura errores no controlados  
- Registra trazabilidad con `traceId`  
- Evita exponer excepciones internas  
- Unifica las respuestas para el cliente  

---

## 🔄 Integración con Kafka (Simulada)

El proyecto incluye:

### `IEventProducer`
```csharp
Task PublishAsync(string topic, object message);
```

### Implementación Fake:
- Publica eventos simulados como `payment.created`
- Fácil de reemplazar por Kafka real mediante Confluent.Kafka

---


## 📄 Licencia

Este proyecto está bajo la **Licencia MIT**.

---

## 👤 Contacto

📧 **nirn18345@gmail.com**
