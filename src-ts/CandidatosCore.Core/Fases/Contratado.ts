import { FaseBase } from "./FaseBase";

export class Contratado extends FaseBase {
    public get nombre(): string {
        return "Contratado";
    }

    public override avanzar(): FaseBase {
        throw new Error(`Transición inválida: ${this.nombre} no tiene una siguiente etapa`);
    }

    public override rechazar(): void {
        throw new Error(`Transición inválida: ${this.nombre} -> Rechazado`);
    }
}
