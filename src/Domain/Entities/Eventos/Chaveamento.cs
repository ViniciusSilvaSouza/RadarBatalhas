using System;
using System.Collections.Generic;

namespace Domain.Entities.Eventos;

public class Chaveamento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public int Versao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? TravadoEm { get; set; }

    public List<RodadaChaveamento> Rodadas { get; set; } = new();
}
