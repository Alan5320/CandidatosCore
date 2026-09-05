namespace CandidatosCore.Core.Dominio;

using CandidatosCore.Core.Fases;

public sealed class Candidato
{
    public string Nombre { get; }
    public string Correo { get; }

    private FaseBase faseActual;

    public Candidato(string nombre, string correo)
    {
        Nombre = nombre;
        Correo = correo;
        faseActual = new Aplicado();
        faseActual.SetContext(this);
    }

    public void Avanzar() => faseActual.Avanzar();

    public void Rechazar() => faseActual.Rechazar();

    public void CambiarFase(FaseBase nuevaFase)
    {
        nuevaFase.SetContext(this);
        faseActual = nuevaFase;
    }

    public FaseBase ObtenerFaseActual() => faseActual;
}
