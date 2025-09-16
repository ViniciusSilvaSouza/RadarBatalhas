namespace Application.DTOs.Common;

public record ErrorDetails(int Status, string Code, string Message, string CorrelationId);
