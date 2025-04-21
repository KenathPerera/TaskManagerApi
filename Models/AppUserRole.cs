public class AppUserRole
{
    public int UserId { get; set; }
    public AppUser User { get; set; }

    public int RoleId { get; set; }
    public AppRole Role { get; set; }
}
