namespace CandidatosCore.Core.Fases;

public sealed class VerificacionReferencias : FaseBase
{
    public override string Nombre => "Verificación de referencias";

    public override void Avanzar() => Context.CambiarFase(new Contratado());
}
