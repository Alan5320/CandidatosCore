namespace CandidatosCore.Core.Dominio;

using CandidatosCore.Core.Fases;

public sealed class Candidato
{
    public string Nombre { get; }
    public string Correo { get; }

    private Fase faseActual;

    public Candidato(string nombre, string correo)
    {
        Nombre = nombre;
        Correo = correo;
        faseActual = new Aplicado();
        faseActual.SetContext(this);
    }

    public void Avanzar() => faseActual.Avanzar();

    public void Rechazar() => faseActual.Rechazar();

    public void CambiarFase(Fase nuevaFase)
    {
        nuevaFase.SetContext(this);
        faseActual = nuevaFase;
    }

    public Fase ObtenerFaseActual() => faseActual;
}
