using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_ESET.Models
{
    /// <summary>
    /// Represents the base class for models in Task_ESET from which other models inherit.
    /// </summary>
    public class BaseFileModel
    {
        public string Name { get; set; } = null!;
        public string? Threat { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Info { get; set; } = null!;
    }
}
