using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDI.TrabalhoFinal.Models
{
    public class Viagem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cpf do Passageiro")]
        public string CpfPassageiro { get; set; }

        [Required]
        [Display(Name = "Código do veículo")]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        [Display(Name = "Cpf do motorista")]
        public string CpfMotorista { get; set; }
        public Motorista Motorista { get; set; }

        [Display(Name = "Código do passageiro")]
        public int PasageiroId { get; set; }
        public Passageiro? Passageiro { get; set; }

        [Required]
        [Display(Name = "Origem")]
        public string LocalOrigem { get; set; }

        [Required]
        [Display(Name = "Destino")]
        public string LocalDestino { get; set; }

        [Required]
        [Display(Name = "Início")]
        public DateTime DataHoraInicio { get; set; }

        [Display(Name = "Fim")]
        public DateTime? DataHoraFim { get; set; }

        [Required]
        [Display(Name = "Forma de pagamento")]
        public string FormaPagamento { get; set; }

        [Display(Name = "Valor")]
        public double ValorPagar { get; set; }

        [Display(Name = "Código do gerente")]
        public int? GerenteId { get; set; }

        [Display(Name = "Foi cancelada?")]
        public bool FoiCancelada { get; set; }

        [Display(Name = "Pagamento a posteriori?")]
        public bool EhPagamentoPosteriori { get; set; }

    }
}
