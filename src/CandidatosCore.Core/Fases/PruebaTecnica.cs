namespace CandidatosCore.Core.Fases;

public sealed class PruebaTecnica : FaseBase
{
    public override string Nombre => "Prueba técnica";

    public override void Avanzar() => Context.CambiarFase(new Oferta());
}
