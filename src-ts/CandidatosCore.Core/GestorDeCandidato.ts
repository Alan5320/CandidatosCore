import type { Candidato } from "./Dominio/Candidato";
import type { HistorialCambios } from "./Historial/HistorialCambios";
import { TipoCambio } from "./Historial/TipoCambio";
import type { NotificadorCambios } from "./Notificaciones/NotificadorCambios";

export class GestorDeCandidato {
  private readonly historial: HistorialCambios;
  private readonly notificador: NotificadorCambios;

  constructor(historial: HistorialCambios, notificador: NotificadorCambios) {
    this.historial = historial;
    this.notificador = notificador;
  }

  public avanzar(
    candidato: Candidato,
    actor: string,
    comentario: string
  ): void {
    this.ejecutar(
      candidato,
      actor,
      TipoCambio.Avance,
      () => candidato.avanzar(),
      comentario
    );
  }

  public rechazar(
    candidato: Candidato,
    actor: string,
    comentario: string
  ): void {
    this.ejecutar(
      candidato,
      actor,
      TipoCambio.Rechazo,
      () => candidato.rechazar(),
      comentario
    );
  }

  public deshacerUltimo(
    candidato: Candidato,
    actor: string,
    comentario: string
  ): void {
    const registro = this.historial.deshacerUltimo(
      candidato,
      actor,
      comentario
    );
    this.notificador.notificar(registro);
  }

  private ejecutar(
    candidato: Candidato,
    actor: string,
    tipo: TipoCambio,
    transicion: () => void,
    comentario: string
  ): void {
    const anterior = candidato.obtenerFaseActual();
    transicion();
    const nuevo = candidato.obtenerFaseActual();

    const registro = this.historial.registrarCambio(
      candidato,
      anterior,
      nuevo,
      actor,
      tipo,
      comentario
    );
    this.notificador.notificar(registro);
  }
}
