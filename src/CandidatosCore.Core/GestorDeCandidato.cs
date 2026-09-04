namespace CandidatosCore.Core;

using CandidatosCore.Core.Dominio;
using CandidatosCore.Core.Historial;
using CandidatosCore.Core.Notificaciones;

public sealed class GestorDeCandidato
{
    private readonly HistorialCambios historial;
    private readonly NotificadorCambios notificador;

    public GestorDeCandidato(HistorialCambios historial, NotificadorCambios notificador)
    {
        this.historial = historial;
        this.notificador = notificador;
    }

    public void Avanzar(Candidato candidato, string actor) =>
        Ejecutar(candidato, actor, TipoCambio.Avance, candidato.Avanzar);

    public void Rechazar(Candidato candidato, string actor) =>
        Ejecutar(candidato, actor, TipoCambio.Rechazo, candidato.Rechazar);

    public void DeshacerUltimo(Candidato candidato, string actor)
    {
        var registro = historial.DeshacerUltimo(candidato, actor);
        notificador.Notificar(registro);
    }

    private void Ejecutar(Candidato candidato, string actor, TipoCambio tipo, Action transicion)
    {
        var anterior = candidato.ObtenerFaseActual();
        transicion();
        var nuevo = candidato.ObtenerFaseActual();

        var registro = historial.RegistrarCambio(candidato, anterior, nuevo, actor, tipo);
        notificador.Notificar(registro);
    }
}
