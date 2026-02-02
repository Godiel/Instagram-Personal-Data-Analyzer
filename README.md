# 📱 Instagram Relationship Analyzer

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Blazor](https://img.shields.io/badge/Blazor-InteractiveServer-512BD4?style=flat&logo=blazor)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)

[ES] Analizador de relaciones de Instagram privado y local. Descubre quién no te sigue de vuelta, tus fans, mejores amigos y más, procesando directamente tu exportación de datos de Meta sin compartir información con terceros.

[EN] Private and local Instagram relationship analyzer. Discover non-followers, fans, close friends, and more by processing your Meta data export directly, without sharing information with third parties.

---

## 📖 Guía: Cómo obtener tu archivo .zip / How to get your .zip file

### [ES] Pasos para descargar tus datos:
1. Ve a **Centro de cuentas de Meta** > **Tu información y tus permisos**.
2. Selecciona **Descargar tu información**.
3. Haz clic en **Solicitar descarga** y selecciona tu cuenta de Instagram.
4. Elige **Tipos de información específicos**.
5. **IMPORTANTE:** Selecciona únicamente **"Seguidores y seguidos"**. Esto genera un archivo mucho más ligero y rápido de procesar.
6. Selecciona el formato **JSON** (Obligatorio, el formato HTML no es compatible).
7. Una vez que Meta genere el archivo, descarga el `.zip` y cárgalo en la aplicación.

> 🔗 [Documentación oficial de ayuda de Instagram](https://help.instagram.com/181231772500920)

### [EN] Steps to download your data:
1. Go to **Meta Accounts Center** > **Your information and permissions**.
2. Select **Download your information**.
3. Click **Request a download** and select your Instagram account.
4. Choose **Types of information**.
5. **IMPORTANT:** Select only **"Followers and following"**. This generates a much lighter and faster file to process.
6. Select **JSON** format (Mandatory, HTML format is not supported).
7. Once Meta generates the file, download the `.zip` and upload it to the app.

---

## 🛠️ Stack Tecnológico / Tech Stack

- **Framework:** .NET 9.0 (ASP.NET Core)
- **Frontend:** Blazor Interactive Server
- **Styling:** Bootstrap 5.3 + Bootstrap Icons
- **Architecture:** Clean Architecture
- **Data Engine:** System.Text.Json (JsonDocument) para parsing dinámico de alto rendimiento.

---

## 🏗️ Arquitectura y Decisiones Técnicas / Architecture & Decisions



### [ES] Puntos Clave:
- **Privacidad Local:** Siguiendo principios de "Privacy by Design", el procesamiento ocurre íntegramente en memoria volátil. Los datos nunca se guardan en disco ni se envían fuera del entorno local.
- **Rendimiento O(n):** La lógica de comparación utiliza `HashSet<string>` con `StringComparer.OrdinalIgnoreCase`. Esto garantiza una complejidad de tiempo constante en las búsquedas, permitiendo analizar miles de registros de forma instantánea.
- **Resiliencia ante Cambios de Meta:** Instagram suele variar las llaves raíz de sus JSON (ej. `relationships_following` vs listas planas). Se desarrolló un motor de extracción dinámica basado en `JsonDocument` que navega por el árbol de nodos identificando automáticamente las colecciones de datos.
- **Optimización de Memoria en .NET 9:** Aprovechamos las mejoras de rendimiento de `System.IO.Compression` para procesar el flujo de datos del ZIP sin necesidad de descompresión física.

### [EN] Key Points:
- **Local Privacy:** Following "Privacy by Design" principles, processing occurs entirely in volatile memory. Data is never saved to disk or sent outside the local environment.
- **O(n) Performance:** Comparison logic uses `HashSet<string>` with `StringComparer.OrdinalIgnoreCase`. This guarantees constant-time complexity for lookups, allowing thousands of records to be analyzed instantly.
- **Resilience to Meta Changes:** Instagram often varies JSON root keys (e.g., `relationships_following` vs. flat lists). A dynamic extraction engine based on `JsonDocument` was developed to navigate the node tree, automatically identifying data collections.
- **Memory Optimization in .NET 9:** We leverage the performance improvements of `System.IO.Compression` to process the ZIP data stream without physical decompression.

---

## 🚀 Instalación / Installation

1. Clonar el repositorio / Clone the repo:
   ```bash
   git clone [https://github.com/Godiel/InstagramAnalyzer.git](https://github.com/Godiel/InstagramAnalyzer.git)

2. Ir a la carpeta del proyecto / Go to project folder:
   ```bash
   cd InstagramAnalyzer

3. Restaurar dependencias y ejecutar / Restore and run:
   ```bash
   dotnet watch run --project src/InstagramAnalyzer.WebUI

## 👨‍💻 Developed by
**Godiel** [![GitHub](https://img.shields.io/badge/GitHub-Profile-181717?style=flat&logo=github)](https://github.com/Godiel)