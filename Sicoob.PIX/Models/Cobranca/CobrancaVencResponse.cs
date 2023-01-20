using Sicoob.Shared.Models.Geral;
using System;

namespace Sicoob.PIX.Models.Cobranca
{
    internal class CobrancaVencResponse
    {
        public ResponseParametros parametros { get; set; }
        public Cob[] cobs { get; set; }
    }


    public class Cob
    {
        public string _ref { get; set; }
    }

}
