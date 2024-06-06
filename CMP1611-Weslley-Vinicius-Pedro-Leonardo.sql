CREATE TABLE Veiculos ( 
    Id SERIAL NOT NULL,
    Placa VARCHAR(10) NOT NULL,
    Marca VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    AnoFabricacao INT NOT NULL,
    Capacidade INT NOT NULL,
    Cor VARCHAR(20) NOT NULL,
    Tipo_combustivel CHAR(1) NOT NULL CHECK (Tipo_combustivel IN ('G', 'A', 'D', 'F')),
    PotenciaMotor INT NOT NULL,
    ProprietarioId INT NOT NULL,
    MotoristaId INT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (MotoristaId) REFERENCES Motoristas(Id),
    FOREIGN KEY (ProprietarioId) REFERENCES Proprietarios(Id)
);

CREATE TABLE Passageiros (
    Id SERIAL PRIMARY KEY,
    CPF VARCHAR(14) NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    Endereco VARCHAR(100) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    CartaoCredito VARCHAR(20) NOT NULL,
    Sexo CHAR(1) CHECK (Sexo IN ('M', 'F')),
    CidadeOrigem VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

CREATE TABLE Motoristas (
    Id SERIAL NOT NULL,
    CPF VARCHAR(14) NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    Endereco VARCHAR(100) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Sexo CHAR(1) CHECK (sexo IN ('M', 'F')),
    CNH VARCHAR(11) NOT NULL,
    ContaBancariaId INT NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT FK_Motoristas_ContasBancarias FOREIGN KEY (ContaBancariaId) REFERENCES ContasBancarias(Id)
);

CREATE TABLE Proprietarios (
    Id SERIAL NOT NULL,
    CPF VARCHAR(14) NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    Endereco VARCHAR(100) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Sexo CHAR(1) CHECK (sexo IN ('M', 'F')),
    CNH VARCHAR(11) NOT NULL,
    ContaBancariaId INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (ContaBancariaId) REFERENCES ContasBancarias(Id)
);


CREATE TABLE Viagens (
    Id SERIAL NOT NULL,
    CpfPassageiro VARCHAR(255) NOT NULL,
    VeiculoId INT NOT NULL,
    CpfMotorista VARCHAR(255) NOT NULL,
    MotoristaId INT NOT NULL,
    PasageiroId INT NOT NULL,
    PassageiroId INT NULL,
    LocalOrigem VARCHAR(255) NOT NULL,
    LocalDestino VARCHAR(255) NOT NULL,
    DataHoraInicio TIMESTAMP(6) NOT NULL,
    DataHoraFim TIMESTAMP(6) NULL,
    FormaPagamento VARCHAR(255) NOT NULL,
    ValorPagar DECIMAL(18, 2) NOT NULL,
    GerenteId INT NULL,
    FoiCancelada BOOLEAN NULL,
    EhPagamentoPosteriori BOOLEAN NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (MotoristaId) REFERENCES Motoristas(Id),
    FOREIGN KEY (PassageiroId) REFERENCES Passageiros(Id),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id),
    CONSTRAINT uk_viagem UNIQUE (CpfPassageiro, DataHoraInicio)
);

CREATE TABLE VeiculoMotorista (
    VeiculoId INT NOT NULL,
    MotoristaId INT NOT NULL,
    PRIMARY KEY (VeiculoId, MotoristaId),
    FOREIGN KEY (VeiculoId) REFERENCES Veiculos(Id),
    FOREIGN KEY (MotoristaId) REFERENCES Motoristas(Id)
);

CREATE TABLE ContasBancarias (
    Id SERIAL NOT NULL,
    Banco VARCHAR(50) NOT NULL,
    Agencia VARCHAR(10) NOT NULL,
    Conta VARCHAR(20) NOT NULL,
    PRIMARY KEY (Id)
);


-- verificação de cancelamento de viagem
CREATE OR REPLACE FUNCTION verifica_cancelamento_func()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.FoiCancelada IS TRUE THEN
        IF NEW.GerenteId IS NULL THEN
            RAISE EXCEPTION 'O código do gerente deve ser fornecido para cancelamentos';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER verifica_cancelamento
BEFORE INSERT ON Viagens
FOR EACH ROW
EXECUTE FUNCTION verifica_cancelamento_func();