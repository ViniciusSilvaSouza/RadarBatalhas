using System;

namespace Domain.Compartilhado.Contracts;

public interface IProvedorDataHora
{
    DateTime UtcNow { get; }
}
