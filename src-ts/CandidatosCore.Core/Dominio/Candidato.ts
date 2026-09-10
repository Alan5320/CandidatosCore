import { FaseBase, Aplicado } from "../Fases";

export class Candidato {
    public readonly nombre: string;
    public readonly correo: string;
    private faseActual!: FaseBase;

    constructor(nombre: string, correo: string) {
        this.nombre = nombre;
        this.correo = correo;
        this.cambiarFase(new Aplicado());
    }

    public avanzar(): void {
        this.faseActual.avanzar();
    }

    public rechazar(): void {
        this.faseActual.rechazar();
    }

    public cambiarFase(nuevaFase: FaseBase): void {
        nuevaFase.setContext(this);
        this.faseActual = nuevaFase;
    }

    public obtenerFaseActual(): FaseBase {
        return this.faseActual;
    }
}
