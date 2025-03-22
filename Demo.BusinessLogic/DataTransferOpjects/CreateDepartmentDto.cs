using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferOpjects
{
    public class CreateDepartmentDto
    {
        public string Nmae { get; set; } = null!;

        public string Code { get; set; } = null!;

        public DateOnly DateOfCreate { get; set; }
        public string? Description { get; set; }

    }
}
