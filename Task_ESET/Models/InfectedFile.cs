using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_ESET.Models
{
    public class InfectedFile : BaseFileModel
    {
        public List<Child>? Children { get; set; } = new List<Child>();
        public bool IsArchive { get; set; }
    }
}
