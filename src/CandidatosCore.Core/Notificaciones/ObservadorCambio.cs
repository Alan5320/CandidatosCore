namespace CandidatosCore.Core.Notificaciones;

using CandidatosCore.Core.Historial;

public interface ObservadorCambio
{
    void Actualizar(RegistroCambio cambio);
}
