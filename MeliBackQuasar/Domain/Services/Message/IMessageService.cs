namespace Domain.Services.Message;

public interface IMessageService
{
    /// <summary>
    /// Metodo encargado de verificar si es posible leer el mensaje
    /// </summary>
    /// <param name="messages">Lista de mensajes recibidos</param>
    /// <returns>Mensaje descubierto</returns>
    string GetMessage(List<List<string>> messages);
}

