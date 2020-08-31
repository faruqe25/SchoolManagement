using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Models
{
    public class SectionCapacity
    {
        [Key]
        public int SectionCapacityId { get; set; }
        public int Capacity{ get; set; }
        public Int64 SectionYear { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }

    }
}
