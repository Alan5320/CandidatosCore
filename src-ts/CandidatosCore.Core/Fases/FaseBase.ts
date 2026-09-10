import type { Candidato } from "../Dominio/Candidato";

export abstract class FaseBase {
    protected context!: Candidato;

    public static readonly SiguientePaso: Record<string, new () => FaseBase> = {};

    public abstract get nombre(): string;

    public setContext(candidato: Candidato): void {
        this.context = candidato;
    }

    public avanzar(): FaseBase {
        const FaseSiguiente = FaseBase.SiguientePaso[this.nombre];
        if (FaseSiguiente) {
            const nuevaFase = new FaseSiguiente();
            this.context?.cambiarFase(nuevaFase);
            return nuevaFase;
        }

        throw new Error(`Transición inválida: ${this.nombre} no tiene una siguiente etapa`);
    }

    public rechazar(): void {
        const FaseRechazado = FaseBase.SiguientePaso["Rechazado"];
        if (FaseRechazado) {
            const nuevaFase = new FaseRechazado();
            this.context?.cambiarFase(nuevaFase);
            return;
        }

        throw new Error(`Transición inválida: ${this.nombre} -> Rechazado`);
    }
}
