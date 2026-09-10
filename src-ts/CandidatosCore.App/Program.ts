import { Candidato } from "../CandidatosCore.Core/Dominio/Candidato";
import { GestorDeCandidato } from "../CandidatosCore.Core/GestorDeCandidato";
import { HistorialCambios } from "../CandidatosCore.Core/Historial/HistorialCambios";
import { NotificadorCambios } from "../CandidatosCore.Core/Notificaciones/NotificadorCambios";
import { SuscriptorNotificacion } from "../CandidatosCore.Core/Notificaciones/SuscriptorNotificacion";

const historial = new HistorialCambios();
const notificador = new NotificadorCambios();

notificador.suscribir(
  new SuscriptorNotificacion(
    "reclutador@hirecore.com",
    [SuscriptorNotificacion.Todos],
    true
  )
);
notificador.suscribir(
  new SuscriptorNotificacion(
    "gerente.contratacion@hirecore.com",
    ["Oferta", "Contratado"],
    true
  )
);
notificador.suscribir(
  new SuscriptorNotificacion("nomina@hirecore.com", ["Contratado"], true)
);
notificador.suscribir(
  new SuscriptorNotificacion("portal@hirecore.com", [
    //Portal recibe todos los cambios, sin comentarios
    SuscriptorNotificacion.Todos,
  ])
);

const gestor = new GestorDeCandidato(historial, notificador);
const candidato = new Candidato("Ana Torres", "ana.torres@correo.com");

console.log(`--- Proceso de ${candidato.nombre} ---`);

gestor.avanzar(candidato, "reclutador.laura", "Avanza por que me cae bien");
gestor.avanzar(candidato, "reclutador.laura", "Avanza por que me cae bien");
gestor.avanzar(candidato, "reclutador.laura", "Avanza por que me cae bien");

console.log(`Fase actual: ${candidato.obtenerFaseActual().nombre}`);

gestor.rechazar(
  candidato,
  "reclutador.laura",
  "Rechaza por que no me cae bien"
);
console.log(`Fase actual: ${candidato.obtenerFaseActual().nombre}`);

gestor.deshacerUltimo(
  candidato,
  "rrhh.supervisor",
  "Deshace porque no me cae bien"
);
console.log(
  `Fase actual tras deshacer: ${candidato.obtenerFaseActual().nombre}`
);

for (const registro of historial.obtenerRegistros(candidato)) {
  console.log(
    `${registro.fecha.toISOString()} | ${registro.actor} | ${registro.tipo} | ${
      registro.estadoAnterior.nombre
    } -> ${registro.estadoNuevo.nombre}`
  );
}
