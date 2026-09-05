namespace CandidatosCore.Core.Fases;

using CandidatosCore.Core.Dominio;

public abstract class FaseBase
{
    protected Candidato Context { get; private set; } = null!;

    public static readonly Dictionary<string, FaseBase> SiguientePaso = new()
    {
        { "Aplicado", new Entrevista() },
        { "Entrevista", new PruebaTecnica() },
        { "Prueba técnica", new Oferta() },
        { "Oferta", new VerificacionReferencias() },
        { "Verificación de referencias", new Contratado() },
        { "Rechazado", new Rechazado() }
    };

    public abstract string Nombre { get; }

    public void SetContext(Candidato candidato) => Context = candidato;

    public virtual FaseBase Avanzar()
    {
        if (SiguientePaso.TryGetValue(Nombre, out var faseSiguiente))
        {
            var nuevaFase = (FaseBase)Activator.CreateInstance(faseSiguiente.GetType())!;
            Context?.CambiarFase(nuevaFase);
            return nuevaFase;
        }

        throw new InvalidOperationException($"Transición inválida: {Nombre} no tiene una siguiente etapa");
    }

    public virtual void Rechazar()
    {
        if (SiguientePaso.TryGetValue("Rechazado", out var faseRechazado))
        {
            var nuevaFase = (FaseBase)Activator.CreateInstance(faseRechazado.GetType())!;
            Context?.CambiarFase(nuevaFase);
            return;
        }

        throw new InvalidOperationException($"Transición inválida: {Nombre} -> Rechazado");
    }
}
