﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class PermissionViewModel
    {
        public int Id { get; set; }

        public Guid IdPermission { get; set; }

        public string PermissionName { get; set; } = null!;
    }
}
