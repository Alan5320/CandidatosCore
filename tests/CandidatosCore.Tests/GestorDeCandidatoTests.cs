namespace CandidatosCore.Tests;

using CandidatosCore.Core;
using CandidatosCore.Core.Dominio;
using CandidatosCore.Core.Historial;
using CandidatosCore.Core.Notificaciones;
using Xunit;

public sealed class GestorDeCandidatoTests
{
    private static (GestorDeCandidato gestor, HistorialCambios historial, NotificadorCambios notificador) CrearGestor()
    {
        var historial = new HistorialCambios();
        var notificador = new NotificadorCambios();
        var gestor = new GestorDeCandidato(historial, notificador);
        return (gestor, historial, notificador);
    }

    [Fact]
    public void Avanzar_RecorreTodasLasEtapasEnOrden()
    {
        var (gestor, _, _) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");

        gestor.Avanzar(candidato, "reclutador");
        Assert.Equal("Entrevista", candidato.ObtenerFaseActual().Nombre);

        gestor.Avanzar(candidato, "reclutador");
        Assert.Equal("Prueba técnica", candidato.ObtenerFaseActual().Nombre);

        gestor.Avanzar(candidato, "reclutador");
        Assert.Equal("Oferta", candidato.ObtenerFaseActual().Nombre);

        gestor.Avanzar(candidato, "reclutador");
        Assert.Equal("Verificación de referencias", candidato.ObtenerFaseActual().Nombre);

        gestor.Avanzar(candidato, "reclutador");
        Assert.Equal("Contratado", candidato.ObtenerFaseActual().Nombre);
    }

    [Fact]
    public void Avanzar_DesdeContratado_LanzaExcepcion()
    {
        var (gestor, _, _) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");

        for (var i = 0; i < 5; i++)
            gestor.Avanzar(candidato, "reclutador");

        Assert.Throws<InvalidOperationException>(() => gestor.Avanzar(candidato, "reclutador"));
    }

    [Fact]
    public void Rechazar_DesdeCualquierEtapa_MuevaACandidatoARechazado()
    {
        var (gestor, _, _) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");

        gestor.Avanzar(candidato, "reclutador");
        gestor.Rechazar(candidato, "reclutador");

        Assert.Equal("Rechazado", candidato.ObtenerFaseActual().Nombre);
    }

    [Fact]
    public void DeshacerUltimo_RestauraLaEtapaAnterior()
    {
        var (gestor, _, _) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");

        gestor.Avanzar(candidato, "reclutador");
        gestor.Rechazar(candidato, "reclutador");
        gestor.DeshacerUltimo(candidato, "supervisor");

        Assert.Equal("Entrevista", candidato.ObtenerFaseActual().Nombre);
    }

    [Fact]
    public void DeshacerUltimo_SinCambiosPrevios_LanzaExcepcion()
    {
        var (gestor, _, _) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");

        Assert.Throws<InvalidOperationException>(() => gestor.DeshacerUltimo(candidato, "supervisor"));
    }

    [Fact]
    public void Notificador_SoloAvisaASuscriptoresInteresadosEnLaEtapa()
    {
        var (gestor, _, notificador) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");
        var eventosRecibidos = new List<string>();

        notificador.Suscribir(new SuscriptorNotificacion("nomina@hirecore.com", new[] { "Contratado" }));
        notificador.Suscribir(new EspiaObservador(eventosRecibidos));

        gestor.Avanzar(candidato, "reclutador");

        Assert.Contains("Entrevista", eventosRecibidos);
    }

    [Fact]
    public void HistorialCambios_RegistraActorYFechaDeCadaCambio()
    {
        var (gestor, historial, _) = CrearGestor();
        var candidato = new Candidato("Ana", "ana@correo.com");

        gestor.Avanzar(candidato, "reclutador.laura");

        var registros = historial.ObtenerRegistros(candidato);
        Assert.Single(registros);
        Assert.Equal("reclutador.laura", registros[0].Actor);
        Assert.Equal(TipoCambio.Avance, registros[0].Tipo);
    }

    private sealed class EspiaObservador : ObservadorCambio
    {
        private readonly List<string> eventosRecibidos;

        public EspiaObservador(List<string> eventosRecibidos) => this.eventosRecibidos = eventosRecibidos;

        public void Actualizar(RegistroCambio cambio) => eventosRecibidos.Add(cambio.EstadoNuevo.Nombre);
    }
}
