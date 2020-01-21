using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintyEntityBase : EntityBase
    {
        private string _CreatedBy = String.Empty;
        private string _Modification = String.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string CreatedBy { get => _CreatedBy; set => _CreatedBy = value ?? throw new ArgumentNullException(nameof(CreatedBy)); }
        public string Modification { get => _Modification; set => _Modification = value ?? throw new ArgumentNullException(nameof(Modification)); }
        

    }
}
