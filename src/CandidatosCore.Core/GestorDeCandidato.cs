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

    public void Avanzar(Candidato candidato, string actor, string comentario) =>
        Ejecutar(candidato, actor, TipoCambio.Avance, candidato.Avanzar, comentario);

    public void Rechazar(Candidato candidato, string actor, string comentario) =>
        Ejecutar(candidato, actor, TipoCambio.Rechazo, candidato.Rechazar, comentario);

    public void DeshacerUltimo(Candidato candidato, string actor, string comentario)
    {
        var registro = historial.DeshacerUltimo(candidato, actor, comentario);
        notificador.Notificar(registro);
    }

    private void Ejecutar(Candidato candidato, string actor, TipoCambio tipo, Action transicion, string comentario)
    {
        var anterior = candidato.ObtenerFaseActual();
        transicion();
        var nuevo = candidato.ObtenerFaseActual();

        var registro = historial.RegistrarCambio(candidato, anterior, nuevo, actor, tipo, comentario);
        notificador.Notificar(registro);
    }
}
