namespace P1.Models.DTO;

//Data Transfer Object used for login and register
public class PasswordChangeDTO
{
    public string? Username {get; set; }
    public string? OldPassword {get; set; } 
    public string? NewPassword {get; set; }

    public PasswordChangeDTO(){}

    public PasswordChangeDTO(string username, string oldPassword, string newPasword) {
        this.Username = username;
        this.OldPassword = oldPassword;
        this.NewPassword = newPasword;
    }
}
