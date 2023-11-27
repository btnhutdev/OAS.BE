using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public Guid IdPermission { get; set; }

        public string PermissionName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
