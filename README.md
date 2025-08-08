# 🍽 La Barraca Bar - Sistema de Gestión de Mesas y Ventas

Sistema de escritorio desarrollado en **WPF (.NET Core)** con patrón **MVVM**, diseñado para gestionar mesas, productos, facturación e informes de ventas de un bar.

## ✨ Funcionalidades principales

- ✅ Crear y editar mesas con selección de productos.
- 🧾 Facturar mesa con impresión de ticket en impresora térmica (Gadnic IT1050).
- 💾 Registro automático en base de datos de productos vendidos (`sale_items`).
- 📊 Análisis detallado de ventas por fecha, total, cantidad y categorías.
- 🖨 Soporte para impresión de tickets con o sin logo.

## 🖼️ Capturas del sistema


> ⚠️ *Agregar aquí capturas de pantalla del sistema*
- Pantalla principal
- Gestión de mesas
- Ventana de facturación
- Análisis de ventas



## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C# (.NET Core 7+)
- **Interfaz:** WPF + XAML
- **Patrón:** MVVM
- **Base de datos:** MySQL
- **Impresión:** RawPrinterHelper
- **Componentes UI personalizados:** Toasts, diálogos, vistas modernas

## 📁 Estructura del sistema

```
LaBarracaBar/
├── Models/            # Modelos de datos (Product, Sale, etc)
├── ViewModels/        # Lógica de presentación
├── Views/             # Vistas XAML y diálogos
├── Repositories/      # Acceso a datos (MySQL)
├── Resources/         # Imágenes y assets
├── App.xaml           # Aplicación principal
└── MainWindow.xaml    # Layout principal con navegación
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

## 💬 Toasts y mensajes modernos

Todos los mensajes del sistema se realizan mediante **toasts visuales animados** (no MessageBox), para una experiencia de usuario más limpia.

---

## 🚀 Cómo ejecutar

1. Configurar una base de datos MySQL con el esquema proporcionado.
2. Modificar la cadena de conexión en `BaseRepository.cs`.
3. Ejecutar el proyecto en Visual Studio.
4. Verificar la conexión a la impresora térmica IT1050 (si se desea facturación real).

---

## 📌 Requisitos

- .NET Core 7.0 o superior
- MySQL Server
- Visual Studio 2022+
- Impresora térmica compatible (opcional)

---

## 🙌 Autor

**Luis Nicolás Gomez**  
En desarrollo como parte de un sistema completo para un bar argentino.

---
