﻿/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class Cobranca
    {
        public enum ListaStatus
        {
            ATIVA,
            CONCLUIDA,
            REMOVIDA_PELO_USUARIO_RECEBEDOR,
            REMOVIDA_PELO_PSP,

            DESCONHECIDO,
        }

        public Calendario calendario { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public DadosLoc loc { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }
        public string txid { get; set; }
        public int revisao { get; set; }
        public string location { get; set; }
        /// <summary>
        /// ATIVA, CONCLUIDA, REMOVIDA_PELO_USUARIO_RECEBEDOR, REMOVIDA_PELO_PSP
        /// </summary>
        public string status { get; set; }
        public string brcode { get; set; }
        public PixRecebido[] pix { get; set; }

        public ListaStatus ObterStatus()
        {
            if (!Enum.TryParse(status, out ListaStatus result))
            {
                result = ListaStatus.DESCONHECIDO;
            }

            return result;
        }
    }
}