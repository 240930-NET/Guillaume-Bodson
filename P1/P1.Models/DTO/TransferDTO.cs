namespace P1.Models.DTO;

public class TransferDTO
{
    public int FromAccountId {get; set; }
    public int ToAccountId {get; set; }
    public double Amount {get; set; }
}