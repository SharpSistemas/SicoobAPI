using NUnit.Framework;
using System.Linq;

namespace Sicoob.Shared.UnitTest.ExtensionsTests
{
    public class ToKvpTests
    {
        [Test]
        public void Extensions_ToVKP_PixConsulta()
        {
            var cons = new PIX.Lib.Models.Pix.ConsultaRequest()
            {
                inicio = new System.DateTime(2020, 01, 01, 0, 0, 0, System.DateTimeKind.Utc),
                fim = new System.DateTime(2021, 12, 31, 0, 0, 0, System.DateTimeKind.Utc),

                cnpj = "17189722000139",

                paginacao = new Models.Geral.RequestPaginacao()
                {
                    paginaAtual = 0,
                    itensPorPagina = 200,
                }
            };

            var kvp = cons.ToKVP();
            var str = string.Join(';', kvp.Select(p => $"{p.Key}={p.Value}"));

            var expected = "inicio=2020-01-01T00:00:00.000+00:00;fim=2021-12-31T00:00:00.000+00:00;cnpj=17189722000139;paginacao.paginaAtual=0;paginacao.itensPorPagina=200";

            Assert.That(str, Is.EqualTo(expected));
        }
    }
}
