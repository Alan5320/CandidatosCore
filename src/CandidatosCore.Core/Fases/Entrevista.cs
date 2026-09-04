namespace CandidatosCore.Core.Fases;

public sealed class Entrevista : FaseBase
{
    public override string Nombre => "Entrevista";

    public override void Avanzar() => Context.CambiarFase(new PruebaTecnica());
}
