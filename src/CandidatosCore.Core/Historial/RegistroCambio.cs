namespace CandidatosCore.Core.Historial;

using CandidatosCore.Core.Dominio;
using CandidatosCore.Core.Fases;

public sealed class RegistroCambio
{
    public Candidato Candidato { get; }
    public FaseBase EstadoAnterior { get; }
    public FaseBase EstadoNuevo { get; }
    public string Actor { get; }
    public DateTime Fecha { get; }
    public TipoCambio Tipo { get; }

    public string Comentario { get; }
    public bool EsNotaInterna { get; }

    public RegistroCambio(Candidato candidato, FaseBase estadoAnterior, FaseBase estadoNuevo, string actor, TipoCambio tipo, string comentario = "", bool esNotaInterna = false)
    {
        Candidato = candidato;
        EstadoAnterior = estadoAnterior;
        EstadoNuevo = estadoNuevo;
        Actor = actor;
        Tipo = tipo;
        Fecha = DateTime.UtcNow;
        Comentario = comentario;
        EsNotaInterna = esNotaInterna;
    }
}
