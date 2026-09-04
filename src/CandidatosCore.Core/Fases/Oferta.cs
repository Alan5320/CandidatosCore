namespace CandidatosCore.Core.Fases;

public sealed class Oferta : FaseBase
{
    public override string Nombre => "Oferta";

    public override void Avanzar() => Context.CambiarFase(new VerificacionReferencias());
}
