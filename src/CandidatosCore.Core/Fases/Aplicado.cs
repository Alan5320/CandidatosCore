namespace CandidatosCore.Core.Fases;

public sealed class Aplicado : FaseBase
{
    public override string Nombre => "Aplicado";

    public override void Avanzar() => Context.CambiarFase(new Entrevista());
}
