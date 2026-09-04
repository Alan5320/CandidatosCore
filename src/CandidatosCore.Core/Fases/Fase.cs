namespace CandidatosCore.Core.Fases;

using CandidatosCore.Core.Dominio;

public interface Fase
{
    string Nombre { get; }
    void SetContext(Candidato candidato);
    void Avanzar();
    void Rechazar();
}
