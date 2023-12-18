/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
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