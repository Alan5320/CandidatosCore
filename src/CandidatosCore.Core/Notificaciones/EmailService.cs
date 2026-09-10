namespace CandidatosCore.Core.Notificaciones;

public static class EmailService
{
    public static void Enviar(string destinatario, string mensaje, string? comentario = null) =>
        Console.WriteLine($"[correo -> {destinatario}] {mensaje} {comentario}");
}
