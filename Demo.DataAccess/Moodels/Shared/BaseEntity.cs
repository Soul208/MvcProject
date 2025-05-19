using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Moodels.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; } // Pk
        public int CreateBy { get; set; } //Usre ID
        public DateTime CreateOn { get; set; }
        public int LastModifiedBy { get; set; } //Usre Id
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } // Soft Delete
    }
}
