
public record CreateBookDTO (int year, string author, string title);

public record UpdateBookDTO(Guid id, int year, string author, string title);

public record ResponseBookDTO(Guid id, int year, string author, string title);