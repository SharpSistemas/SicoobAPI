/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/

using System;
using Sicoob.Conta.Models.Shared;

namespace Sicoob.Conta.Models.v2;

public class ExtratoResponse
{
    public decimal Saldo { get; set; }
    public Transacao[] Transacoes { get; set; }

}

public class Transacao : TransacaoBase
{
}
