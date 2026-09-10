import type { RegistroCambio } from "../Historial/RegistroCambio";

export interface ObservadorCambio {
    actualizar(cambio: RegistroCambio): void;
}
