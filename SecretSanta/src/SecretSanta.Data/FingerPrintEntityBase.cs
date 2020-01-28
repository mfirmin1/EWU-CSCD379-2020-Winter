using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        [Required]
        public string CreatedBy { get; set; } = String.Empty;
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string ModifiedBy { get; set; } = String.Empty;
        [Required]
        public DateTime ModifiedOn { get; set; }
    }
}
