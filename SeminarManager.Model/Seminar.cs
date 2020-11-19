using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarManager.Model
{
    public class Seminar : Entity
    {
        [Required]
        public string Name { get; set; }

        [Extent]
        public string Extent { get; set; }
    }
}
