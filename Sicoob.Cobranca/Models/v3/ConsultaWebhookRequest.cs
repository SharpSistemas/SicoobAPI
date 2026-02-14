namespace Sicoob.Cobranca.Models.v3;

using System;
using System.Diagnostics;
using Newtonsoft.Json;

public class ConsultaWebhookRequest
{
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public long? idWebhook { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public int? codigoTipoMovimento { get; set; }
}

public class ConsultaWebhookResponse
{
    public DadosWebhook[]? resultado { get; set; }
}

public class ConsultaSolicitacoesWebhookRequest
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataSolicitacao { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? pagina { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? codigoSolicitacaoSituacao { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? codigoBarras { get; set; } = null;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? nossoNumero { get; set; }
}

public class ConsultaSolicitacoesWebhookResponse
{
    public SolicitacoesWebhookResponse? resultado { get; set; }
}

public class IncluirWebhooksResponse
{
    public DadosWebhook? resultado { get; set; }
}

public class DadosWebhook
{
    public long idWebhook { get; set; }
    public string url { get; set; } = null;
    public string email { get; set; }
    public int codigoTipoMovimento { get; set; }
    public string descricaoTipoMovimento { get; set; }
    public int codigoPeriodoMovimento { get; set; }
    public string descricaoPeriodoMovimento { get; set; }
    public int codigoSituacao { get; set; }
    public string descricaoSituacao { get; set; }
    public DateTime dataHoraCadastro { get; set; }
    public DateTime dataHoraUltimaAlteracao { get; set; }
    public DateTime dataHoraInativacao { get; set; }
    public string descricaoMotivoInativacao { get; set; }
}

public class SolicitacoesWebhookResponse
{
    public int paginaAtual { get; set; }
    public int totalPaginas { get; set; }
    public int totalRegistros { get; set; }
    public DadosSolicitacoesWebhookResponse[] webhookSolicitacoes { get; set; }
}

public class DadosSolicitacoesWebhookResponse
{
    public int codigoWebhookSituacao { get; set; }
    public string descricaoWebhookSituacao { get; set; }
    public int codigoSolicitacaoSituacao { get; set; }
    public string descricaoSolicitacaoSituacao { get; set; }
    public int codigoTipoMovimento { get; set; }
    public string descricaoTipoMovimento { get; set; }
    public int codigoPeriodoMovimento { get; set; }
    public string descricaoPeriodoMovimento { get; set; }
    public string descricaoErroProcessamento { get; set; }
    public DateTime dataHoraCadastro { get; set; }
    public bool validacaoWebhook { get; set; }
    public int nossoNumero { get; set; }
    public string codigoBarras { get; set; }
    public DadosSolicitacoesWebhookNotificacoesResponse[] webhookNotificacoes { get; set; }
}

public class DadosSolicitacoesWebhookNotificacoesResponse
{
    public string url { get; set; }
    public DateTime dataHoraInicio { get; set; }
    public DateTime dataHoraFim { get; set; }
    public int tempoComunicao { get; set; }
    public int codigoStatusRequisicao { get; set; }
    public string descricaoCodigoStatusRequisicao { get; set; }
}