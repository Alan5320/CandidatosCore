namespace CandidatosCore.Core.Notificaciones;

using CandidatosCore.Core.Historial;

public sealed class SuscriptorNotificacion : ObservadorCambio
{
    public const string Todos = "*";

    public string Destinatario { get; }
    public IReadOnlySet<string> EventosDeInteres { get; }

    public bool conComentario { get; }

    public SuscriptorNotificacion(string destinatario, IEnumerable<string> eventosDeInteres, bool? conComentario)
    {
        Destinatario = destinatario;
        EventosDeInteres = new HashSet<string>(eventosDeInteres);
        this.conComentario = conComentario ?? false;
    }

    public void Actualizar(RegistroCambio cambio)
    {
        if (!EventosDeInteres.Contains(Todos) && !EventosDeInteres.Contains(cambio.EstadoNuevo.Nombre))
            return;

        string descripcion = conComentario ? $"{cambio.Comentario}" : null;

        EmailService.Enviar(Destinatario, DescribirCambio(cambio), comentario);
    }

    private static string DescribirCambio(RegistroCambio cambio) => cambio.Tipo switch
    {
        TipoCambio.Deshacer => $"{cambio.Candidato.Nombre}: se deshizo el último cambio, ahora en {cambio.EstadoNuevo.Nombre}",
        TipoCambio.Rechazo => $"{cambio.Candidato.Nombre} fue rechazado",
        TipoCambio.Avance => $"{cambio.Candidato.Nombre} pasó a {cambio.EstadoNuevo.Nombre}",
        _ => $"Error al describir el cambio: {cambio.Tipo}"
    };
}
