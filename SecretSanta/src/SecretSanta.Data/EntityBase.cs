using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Data
{
    public class EntityBase
    {
        [Required]
        public int Id { get; internal set; }
    }
}
