using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class GroupInput
    {
        [Required]
        public string? Title { get; set; }
    }
}
