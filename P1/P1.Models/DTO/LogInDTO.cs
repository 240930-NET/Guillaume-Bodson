namespace P1.Models.DTO;

//Data Transfer Object used for login and register
public class LogInDTO
{
    public string? Username {get; set; }
    public string? Password {get; set; } 

    public LogInDTO(){}

    public LogInDTO(string username, string password) {
        this.Username = username;
        this.Password = password;
    }
}
