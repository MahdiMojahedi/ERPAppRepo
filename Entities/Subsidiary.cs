using IDP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Subsidiary: IValidatableObject
    {
        public int SubsidiaryId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(20)]
        public string Code { get; set; }

        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }

        public bool IsLastLevel { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int MasterId { get; set; }
        public Master Master { get; set; }

        public int CreatedByPersonId { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public int? ParentSubsidiaryId { get; set; }
        public Subsidiary ParentSubsidiary { get; set; }
        public ICollection<Subsidiary> Children { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsLastLevel)
            {
                if (DebitAmount is null || CreditAmount is null)
                    yield return new ValidationResult("برای سطح نهایی، مقادیر بدهکار و بستانکار الزامی هستند.",
                                                      new[] { nameof(DebitAmount), nameof(CreditAmount) });
            }
            else
            {
                if (DebitAmount.HasValue || CreditAmount.HasValue)
                    yield return new ValidationResult("برای سطح غیر نهایی، نباید مقدار بدهکار یا بستانکار وارد شود.",
                                                      new[] { nameof(DebitAmount), nameof(CreditAmount) });
            }
        }
    }

}
