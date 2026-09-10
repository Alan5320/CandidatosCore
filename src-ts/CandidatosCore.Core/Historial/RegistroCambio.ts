import type { Candidato } from "../Dominio/Candidato";
import type { FaseBase } from "../Fases";
import { TipoCambio } from "./TipoCambio";

export class RegistroCambio {
    public readonly candidato: Candidato;
    public readonly estadoAnterior: FaseBase;
    public readonly estadoNuevo: FaseBase;
    public readonly actor: string;
    public readonly fecha: Date;
    public readonly tipo: TipoCambio;
    public readonly comentario?: string;

    constructor(
        candidato: Candidato,
        estadoAnterior: FaseBase,
        estadoNuevo: FaseBase,
        actor: string,
        tipo: TipoCambio,
        comentario?: string
    ) {
        this.candidato = candidato;
        this.estadoAnterior = estadoAnterior;
        this.estadoNuevo = estadoNuevo;
        this.actor = actor;
        this.tipo = tipo;
        this.fecha = new Date();
        this.comentario = comentario;
    }
}
