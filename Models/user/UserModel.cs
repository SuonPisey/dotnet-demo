using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class UserModel
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("Username")]
    public string Username { get; set; } = string.Empty;

    [Column("Email")]
    public string Email { get; set; } = string.Empty;

    [Column("PasswordHash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("FirstName")]
    public string FirstName { get; set; } = string.Empty;

    [Column("LastName")]
    public string LastName { get; set; } = string.Empty;

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }
}