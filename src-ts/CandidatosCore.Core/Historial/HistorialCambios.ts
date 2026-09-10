import type { Candidato } from "../Dominio/Candidato";
import type { FaseBase } from "../Fases";
import { RegistroCambio } from "./RegistroCambio";
import { TipoCambio } from "./TipoCambio";

export class HistorialCambios {
  private readonly registros: RegistroCambio[] = [];

  public registrarCambio(
    candidato: Candidato,
    anterior: FaseBase,
    nuevo: FaseBase,
    actor: string,
    tipo: TipoCambio,
    comentario?: string
  ): RegistroCambio {
    const registro = new RegistroCambio(
      candidato,
      anterior,
      nuevo,
      actor,
      tipo,
      comentario
    );
    this.registros.push(registro);
    return registro;
  }

  public deshacerUltimo(
    candidato: Candidato,
    actor: string,
    comentario: string
  ): RegistroCambio {
    const ultimoIndex = [...this.registros]
      .reverse()
      .findIndex(
        (r) => r.candidato === candidato && r.tipo !== TipoCambio.Deshacer
      );

    if (ultimoIndex === -1) {
      throw new Error(`No hay cambios para deshacer sobre ${candidato.nombre}`);
    }

    const realIndex = this.registros.length - 1 - ultimoIndex;
    const ultimo = this.registros.splice(realIndex, 1)[0];

    candidato.cambiarFase(ultimo.estadoAnterior);

    return this.registrarCambio(
      candidato,
      ultimo.estadoNuevo,
      ultimo.estadoAnterior,
      actor,
      TipoCambio.Deshacer,
      ultimo.comentario
    );
  }

  public obtenerRegistros(candidato: Candidato): readonly RegistroCambio[] {
    return this.registros.filter((r) => r.candidato === candidato);
  }
}
