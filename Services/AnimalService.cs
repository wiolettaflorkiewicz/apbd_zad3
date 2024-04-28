using APBD_Zad3.DTOs;
using APBD_Zad3.Models;
using APBD_Zad3.Repositories;


namespace APBD_Zad3.Services;

public class AnimalService(IAnimalRepository animalRepository) : IAnimalService
{
    public IEnumerable<AnimalDto> GetAnimals(string orderBy)
    {
        return animalRepository.GetAnimals(orderBy).Select(a => new AnimalDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            Category = a.Category,
            Area = a.Area
        });
    }

    public AnimalDto GetAnimal(int id)
    {
        var animal = animalRepository.GetAnimal(id);

        if (animal == null)
        {
            return null!;
        }

        return new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Description = animal.Description,
            Category = animal.Category,
            Area = animal.Area
        };
    }

    public int CreateAnimal(AnimalCreationDto animal)
    {
        return animalRepository.CreateAnimal(new Animal
        {
            Name = animal.Name,
            Description = animal.Description,
            Category = animal.Category,
            Area = animal.Area
        });
    }

    public int UpdateAnimal(int id, AnimalUpdateDto animal)
    {
        return animalRepository.UpdateAnimal(new Animal
        {
            Id = id,
            Name = animal.Name,
            Description = animal.Description,
            Category = animal.Category,
            Area = animal.Area
        });
    }

    public int DeleteAnimal(int id)
    {
        return animalRepository.DeleteAnimal(id);
    }
}