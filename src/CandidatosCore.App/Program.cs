using CandidatosCore.Core;
using CandidatosCore.Core.Dominio;
using CandidatosCore.Core.Historial;
using CandidatosCore.Core.Notificaciones;

var historial = new HistorialCambios();
var notificador = new NotificadorCambios();

notificador.Suscribir(new SuscriptorNotificacion("reclutador@hirecore.com", new[] { SuscriptorNotificacion.Todos }, true, true));
notificador.Suscribir(new SuscriptorNotificacion("gerente.contratacion@hirecore.com", new[] { "Oferta", "Contratado" }));
notificador.Suscribir(new SuscriptorNotificacion("nomina@hirecore.com", new[] { "Contratado" }));
notificador.Suscribir(new SuscriptorNotificacion("portal@hirecore.com", new[] { SuscriptorNotificacion.Todos }, false));

var gestor = new GestorDeCandidato(historial, notificador);
var candidato = new Candidato("Ana Torres", "ana.torres@correo.com");

Console.WriteLine($"--- Proceso de {candidato.Nombre} ---");

gestor.Avanzar(candidato, "reclutador.laura", "Validar experiencia laboral", true);
gestor.Avanzar(candidato, "reclutador.laura", "Avanza por que me cae bien");
gestor.Avanzar(candidato, "reclutador.laura", "Avanza por que me cae bien");

Console.WriteLine($"Fase actual: {candidato.ObtenerFaseActual().Nombre}");

gestor.Rechazar(candidato, "reclutador.laura", "Rechaza por que no me cae bien");
Console.WriteLine($"Fase actual: {candidato.ObtenerFaseActual().Nombre}");

gestor.DeshacerUltimo(candidato, "rrhh.supervisor", "Deshace porque no me cae bien");
Console.WriteLine($"Fase actual tras deshacer: {candidato.ObtenerFaseActual().Nombre}");

foreach (var registro in historial.ObtenerRegistros(candidato))
{
    Console.WriteLine($"{registro.Fecha:u} | {registro.Actor} | {registro.Tipo} | {registro.EstadoAnterior.Nombre} -> {registro.EstadoNuevo.Nombre}");
}
