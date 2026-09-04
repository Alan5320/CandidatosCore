namespace CandidatosCore.Core.Notificaciones;

using CandidatosCore.Core.Historial;

public sealed class NotificadorCambios
{
    private readonly List<ObservadorCambio> observadores = new();

    public void Suscribir(ObservadorCambio observador) => observadores.Add(observador);

    public void Notificar(RegistroCambio cambio)
    {
        foreach (var observador in observadores)
            observador.Actualizar(cambio);
    }
}
