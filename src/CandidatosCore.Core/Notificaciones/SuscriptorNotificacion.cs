namespace CandidatosCore.Core.Notificaciones;

using CandidatosCore.Core.Historial;

public sealed class SuscriptorNotificacion : ObservadorCambio
{
    public const string Todos = "*";

    public string Destinatario { get; }
    public IReadOnlySet<string> EventosDeInteres { get; }

    public SuscriptorNotificacion(string destinatario, IEnumerable<string> eventosDeInteres)
    {
        Destinatario = destinatario;
        EventosDeInteres = new HashSet<string>(eventosDeInteres);
    }

    public void Actualizar(RegistroCambio cambio)
    {
        if (!EventosDeInteres.Contains(Todos) && !EventosDeInteres.Contains(cambio.EstadoNuevo.Nombre))
            return;

        EmailService.Enviar(Destinatario, DescribirCambio(cambio));
    }

    private static string DescribirCambio(RegistroCambio cambio) => cambio.Tipo switch
    {
        TipoCambio.Deshacer => $"{cambio.Candidato.Nombre}: se deshizo el último cambio, ahora en {cambio.EstadoNuevo.Nombre}",
        TipoCambio.Rechazo => $"{cambio.Candidato.Nombre} fue rechazado",
        _ => $"{cambio.Candidato.Nombre} pasó a {cambio.EstadoNuevo.Nombre}"
    };
}
