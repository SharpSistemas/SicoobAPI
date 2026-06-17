/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/

using Newtonsoft.Json;

namespace Sicoob.PIX.Models;

using System;

public class IniciarPagamentoRequest
{
    public string Chave { get; set; } = null!;
    public string? DataAgendamento { get; set; }
}

public class IniciarPagamentoResponse
{
    public string? EndToEndId { get; set; }
    public string? Chave { get; set; }
    public string? Tipo { get; set; }
    public ProprietarioChave? Proprietario { get; set; }
}

public class ProprietarioChave
{
    public string? Identificador { get; set; }
    public string? Nome { get; set; }
    public string? Tipo { get; set; }
}

public class ConfirmarPagamentoRequest
{
    public string? EndToEndId { get; set; }
    public string? Valor { get; set; }
    public string? Descricao { get; set; }
    public bool? Repeticao { get; set; }
    public string? MeioIniciacao { get; set; }
    public string? DataAgendamento { get; set; }
    public ParticipantePixPagamento? Origem { get; set; }
    public ParticipantePixPagamento? Destino { get; set; }
}

public class PixPagamento
{
    public string? Id { get; set; }
    public string? Estado { get; set; }
    public string? Valor { get; set; }
    public string? DetalheRejeicao { get; set; }
    public string? Descricao { get; set; }
    public DateTime? Horario { get; set; }
    public ParticipantePixPagamento? Origem { get; set; }
    public ParticipantePixPagamento? Destino { get; set; }
    public string? DataAgendamento { get; set; }
}

public class ParticipantePixPagamento
{
    public string? Ispb { get; set; }
    public string? CpfCnpj { get; set; }
    public string? Nome { get; set; }
    public string? Conta { get; set; }
    public string? Agencia { get; set; }
    public string? Tipo { get; set; }
    public string? ChaveDict { get; set; }
    public bool? BoolFavorecido { get; set; }
}

public class WebhookPagamentoRequest
{
    [JsonProperty("webhookUrl")]
    public string? WebhookUrl { get; set; }
}

public class WebhookPagamento
{
    public string? WebhookUrl { get; set; }
    public DateTime? Criacao { get; set; }
}
