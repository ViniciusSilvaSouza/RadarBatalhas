using System;

namespace Application.DTOs.Eventos;

public class RegistrarVencedorConfrontoRequest
{
    public Guid ConfrontoId { get; set; }
    public Guid VencedorId { get; set; }
}
