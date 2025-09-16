using Domain.Enums;
using System;

namespace Domain.Entities.Eventos;

public class Confronto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RodadaId { get; set; }
    public Guid ChaveamentoId { get; set; }
    public Guid EventoId { get; set; }
    public Guid? ParticipanteAId { get; set; }
    public Guid? ParticipanteBId { get; set; }
    public Guid? VencedorId { get; set; }
    public bool EhBye { get; set; }
    public bool VencedorPorWalkover { get; set; }
    public StatusConfronto IdStatusConfronto { get; set; } = StatusConfronto.PENDENTE;
}
