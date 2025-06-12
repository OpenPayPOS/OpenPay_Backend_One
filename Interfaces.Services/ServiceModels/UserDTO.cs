namespace OpenPay.Interfaces.Services.ServiceModels;
public struct UserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public List<UserAccountDTO> Accounts { get; set; }
    public UserAccountDTO? ActiveAccount { get; set; }
}