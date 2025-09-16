using System;
using System.Collections.Generic;

namespace Domain.Entities.Usuarios;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EhMc { get; set; }
    public bool EhOrganizador { get; set; }

    public List<UsuarioPapel> UsuarioPapeis { get; set; } = [];
}
