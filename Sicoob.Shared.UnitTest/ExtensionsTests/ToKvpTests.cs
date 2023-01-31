/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Sicoob.Shared.Models.Geral;
using System;
using System.Linq;
using Xunit;

namespace Sicoob.Shared.UnitTest.ExtensionsTests
{
    public class ToKvpTests
    {
        [Fact]
        public void Extensions_ToVKP_PixConsulta()
        {
            var cons = new ConsultaRequest()
            {
                inicio = new System.DateTime(2020, 01, 01, 0, 0, 0, System.DateTimeKind.Utc),
                fim = new System.DateTime(2021, 12, 31, 0, 0, 0, System.DateTimeKind.Utc),

                cnpj = "17189722000139",

                paginacao = new Shared.Models.Geral.RequestPaginacao()
                {
                    paginaAtual = 0,
                    itensPorPagina = 200,
                }
            };

            var kvp = cons.ToKVP();
            var str = string.Join(';', kvp.Select(p => $"{p.Key}={p.Value}"));

            var expected = "inicio=2020-01-01T00:00:00.000+00:00;fim=2021-12-31T00:00:00.000+00:00;cnpj=17189722000139;paginacao.paginaAtual=0;paginacao.itensPorPagina=200";

            Assert.Equal(str, expected);
        }
    }

    public class ConsultaRequest
    {
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }
        public string? txid { get; set; }
        public bool? txIdPresente { get; set; }
        public bool? devolucaoPresente { get; set; }
        public string? cpf { get; set; }
        public string? cnpj { get; set; }

        public RequestPaginacao? paginacao { get; set; }

    }

}
