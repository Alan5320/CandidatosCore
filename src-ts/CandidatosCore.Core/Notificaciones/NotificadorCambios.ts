import type { RegistroCambio } from "../Historial/RegistroCambio";
import type { ObservadorCambio } from "./ObservadorCambio";

export class NotificadorCambios {
    private readonly observadores: ObservadorCambio[] = [];

    public suscribir(observador: ObservadorCambio): void {
        this.observadores.push(observador);
    }

    public notificar(cambio: RegistroCambio): void {
        for (const observador of this.observadores) {
            observador.actualizar(cambio);
        }
    }
}
