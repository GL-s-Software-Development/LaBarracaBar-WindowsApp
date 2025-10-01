# 🍽 La Barraca Bar - Sistema de Gestión de Mesas y Ventas

Sistema de escritorio desarrollado en **WPF (.NET Core)** con patrón **MVVM**, diseñado para gestionar mesas, productos, facturación e informes de ventas de un bar.

## ✨ Funcionalidades principales

- ✅ Crear y editar mesas con selección de productos.
- 🧾 Facturar mesa con impresión de ticket en impresora térmica (Gadnic IT1050).
- 💾 Registro automático en base de datos de productos vendidos (`sale_items`).
- 📊 Análisis detallado de ventas por fecha, total, cantidad y categorías.
- 🖨 Soporte para impresión de tickets con o sin logo.

## 🖼️ Capturas del sistema


- Ventana de Logeo
<img width="606" height="537" alt="image" src="https://github.com/user-attachments/assets/73274347-fe66-4586-98de-bdf865eb7f15" />

- Menú principal
<img width="1305" height="702" alt="image" src="https://github.com/user-attachments/assets/dcee479d-53b9-4e9c-9a03-dfce5c12270c" />

- Gestión de mesas
![Gif Gestionar Mesas](https://github.com/user-attachments/assets/ef14b8da-b860-4dfe-8be3-45ae0e5197fa)
![Gif Gestionar Mesas #2](https://github.com/user-attachments/assets/4f59d89b-d738-41da-afb2-67543efe7da0)

- Gestión de productos
![Gestion de productos](https://github.com/user-attachments/assets/a7a3aa06-c626-4af8-9219-bf36ffc4a8c8)

- Ventana de facturación y editar mesa
![Gif Facturacion y editar mesa](https://github.com/user-attachments/assets/a696c41a-7738-46cc-b504-612fb26bf41d)

- Análisis de ventas
<img width="1301" height="702" alt="image" src="https://github.com/user-attachments/assets/043a2fb8-1c50-4ae3-b0b5-e162845d2e46" />

- Actualizaciones
![Actualizaciones](https://github.com/user-attachments/assets/174d157a-174b-4f6a-8c30-555ee42b814f)


- Notificaciones modernas
  
![Notificaciones](https://github.com/user-attachments/assets/3ef26d52-fa9d-4752-966a-cab6a75e04f2)


## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C# (.NET Core 7+)
- **Interfaz:** WPF + XAML
- **Patrón:** MVVM
- **Base de datos:** MySQL
- **Impresión:** RawPrinterHelper
- **Componentes UI personalizados:** Notificaciones, diálogos y vistas modernas

## 📁 Estructura del sistema

```
LaBarracaBar/
├── Models/            # Modelos de datos
├── ViewModels/        # Lógica de presentación
├── Views/             # Vistas XAML y diálogos
├── Repositories/      # Acceso a datos (MySQL)
├── Services/          # Servicios de notificación y impresión (Ticketera IT1050)
├── Converters/        # Conversor para escalar cantidad a ancho en píxeles del gráfico en ventas
├── Resources/         # Imágenes y assets
└── App.xaml           # Aplicación principal
```

## 🔍 Funcionalidad destacada: Facturación

Al presionar **"Cobrar mesa"**:
1. Se abre una ventana moderna con el resumen de productos y total.
2. Al hacer clic en **Facturar**, se imprime el ticket.
3. Se guarda la venta en la base de datos (`sales`, `sale_items`).
4. La mesa se elimina de la vista y tabla temporal.

## 📊 Análisis de ventas

Desde el menú principal:
- Visualización total de ventas realizadas.
- Ventas agrupadas por día, con montos totales y cantidades.
- Ideal para informes semanales o mensuales.

## 💬 Notificaciones y mensajes modernos

Todos los mensajes del sistema se realizan mediante **Notificaciones visuales animados** (no MessageBox), para una experiencia de usuario más limpia.

---

## 🚀 Cómo ejecutar

1. Configurar una base de datos MySQL con el esquema proporcionado.
2. Modificar la cadena de conexión en `BaseRepository.cs`.
3. Ejecutar el proyecto en Visual Studio.
4. Verificar la conexión a la impresora térmica IT1050 (si se desea facturación real).

---

## 📌 Requisitos

- .NET Core 8.0 o superior
- MySQL Server
- Visual Studio 2022+
- Impresora térmica compatible (opcional)

---

## 🙌 Autor

**Luis Nicolás Gomez**  
En desarrollo como parte de un sistema completo para un bar argentino.

---


















