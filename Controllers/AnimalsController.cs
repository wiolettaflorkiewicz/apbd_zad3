using Microsoft.AspNetCore.Mvc;
using APBD_Zad3.DTOs;
using APBD_Zad3.Services;


namespace APBD_Zad3.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController(IAnimalService animalService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        return Ok(animalService.GetAnimals(orderBy));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = animalService.GetAnimal(id);

        if (animal == null)
        {
            return NotFound("Animal not found");
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(AnimalCreationDto animal)
    {
        var id = animalService.CreateAnimal(animal);
        return CreatedAtAction(nameof(CreateAnimal), id);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, AnimalUpdateDto animalUpdated)
    {
        var animal = animalService.GetAnimal(id);

        if (animal == null)
        {
            return NotFound("Animal not found");
        }

        _ = animalService.UpdateAnimal(id, animalUpdated);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = animalService.GetAnimal(id);

        if (animal == null)
        {
            return NotFound("Animal not found");
        }

        _ = animalService.DeleteAnimal(id);

        return NoContent();
    }
}