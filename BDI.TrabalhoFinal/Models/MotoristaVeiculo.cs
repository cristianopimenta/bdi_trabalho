using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDI.TrabalhoFinal.Models
{
    public class MotoristaVeiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FK_MotoristaVeiculo_Veiculos_VeiculosId")]
        public int MotoristaId { get; set; }

        [Required]
        [ForeignKey("FK_MotoristaVeiculo_Motorista_MotoristaId")]
        public int VeiculoId { get; set; }
    }
}
