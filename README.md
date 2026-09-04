# CandidatosCore

Rediseño de **HireCore**, el sistema de seguimiento de candidatos (ATS), como parte del reto evaluativo del Bloque 1 — Principios y Patrones de Diseño.

El `GestorDeCandidato` original mezclaba transiciones de estado, reglas de notificación y lógica de deshacer en un único método con condicionales anidados. Esta versión separa esas tres responsabilidades con tres patrones de diseño, de modo que `GestorDeCandidato` no conoce el nombre de ninguna etapa, ningún destinatario de notificación, ni cómo se deshace un cambio.

## Patrones aplicados

| Patrón | Requisito que resuelve | Clases involucradas |
|---|---|---|
| **State** | Agregar etapas (ej. Prueba técnica, Verificación de referencias) sin tocar las existentes; cada etapa declara a cuál puede avanzar. | `Fase`, `FaseBase`, `Aplicado`, `Entrevista`, `PruebaTecnica`, `Oferta`, `VerificacionReferencias`, `Contratado`, `Rechazado` |
| **Observer** | Notificaciones diferenciadas por rol (reclutador, gerente, nómina, portal) sin que el gestor conozca a los destinatarios. | `NotificadorCambios`, `ObservadorCambio`, `SuscriptorNotificacion` |
| **Memento** | Deshacer la última transición y mantener auditoría de quién y cuándo. | `HistorialCambios` (caretaker), `RegistroCambio` (memento) |

`Candidato` es el *Context* del State: delega `Avanzar()`/`Rechazar()` en su `Fase` actual. `GestorDeCandidato` solo coordina: ejecuta la transición sobre el candidato, pide a `HistorialCambios` que la registre, y a `NotificadorCambios` que la informe.

## Estructura

```
src/
  CandidatosCore.Core/
    Dominio/          Candidato (Context)
    Fases/            Fase, FaseBase y las etapas concretas (State)
    Historial/        RegistroCambio, HistorialCambios (Memento)
    Notificaciones/    ObservadorCambio, NotificadorCambios, SuscriptorNotificacion (Observer)
    GestorDeCandidato.cs
  CandidatosCore.App/  Consola de demostración del flujo completo
tests/
  CandidatosCore.Tests/ Pruebas xUnit de transiciones, notificaciones y deshacer
```

## Ejecutar

```bash
dotnet run --project src/CandidatosCore.App
```

## Pruebas

```bash
dotnet test
```

## Diagrama de clases

```mermaid
classDiagram
    class GestorDeCandidato {
        -HistorialCambios historial
        -NotificadorCambios notificador
        +Avanzar(candidato, actor)
        +Rechazar(candidato, actor)
        +DeshacerUltimo(candidato, actor)
    }

    class Candidato {
        <<Context>>
        +Nombre
        +Correo
        -faseActual : Fase
        +Avanzar()
        +Rechazar()
        +CambiarFase(nuevaFase)
        +ObtenerFaseActual()
    }

    class Fase {
        <<interface>>
        +Nombre
        +SetContext(candidato)
        +Avanzar()
        +Rechazar()
    }

    class FaseBase {
        <<abstract>>
        #Context : Candidato
        +SetContext(candidato)
        +Avanzar()*
        +Rechazar()
    }

    class HistorialCambios {
        -registros : List~RegistroCambio~
        +RegistrarCambio(candidato, anterior, nuevo, actor, tipo)
        +DeshacerUltimo(candidato, actor)
    }

    class RegistroCambio {
        +Candidato
        +EstadoAnterior : Fase
        +EstadoNuevo : Fase
        +Actor
        +Fecha
        +Tipo
    }

    class NotificadorCambios {
        -observadores : List~ObservadorCambio~
        +Suscribir(observador)
        +Notificar(cambio)
    }

    class ObservadorCambio {
        <<interface>>
        +Actualizar(cambio)
    }

    class SuscriptorNotificacion {
        +Destinatario
        +EventosDeInteres
        +Actualizar(cambio)
    }

    GestorDeCandidato --> Candidato : administra
    GestorDeCandidato --> HistorialCambios : delega
    GestorDeCandidato --> NotificadorCambios : informa
    Candidato o-- Fase : fase actual
    Fase <|.. FaseBase
    FaseBase <|-- Aplicado
    FaseBase <|-- Entrevista
    FaseBase <|-- PruebaTecnica
    FaseBase <|-- Oferta
    FaseBase <|-- VerificacionReferencias
    FaseBase <|-- Contratado
    FaseBase <|-- Rechazado
    FaseBase ..> Candidato : cambia el context
    HistorialCambios o-- RegistroCambio
    RegistroCambio --> Fase : anterior y nueva
    ObservadorCambio <|.. SuscriptorNotificacion
    NotificadorCambios o-- ObservadorCambio
```
