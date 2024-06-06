using System.ComponentModel.DataAnnotations;

namespace BDI.TrabalhoFinal.Models
{
    public class Motorista
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

        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(11)]
        public string CNH { get; set; }

        // Relacionamento com ContaBancaria
        [Display(Name = "Código conta bancária")]
        public int ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }


        // Relacionamento com Veiculo
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
