import { FaseBase } from "./FaseBase";
import { Aplicado } from "./Aplicado";
import { Entrevista } from "./Entrevista";
import { PruebaTecnica } from "./PruebaTecnica";
import { Oferta } from "./Oferta";
import { VerificacionReferencias } from "./VerificacionReferencias";
import { Contratado } from "./Contratado";
import { Rechazado } from "./Rechazado";

FaseBase.SiguientePaso["Aplicado"] = Entrevista;
FaseBase.SiguientePaso["Entrevista"] = PruebaTecnica;
FaseBase.SiguientePaso["Prueba técnica"] = Oferta;
FaseBase.SiguientePaso["Oferta"] = VerificacionReferencias;
FaseBase.SiguientePaso["Verificación de referencias"] = Contratado;
FaseBase.SiguientePaso["Rechazado"] = Rechazado;

export {
    FaseBase,
    Aplicado,
    Entrevista,
    PruebaTecnica,
    Oferta,
    VerificacionReferencias,
    Contratado,
    Rechazado
};
