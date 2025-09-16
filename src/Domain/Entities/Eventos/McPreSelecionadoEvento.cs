using System;

namespace Domain.Entities.Eventos;

public class McPreSelecionadoEvento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public Guid UsuarioMcId { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
