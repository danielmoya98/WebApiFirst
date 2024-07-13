namespace WebApiFirst.Models;

public class Animal
{
    public int AnimalID { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public int EspecieID { get; set; }
    public Especie Especie { get; set; }
    public int HabitatID { get; set; }
    public Habitat Habitat { get; set; }
}