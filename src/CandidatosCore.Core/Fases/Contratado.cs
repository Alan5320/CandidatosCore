namespace CandidatosCore.Core.Fases;

public sealed class Contratado : FaseBase
{
    public override string Nombre => "Contratado";

    public override void Avanzar() =>
        throw new InvalidOperationException($"Transición inválida: {Nombre} no tiene una siguiente etapa");

    public override void Rechazar() =>
        throw new InvalidOperationException($"Transición inválida: {Nombre} -> Rechazado");
}
