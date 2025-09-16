using System;

namespace Domain.Entities.Usuarios;

public class Permissao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Codigo { get; set; } = string.Empty;
}
