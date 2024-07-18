using System.ComponentModel.DataAnnotations;
public class AnOrigin
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string VideoLink { get; set; }
    public List<string> Episodes { get; set; }
}