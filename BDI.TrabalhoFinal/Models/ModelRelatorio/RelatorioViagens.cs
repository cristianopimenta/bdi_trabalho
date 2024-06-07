namespace BDI.TrabalhoFinal.Models.ModelRelatorio
{


    public class Veiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public List<Viagem> Viagens { get; set; }
    }

    public class Viagem
    {
        public int Id { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string LocalOrigem { get; set; }
        public string LocalDestino { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public int MotoristaId { get; set; }
        public Motorista Motorista { get; set; }
        public int PassageiroId { get; set; }
        public Passageiro Passageiro { get; set; }
    }

    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class Passageiro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}

