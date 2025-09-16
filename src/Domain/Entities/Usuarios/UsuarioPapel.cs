using System;

namespace Domain.Entities.Usuarios;

public class UsuarioPapel
{
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = default!;
    public Guid PapelId { get; set; }
    public Papel Papel { get; set; } = default!;
}
