using System;
using System.Collections.Generic;

namespace Domain.Entities.Eventos;

public class RodadaChaveamento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ChaveamentoId { get; set; }
    public int NumeroRodada { get; set; }

    public List<Confronto> Confrontos { get; set; } = new();
}
