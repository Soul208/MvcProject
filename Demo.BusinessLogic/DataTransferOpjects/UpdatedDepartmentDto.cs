using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferOpjects
{
    public class UpdatedDepartmentDto
    {
        public int Id { get; set; }
        public string Nmae { get; set; } = string.Empty!;

        public string Code { get; set; } = string.Empty!;

        public DateOnly DateOfCreate { get; set; }
        public string? Description { get; set; }
    }
}
