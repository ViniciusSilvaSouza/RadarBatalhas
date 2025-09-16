namespace Application.DTOs.Usuarios;

public class CriarUsuarioRequest
{
    public Guid Id { get; set; } // deve ser o sub do Keycloak
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Papel { get; set; } = "VISUALIZADOR"; // ADMINISTRADOR | ORGANIZADOR | MC | VISUALIZADOR
}
