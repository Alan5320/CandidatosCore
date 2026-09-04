namespace CandidatosCore.Core.Fases;

using CandidatosCore.Core.Dominio;

public abstract class FaseBase : Fase
{
    protected Candidato Context { get; private set; } = null!;

    public abstract string Nombre { get; }

    public void SetContext(Candidato candidato) => Context = candidato;

    public abstract void Avanzar();

    public virtual void Rechazar() => Context.CambiarFase(new Rechazado());
}
