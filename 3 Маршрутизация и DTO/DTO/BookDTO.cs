namespace _3_Маршрутизация_и_DTO.Valitador;
public record CreateBookDTO (int year, string author, string title);

public record UpdateBookDTO(Guid id, int year, string author, string title);

public record BookResponseDTO(Guid id, int year, string author, string title);