using System.ComponentModel.DataAnnotations;

namespace BDI.TrabalhoFinal.Models
{
    public class Pessoa
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
        public string Endereco { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(11)]
        public string CNH { get; set; }

        public int ContaBancariaId { get; set; }
        public ContaBancaria? ContaBancaria { get; set; }
    }
}
