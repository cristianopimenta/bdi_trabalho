using System.ComponentModel.DataAnnotations;

namespace BDI.TrabalhoFinal.Models
{
    public class Passageiro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Cartão de crédito")]
        public string CartaoCredito { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        [StringLength(100)]
        [Display(Name = "Cidade de origem")]
        public string CidadeOrigem { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
