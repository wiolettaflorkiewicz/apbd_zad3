using System.ComponentModel.DataAnnotations;

namespace APBD_Zad3.DTOs;

public class AnimalUpdateDto
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string Area { get; init; } = null!;
}