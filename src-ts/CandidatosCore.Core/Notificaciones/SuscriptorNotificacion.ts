import type { RegistroCambio } from "../Historial/RegistroCambio";
import { TipoCambio } from "../Historial/TipoCambio";
import { EmailService } from "./EmailService";
import type { ObservadorCambio } from "./ObservadorCambio";

export class SuscriptorNotificacion implements ObservadorCambio {
    public static readonly Todos = "*";

    public readonly destinatario: string;
    public readonly eventosDeInteres: Set<string>;
    public readonly conComentario: boolean;

    constructor(
        destinatario: string,
        eventosDeInteres: Iterable<string>,
        conComentario?: boolean
    ) {
        this.destinatario = destinatario;
        this.eventosDeInteres = new Set(eventosDeInteres);
        this.conComentario = conComentario ?? false;
    }

    public actualizar(cambio: RegistroCambio): void {
        if (
            !this.eventosDeInteres.has(SuscriptorNotificacion.Todos) &&
            !this.eventosDeInteres.has(cambio.estadoNuevo.nombre)
        ) {
            return;
        }

        const comentario = this.conComentario ? `${cambio.comentario ?? ""}` : undefined;

        EmailService.enviar(this.destinatario, SuscriptorNotificacion.describirCambio(cambio), comentario);
    }

    private static describirCambio(cambio: RegistroCambio): string {
        switch (cambio.tipo) {
            case TipoCambio.Deshacer:
                return `${cambio.candidato.nombre}: se deshizo el último cambio, ahora en ${cambio.estadoNuevo.nombre}`;
            case TipoCambio.Rechazo:
                return `${cambio.candidato.nombre} fue rechazado`;
            case TipoCambio.Avance:
                return `${cambio.candidato.nombre} pasó a ${cambio.estadoNuevo.nombre}`;
            default:
                return `Error al describir el cambio: ${cambio.tipo}`;
        }
    }
}
