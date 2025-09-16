using System;

namespace Domain.Entities.Usuarios;

public class PapelPermissao
{
    public Guid PapelId { get; set; }
    public Papel Papel { get; set; } = default!;
    public Guid PermissaoId { get; set; }
    public Permissao Permissao { get; set; } = default!;
}
