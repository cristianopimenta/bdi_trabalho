using System.ComponentModel.DataAnnotations;

namespace BDI.TrabalhoFinal.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Placa { get; set; }

        [Required]
        [StringLength(50)]
        public string Marca { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Ano de fabricação")]
        public int AnoFabricacao { get; set; }

        [Required]
        public int Capacidade { get; set; }

        [Required]
        [StringLength(20)]
        public string Cor { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Tipo de combustível")]
        public string TipoCombustivel { get; set; }

        [Required]
        [Display(Name = "Potência do motor")]
        public int PotenciaMotor { get; set; }

        // Relacionamento com Proprietario
        [Display(Name = "Código do proprietário")]
        public int ProprietarioId { get; set; }
        public Proprietario Proprietario { get; set; }

        // Relacionamento com Motorista
        [Display(Name = "Código do motorista")]
        public int? MotoristaId { get; set; }
        public Motorista Motorista { get; set; }
    }
}
