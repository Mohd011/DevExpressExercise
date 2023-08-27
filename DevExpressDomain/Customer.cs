using DevExpressDomain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevExpressDomain
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Invalid Phone format.")]
        public string Phone { get; set; }

        public CustomerStatus Status { get; set; } = CustomerStatus.Active;

        public bool IsNew => Id == 0;
    }
}