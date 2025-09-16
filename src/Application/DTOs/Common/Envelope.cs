namespace Application.DTOs.Common;

public record Envelope<T>(T Data, string CorrelationId)
{
    public static Envelope<T> Ok(T data, string correlationId = "") => new(data, correlationId);
}
