﻿using NUnit.Framework;
using PIX.Sicoob.Lib;
using System.Linq;

namespace PIX.Sicoob.UnitTest.ExtensionsTests
{
    public class ToKvpTests
    {
        [Test]
        public void Extensions_ToVKP_PixConsulta()
        {
            var cons = new Lib.Models.Pix.ConsultaRequest()
            {
                inicio = new System.DateTime(2020, 01, 01),
                fim = new System.DateTime(2021, 12, 31),

                cnpj = "17189722000139",

                paginacao = new Lib.Models.Pix.Paginacao()
                {
                    paginaAtual = 0,
                    itensPorPagina = 200,
                }
            };

            var kvp = cons.ToKVP();
            var str = string.Join(';', kvp.Select(p => $"{p.Key}={p.Value}"));

            var expected = "inicio=2020-01-01T00:00:00.000-03:00;fim=2021-12-31T00:00:00.000-03:00;cnpj=17189722000139;paginacao.paginaAtual=0;paginacao.itensPorPagina=200";

            Assert.That(str, Is.EqualTo(expected));
        }
    }
}
