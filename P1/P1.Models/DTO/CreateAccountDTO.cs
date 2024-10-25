namespace P1.Models.DTO;

public class CreateAccountDTO
{
    public string? Name {get; set; }
    //Foreign Key for Customer table
    public int CustomerId {get; set;}

}