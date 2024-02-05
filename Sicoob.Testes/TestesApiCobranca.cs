/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/

using Sicoob.Cobranca.Models;

namespace Sicoob.Testes;

using Newtonsoft.Json;
using Sicoob.Cobranca;
using Sicoob.Shared.Models;
using System;
using System.IO;
using System.Threading.Tasks;

public static class TestesApiCobranca
{
    internal static async Task Run_Cobranca()
    {
        // carrega do disco
        var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config_Cob.json")) ?? throw new Exception();
        //cfg.Scope.RemoverTodos();
        //cfg.Scope.Cobranca_Setar(true);
        //File.WriteAllText("config_Cob.json", JsonConvert.SerializeObject(cfg));

        var cobranca = new SicoobCobranca(cfg, NumeroContrato: 000);
        await cobranca.SetupAsync();

        var boleto = await cobranca.IncluirBoletos(new []
        {
            new IncluirBoletosRequest()
            {
                numeroContrato= 25546454,
                modalidade= 1,
                numeroContaCorrente= 0,
                especieDocumento= "DM",
                dataEmissao= new DateTime(2018,09, 20).ToUniversalTime(),
                nossoNumero= 2588658,
                seuNumero= "1235512",
                identificacaoBoletoEmpresa= "4562",
                identificacaoEmissaoBoleto= 1,
                identificacaoDistribuicaoBoleto= 1,
                valor= 156.23m,
                dataVencimento= new DateTime(2018,09, 20).ToUniversalTime(),
                dataLimitePagamento= new DateTime(2018,09, 20).ToUniversalTime(),
                valorAbatimento= 1,
                tipoDesconto= 1,
                dataPrimeiroDesconto= new DateTime(2018,09, 20).ToUniversalTime(),
                valorPrimeiroDesconto= 1,
                dataSegundoDesconto= new DateTime(2018,09, 20).ToUniversalTime(),
                valorSegundoDesconto= 0,
                dataTerceiroDesconto= new DateTime(2018,09, 20).ToUniversalTime(),
                valorTerceiroDesconto= 0,
                tipoMulta= 1,
                dataMulta= new DateTime(2018,09, 20).ToUniversalTime(),
                valorMulta= 5,
                tipoJurosMora= 1,
                dataJurosMora= new DateTime(2018,09, 20).ToUniversalTime(),
                valorJurosMora= 4,
                numeroParcela= 1,
                aceite= true,
                codigoNegativacao= 2,
                numeroDiasNegativacao= 60,
                codigoProtesto= 1,
                numeroDiasProtesto= 30,
                pagador = new DadosPagador()
                {
                    numeroCpfCnpj = "98765432185",
                    nome = "Marcelo dos Santos",
                    endereco = "Rua 87 Quadra 1 Lote 1 casa 1",
                    bairro = "Santa Rosa",
                    cidade = "Luziânia",
                    cep = "72320000",
                    uf = "DF",
                    email = new[] {
                        "pagador@dominio.com.br"
                    },
                },
                beneficiarioFinal = new Beneficiariofinal()
                {
                    numeroCpfCnpj = "98784978699",
                    nome = "Lucas de Lima"
                },
                mensagensInstrucao = new Mensagensinstrucao()
                {
                    tipoInstrucao = 1,
                    mensagens = new [] {
                        "Descrição da Instrução 1",
                        "Descrição da Instrução 2",
                        "Descrição da Instrução 3",
                        "Descrição da Instrução 4",
                        "Descrição da Instrução 5"
                    }
                },
                rateioCreditos = new [] {
                    new Rateiocredito()
                    {
                        numeroBanco = 756,
                        numeroAgencia = 4027,
                        numeroContaCorrente = 0,
                        contaPrincipal = true,
                        codigoTipoValorRateio = 1,
                        valorRateio = 156.23m,
                        codigoTipoCalculoRateio = 1,
                        numeroCpfCnpjTitular = "98765432185",
                        nomeTitular = "Marcelo dos Santos",
                        codigoFinalidadeTed = 10,
                        codigoTipoContaDestinoTed = "CC",
                        quantidadeDiasFloat = 1,
                        dataFloatCredito = "2020-12-30"
                    }
                },
                gerarPdf = true,
                codigoCadastrarPIX = 1
            }   
        });
        
        //var boleto = await cobranca.ConsultarBoleto(nossoNumero: 0);
        //var consulta = await cobranca.ConsultarBoletosPagador(numeroCpfCnpj: "00000000000000");
        //var segVia = await cobranca.ConsultarSegundaViaBoleto(1, nossoNumero: 0, gerarPdf: true);

        var p1 = await cobranca.SolicitarMovimentacao(Cobranca.Models.SolicitacaoMovimentacoesCarteira.Tipo.Liquidacao, new DateTime(2023, 12, 14));
        int codigo = p1.codigoSolicitacao;

        Cobranca.Models.RetornoConsultaMovimentacoes? p2 = null;
        while (p2 == null)
        {
            Console.WriteLine("Consultando ...");
            p2 = await cobranca.ConsultarSituacaoSolicitacao(codigoSolicitacao: codigo);
            await Task.Delay(1000);
        }

        int[] idsArquivos = p2.idArquivos;
        var p3 = await cobranca.BaixarMovimentacoes(codigo, idsArquivos);
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
        cfg = cfg;
    }
}