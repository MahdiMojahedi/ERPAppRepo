using IDP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Master
    {
        public int MasterId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(20)]
        public string Code { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedByPersonId { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public ICollection<Subsidiary> Subsidiaries { get; set; }
    }

}
