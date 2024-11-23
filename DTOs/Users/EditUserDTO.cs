namespace PostaAPI.DTOs.Users
{
    public class EditUserDTO : CreateUserDTO
    {
        public required int Id { get; set; }
    }
}
