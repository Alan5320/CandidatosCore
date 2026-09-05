namespace CandidatosCore.Core.Fases;

public sealed class Rechazado : FaseBase
{
    public override string Nombre => "Rechazado";

    public override FaseBase Avanzar() =>
        throw new InvalidOperationException($"Transición inválida: {Nombre} no tiene una siguiente etapa");

    public override void Rechazar() =>
        throw new InvalidOperationException($"Transición inválida: {Nombre} ya está rechazado");
}
