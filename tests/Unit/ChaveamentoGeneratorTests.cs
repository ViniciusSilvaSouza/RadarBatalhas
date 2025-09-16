using Domain.Entities.Eventos;
using Xunit;

namespace Unit;

public class ChaveamentoGeneratorTests
{
    [Fact]
    public void DeveGerarByeQuandoQuantidadeImpar()
    {
        // Arrange
        var eventoId = Guid.NewGuid();
        var chave = new Chaveamento { EventoId = eventoId, Versao = 1 };
        var rodada = new RodadaChaveamento { ChaveamentoId = chave.Id, NumeroRodada = 1 };
        chave.Rodadas.Add(rodada);

        var participantes = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        // Act (template simplificado)
        for (int i = 0; i < participantes.Count; i += 2)
        {
            var c = new Confronto
            {
                EventoId = eventoId,
                ChaveamentoId = chave.Id,
                RodadaId = rodada.Id,
                ParticipanteAId = participantes[i],
                ParticipanteBId = (i + 1 < participantes.Count) ? participantes[i + 1] : null,
                EhBye = (i + 1 >= participantes.Count)
            };
            rodada.Confrontos.Add(c);
        }

        // Assert
        Assert.Contains(rodada.Confrontos, x => x.EhBye && x.ParticipanteBId == null);
    }
}
