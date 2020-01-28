using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; } = String.Empty;
        public DateTime ModifiedOn { get; set; }
    }
}
