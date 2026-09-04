namespace CandidatosCore.Core.Historial;

using CandidatosCore.Core.Dominio;
using CandidatosCore.Core.Fases;

public sealed class RegistroCambio
{
    public Candidato Candidato { get; }
    public Fase EstadoAnterior { get; }
    public Fase EstadoNuevo { get; }
    public string Actor { get; }
    public DateTime Fecha { get; }
    public TipoCambio Tipo { get; }

    public RegistroCambio(Candidato candidato, Fase estadoAnterior, Fase estadoNuevo, string actor, TipoCambio tipo)
    {
        Candidato = candidato;
        EstadoAnterior = estadoAnterior;
        EstadoNuevo = estadoNuevo;
        Actor = actor;
        Tipo = tipo;
        Fecha = DateTime.UtcNow;
    }
}
