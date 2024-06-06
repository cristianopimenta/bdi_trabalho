using System.ComponentModel.DataAnnotations;

namespace BDI.TrabalhoFinal.Models
{
    public class ContaBancaria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Banco { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Required]
        [StringLength(20)]
        public string Conta { get; set; }
    }
}
