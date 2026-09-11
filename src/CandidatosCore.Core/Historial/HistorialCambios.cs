namespace CandidatosCore.Core.Historial;

using CandidatosCore.Core.Dominio;
using CandidatosCore.Core.Fases;

public sealed class HistorialCambios
{
    private readonly List<RegistroCambio> registros = new();

    public RegistroCambio RegistrarCambio(Candidato candidato, FaseBase anterior, FaseBase nuevo, string actor, TipoCambio tipo, string comentario = "", bool esNotaInterna = false)
    {
        var registro = new RegistroCambio(candidato, anterior, nuevo, actor, tipo, comentario, esNotaInterna);
        registros.Add(registro);
        return registro;
    }

    public RegistroCambio DeshacerUltimo(Candidato candidato, string actor, string comentario = "")
    {
        var ultimo = registros.LastOrDefault(r => r.Candidato == candidato);

        if (ultimo is null)
            throw new InvalidOperationException($"No hay cambios para deshacer sobre {candidato.Nombre}");

        if (ultimo.Tipo == TipoCambio.Deshacer)
            throw new InvalidOperationException($"El último cambio de {candidato.Nombre} ya fue deshecho");

        candidato.CambiarFase(ultimo.EstadoAnterior);

        return RegistrarCambio(candidato, ultimo.EstadoNuevo, ultimo.EstadoAnterior, actor, TipoCambio.Deshacer, comentario, false);
    }

    public IReadOnlyList<RegistroCambio> ObtenerRegistros(Candidato candidato) =>
        registros.Where(r => r.Candidato == candidato).ToList();
}
