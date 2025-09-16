using System;
using System.Collections.Generic;

namespace Domain.Entities.Usuarios;

public class Papel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public List<PapelPermissao> PapelPermissoes { get; set; } = new();
}
