namespace Core.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public Guid IdUser { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool? Gender { get; set; }

        public Guid? IdPermission { get; set; }

        public string PermissionName { get; set; } = null!;

    }
}
