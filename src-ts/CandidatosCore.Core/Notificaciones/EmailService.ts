export class EmailService {
  public static enviar(
    destinatario: string,
    mensaje: string,
    comentario?: string
  ): void {
    console.log(
      `[correo -> ${destinatario}] ${mensaje} ${
        comentario ? `[${comentario}]` : ""
      }`
    );
  }
}
