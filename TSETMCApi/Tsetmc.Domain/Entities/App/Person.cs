using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class Person
    {
        public Person()
        {
            ShareHolders = new HashSet<ShareHolder>();
        }

        public int Id { get; set; }
        public string PersonCode { get; set; } = null!;
        public string PersonName { get; set; } = null!;
        public string? PersonType { get; set; }

        public virtual ICollection<ShareHolder> ShareHolders { get; set; }
    }
}
