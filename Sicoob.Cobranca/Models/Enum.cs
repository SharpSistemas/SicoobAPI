namespace Sicoob.Cobranca.Models;


    public enum IdentificacaoEmissaoBoleto: int
    {
        BancoEmite = 1,
        ClienteEmite = 2
    }

    public enum IdentificacaoDistribuicaoBoleto : int
    {
        BancoDistribui = 1,
        ClienteDistribui = 2
    }

    public enum TipoDesconto : int
    {
        SemDesconto = 0,
        ValorFixoAteDataInformada = 1,
        PercentualAteDataInformada = 2,
        ValorPorAntecipacaoDiaCorrido = 3,
        ValorPorAntecipacaoDiaUtil = 4,
        PercentualPorAntecipacaoDiaCorrido = 5,
        PercentualPorAntecipacaoDiaUtil = 6
    }

    public enum TipoJurosMora : int
    {
        ValorPorDia = 1,
        TaxaMensal = 2,
        Isento = 3
    }
    
    public enum TipoMulta : int
    {
        Isento = 0,
        ValorFixo = 1,
        Percentual = 2
    }
    
    public enum CodigoNegativacao : int
    {
        NegativarDiasUteis = 2,
        NaoNegativar = 3
    }

    public enum CodigoProtesto : int
    {
        ProtestarDiasCorridos = 1,
        ProtestarDiasUteis = 2,
        NaoProtestar = 3,
    }

    public enum CodigoCadastrarPIX : int
    {
        Padrao = 0,
        ComPIX = 1,
        SemPIX = 1
    }
    
    public enum Modalidade : int
    {
        SimplesComRegistro = 1,
        CarneDePagamentos = 5,
        Indexada = 6,
        CartaoDeCredito = 14
    }
    public struct SituacaoBoleto
    {
        public const string Liquidado = "Liquidado";
        public const string EmAberto = "Em Aberto";
        public const string Baixado = "Baixado";
    }

    public enum TipoHistorico : int 
    {
        Entrada = 1,
        Alteracao = 2,
        Prorrogacao = 3,
        Terifas = 4,
        Protesto = 5,
        LiquidacaoBaixa = 6
    }
    