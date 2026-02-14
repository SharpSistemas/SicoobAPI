/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Shared.Models;

using System.Collections.Generic;

public class AuthorizationScope
{
    /* API PIX */
    /// <summary>
    /// [API PIX] cob.write: Permissão para alteração de cobranças imediatas
    /// </summary>
    public bool COB_WRITE { get; set; }
    /// <summary>
    /// [API PIX] cob.read: Permissão para consulta de cobranças imediatas
    /// </summary>
    public bool COB_READ { get; set; }
    /// <summary>
    /// [API PIX] cobv.write: Permissão para alteração de cobranças com vencimento
    /// </summary>
    public bool COBV_WRITE { get; set; }
    /// <summary>
    /// [API PIX] cobv.read: Permissão para consulta de cobranças com vencimento
    /// </summary>
    public bool COBV_READ { get; set; }
    /// <summary>
    /// [API PIX] lotecobv.write: Permissão para alteração de lotes de cobranças com vencimento
    /// </summary>
    public bool LOTE_COBV_WRITE { get; set; }
    /// <summary>
    /// [API PIX] lotecobv.read: Permissão para consulta de lotes de cobranças com vencimento
    /// </summary>
    public bool LOTE_COBV_READ { get; set; }

    /// <summary>
    /// [API PIX] pix.write: Permissão para alteração de Pix
    /// </summary>
    public bool PIX_WRITE { get; set; }
    /// <summary>
    /// [API PIX] pix.read: Permissão para consulta de Pix
    /// </summary>
    public bool PIX_READ { get; set; }

    /// <summary>
    /// [API PIX] webhook.read: Permissão para alteração do webhook
    /// </summary>
    public bool WEBHOOK_WRITE { get; set; }
    /// <summary>
    /// [API PIX] webhook.write: Permissão para consulta do webhook
    /// </summary>
    public bool WEBHOOK_READ { get; set; }

    /// <summary>
    /// [API PIX] payloadlocation.write: Permissão para alteração de payloads
    /// </summary>
    public bool PAYLOAD_LOCATION_WRITE { get; set; }
    /// <summary>
    /// [API PIX] payloadlocation.read: Permissão para consulta de payloads
    /// </summary>
    public bool PAYLOAD_LOCATION_READ { get; set; }

    /* API Conta Corrente */
    /// <summary>
    /// [API Conta Corrente] openid: Escopo de acesso para Logon para Conta Corrente
    /// </summary>
    public bool OPENID { get; set; }
    /// <summary>
    /// [API Conta Corrente] cco_extrato: Acessa dados do Extrato
    /// </summary>
    public bool CCO_EXTRATO { get; set; }
    /// <summary>
    /// [API Conta Corrente] cco_saldo: Acessa dados do Saldo
    /// </summary>
    public bool CCO_SALDO { get; set; }

    /* API Poupança */
    /// <summary>
    /// [API Conta Poupança] poupanca_contas: Acessa dados das Contas
    /// </summary>
    public bool POUPANCA_CONTAS { get; set; }
    /// <summary>
    /// [API Conta Poupança] poupanca_extrato: Acessa dados do Extrato
    /// </summary>
    public bool POUPANCA_EXTRATO { get; set; }
    /// <summary>
    /// [API Conta Poupança] poupanca_saldo: Acessa dados do Saldo
    /// </summary>
    public bool POUPANCA_SALDO { get; set; }

    /* API Cobrança (v2) */
    /// <summary>
    /// [API Cobrança] cobranca_boletos_consultar: Permissão de consulta de boletos
    /// </summary>
    public bool COBRANCA_BOLETOS_CONSULTAR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_incluir: Permissão de inclusão de boletos
    /// </summary>
    public bool COBRANCA_BOLETOS_INCLUIR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_pagador: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_PAGADOR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_segunda_via: Permissão emissão de segunda via
    /// </summary>
    public bool COBRANCA_BOLETOS_SEGUNDA_VIA { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_descontos: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_DESCONTOS { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_abatimentos: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_ABATIMENTOS { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_valor_nominal: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_VALOR_NOMINAL { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_seu_numero: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_SEU_NUMERO { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_especie_documento: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_ESPECIE_DOCUMENTO { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_baixa: Permissão para baixa de boletos
    /// </summary>
    public bool COBRANCA_BOLETOS_BAIXA { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_rateio_credito: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_RATEIO_CREDITO { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_pagadores: Permissão de Gerência de Pagadores
    /// </summary>
    public bool COBRANCA_PAGADORES { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_negativacoes_incluir: Permissão para inclusão de negativação
    /// </summary>
    public bool COBRANCA_BOLETOS_NEGATIVACOES_INCLUIR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_negativacoes_alterar: Permissão para alteração de negativação
    /// </summary>
    public bool COBRANCA_BOLETOS_NEGATIVACOES_ALTERAR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_negativacoes_baixar: Permissão para baixar de negativações
    /// </summary>
    public bool COBRANCA_BOLETOS_NEGATIVACOES_BAIXAR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_protestos_incluir: Permissão para incluir protesto
    /// </summary>
    public bool COBRANCA_BOLETOS_PROTESTOS_INCLUIR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_protestos_alterar: Permissão para alterar protesto
    /// </summary>
    public bool COBRANCA_BOLETOS_PROTESTOS_ALTERAR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_protestos_desistir: Permissão para remover/desistir protesto
    /// </summary>
    public bool COBRANCA_BOLETOS_PROTESTOS_DESISTIR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_solicitacao_movimentacao_incluir: Permissão para incluir movimentação
    /// </summary>
    public bool COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_INCLUIR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_solicitacao_movimentacao_consultar: Permissão para consultar movimentação
    /// </summary>
    public bool COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_CONSULTAR { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_solicitacao_movimentacao_download: Permissão para fazer download de movimentações
    /// </summary>
    public bool COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_DOWNLOAD { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_prorrogacoes_data_vencimento: Permissão para prorrogar vencimentos
    /// </summary>
    public bool COBRANCA_BOLETOS_PRORROGACOES_DATA_VENCIMENTO { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_prorrogacoes_data_limite_pagamento: Permissão prorrogação de data limite
    /// </summary>
    public bool COBRANCA_BOLETOS_PRORROGACOES_DATA_LIMITE_PAGAMENTO { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_encargos_multas: Permissão alteração de encargos e multa
    /// </summary>
    public bool COBRANCA_BOLETOS_ENCARGOS_MULTAS { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_encargos_juros_mora: Permissão alteração de juros
    /// </summary>
    public bool COBRANCA_BOLETOS_ENCARGOS_JUROS_MORA { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_pix: Permissão para alterar Boleto+PIX
    /// </summary>
    public bool COBRANCA_BOLETOS_PIX { get; set; }
    /// <summary>
    /// [API Cobrança] cobranca_boletos_faixa_nn_disponiveis: Permissão ??
    /// </summary>
    public bool COBRANCA_BOLETOS_FAIXA_NN_DISPONIVEIS { get; set; }

    /* API Cobrança Bancária (v3) */
    /// <summary>
    /// [API Cobrança Bancária V3] boletos_consulta: Permissão de consulta de boletos
    /// </summary>
    public bool COBRANCA_V3_BOLETOS_CONSULTA { get; set; }
    /// <summary>
    /// [API Cobrança Bancária V3] boletos_inclusao: Permissão de inclusão de boletos
    /// </summary>
    public bool COBRANCA_V3_BOLETOS_INCLUSAO { get; set; }
    /// <summary>
    /// [API Cobrança Bancária V3] boletos_alteracao: Permissão de alteração de boletos
    /// </summary>
    public bool COBRANCA_V3_BOLETOS_ALTERACAO { get; set; }
    /// <summary>
    /// [API Cobrança Bancária V3] webhooks_consulta: Permissão de consulta de webhooks
    /// </summary>
    public bool COBRANCA_V3_WEBHOOKS_CONSULTA { get; set; }
    /// <summary>
    /// [API Cobrança Bancária V3] boletos_inclusao: Permissão de inclusão de webhooks
    /// </summary>
    public bool COBRANCA_V3_WEBHOOKS_INCLUSAO { get; set; }
    /// <summary>
    /// [API Cobrança Bancária V3] webhooks_alteracao: Permissão de alteração de webhooks
    /// </summary>
    public bool COBRANCA_V3_WEBHOOKS_ALTERACAO { get; set; }

    /// <summary>
    /// [API Convênios Pagamentos] convenios_consulta: Permissão de consulta de Api de Convênio de Pagamentos
    /// </summary>
    public bool CONVENIO_PAGAMENTOS_CONSULTA { get; set; }
    /// <summary>
    /// [API Convênios Pagamentos] convenios_consulta: Permissão de escrita de Api de Convênio de Pagamentos
    /// </summary>
    public bool CONVENIO_PAGAMENTOS_ESCRITA { get; set; }

    /// <summary>
    /// Gera a lista de Scope utilizado na geração do Token
    /// </summary>
    public string[] ToScope()
    {
        List<string> lst = new List<string>();

        /* API PIX */
        if (COB_WRITE) lst.Add("cob.write");
        if (COB_READ) lst.Add("cob.read");

        if (COBV_WRITE) lst.Add("cobv.write");
        if (COBV_READ) lst.Add("cobv.read");

        if (LOTE_COBV_WRITE) lst.Add("lotecobv.write");
        if (LOTE_COBV_READ) lst.Add("lotecobv.read");

        if (PIX_WRITE) lst.Add("pix.write");
        if (PIX_READ) lst.Add("pix.read");

        if (WEBHOOK_WRITE) lst.Add("webhook.write");
        if (WEBHOOK_READ) lst.Add("webhook.read");

        if (PAYLOAD_LOCATION_WRITE) lst.Add("payloadlocation.write");
        if (PAYLOAD_LOCATION_READ) lst.Add("payloadlocation.read");

        /* API Conta Corrente */
        if (OPENID) lst.Add("openid");
        if (CCO_EXTRATO) lst.Add("cco_extrato");
        if (CCO_SALDO) lst.Add("cco_saldo");

        /* API Poupança */
        if (POUPANCA_CONTAS) lst.Add("poupanca_contas");
        if (POUPANCA_EXTRATO) lst.Add("poupanca_extrato");
        if (POUPANCA_SALDO) lst.Add("poupanca_saldo");

        /* API Cobrança */
        if (COBRANCA_BOLETOS_CONSULTAR) lst.Add("cobranca_boletos_consultar");
        if (COBRANCA_BOLETOS_INCLUIR) lst.Add("cobranca_boletos_incluir");
        if (COBRANCA_BOLETOS_PAGADOR) lst.Add("cobranca_boletos_pagador");
        if (COBRANCA_BOLETOS_SEGUNDA_VIA) lst.Add("cobranca_boletos_segunda_via");
        if (COBRANCA_BOLETOS_DESCONTOS) lst.Add("cobranca_boletos_descontos");
        if (COBRANCA_BOLETOS_ABATIMENTOS) lst.Add("cobranca_boletos_abatimentos");
        if (COBRANCA_BOLETOS_VALOR_NOMINAL) lst.Add("cobranca_boletos_valor_nominal");
        if (COBRANCA_BOLETOS_SEU_NUMERO) lst.Add("cobranca_boletos_seu_numero");
        if (COBRANCA_BOLETOS_ESPECIE_DOCUMENTO) lst.Add("cobranca_boletos_especie_documento");
        if (COBRANCA_BOLETOS_BAIXA) lst.Add("cobranca_boletos_baixa");
        if (COBRANCA_BOLETOS_RATEIO_CREDITO) lst.Add("cobranca_boletos_rateio_credito");
        if (COBRANCA_PAGADORES) lst.Add("cobranca_pagadores");
        if (COBRANCA_BOLETOS_NEGATIVACOES_INCLUIR) lst.Add("cobranca_boletos_negativacoes_incluir");
        if (COBRANCA_BOLETOS_NEGATIVACOES_ALTERAR) lst.Add("cobranca_boletos_negativacoes_alterar");
        if (COBRANCA_BOLETOS_NEGATIVACOES_BAIXAR) lst.Add("cobranca_boletos_negativacoes_baixar");
        if (COBRANCA_BOLETOS_PROTESTOS_INCLUIR) lst.Add("cobranca_boletos_protestos_incluir");
        if (COBRANCA_BOLETOS_PROTESTOS_ALTERAR) lst.Add("cobranca_boletos_protestos_alterar");
        if (COBRANCA_BOLETOS_PROTESTOS_DESISTIR) lst.Add("cobranca_boletos_protestos_desistir");
        if (COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_INCLUIR) lst.Add("cobranca_boletos_solicitacao_movimentacao_incluir");
        if (COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_CONSULTAR) lst.Add("cobranca_boletos_solicitacao_movimentacao_consultar");
        if (COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_DOWNLOAD) lst.Add("cobranca_boletos_solicitacao_movimentacao_download");
        if (COBRANCA_BOLETOS_PRORROGACOES_DATA_VENCIMENTO) lst.Add("cobranca_boletos_prorrogacoes_data_vencimento");
        if (COBRANCA_BOLETOS_PRORROGACOES_DATA_LIMITE_PAGAMENTO) lst.Add("cobranca_boletos_prorrogacoes_data_limite_pagamento");
        if (COBRANCA_BOLETOS_ENCARGOS_MULTAS) lst.Add("cobranca_boletos_encargos_multas");
        if (COBRANCA_BOLETOS_ENCARGOS_JUROS_MORA) lst.Add("cobranca_boletos_encargos_juros_mora");
        if (COBRANCA_BOLETOS_PIX) lst.Add("cobranca_boletos_pix");
        if (COBRANCA_BOLETOS_FAIXA_NN_DISPONIVEIS) lst.Add("cobranca_boletos_faixa_nn_disponiveis");

        /* API Cobrança V3*/
        if (COBRANCA_V3_BOLETOS_CONSULTA) lst.Add("boletos_consulta");
        if (COBRANCA_V3_BOLETOS_INCLUSAO) lst.Add("boletos_inclusao");
        if (COBRANCA_V3_BOLETOS_ALTERACAO) lst.Add("boletos_alteracao");
        if (COBRANCA_V3_WEBHOOKS_CONSULTA) lst.Add("webhooks_consulta");
        if (COBRANCA_V3_WEBHOOKS_INCLUSAO) lst.Add("webhooks_inclusao");
        if (COBRANCA_V3_WEBHOOKS_ALTERACAO) lst.Add("webhooks_alteracao");

        /* API Convênios Pagamentos */
        if (CONVENIO_PAGAMENTOS_CONSULTA) lst.Add("convenios_consulta");
        if (CONVENIO_PAGAMENTOS_ESCRITA) lst.Add("convenios_escrita");

        return lst.ToArray();
    }
    /// <summary>
    /// Gera string utilizada na geração do Token
    /// </summary>
    /// <returns></returns>
    public string ToScopeString()
    {
        return string.Join(" ", ToScope());
    }

    public AuthorizationScope PIX_SetarPIX(bool valor)
    {
        PIX_READ = valor;
        PIX_WRITE = valor;
        return this;
    }
    public AuthorizationScope PIX_SetarCOB(bool valor)
    {
        COB_READ = valor;
        COB_WRITE = valor;
        return this;
    }
    public AuthorizationScope PIX_SetarCOBV(bool valor)
    {
        COBV_READ = valor;
        COBV_WRITE = valor;
        return this;
    }
    public AuthorizationScope PIX_SetarLote(bool valor)
    {
        LOTE_COBV_READ = valor;
        LOTE_COBV_WRITE = valor;
        return this;
    }
    public AuthorizationScope PIX_SetarWebhook(bool valor)
    {
        WEBHOOK_READ = valor;
        WEBHOOK_WRITE = valor;
        return this;
    }
    public AuthorizationScope PIX_SetarPayload(bool valor)
    {
        PAYLOAD_LOCATION_READ = valor;
        PAYLOAD_LOCATION_WRITE = valor;
        return this;
    }

    public AuthorizationScope PIX_Setar_Write(bool valor)
    {
        COB_WRITE = valor;
        COBV_WRITE = valor;
        LOTE_COBV_WRITE = valor;

        PIX_WRITE = valor;
        WEBHOOK_WRITE = valor;
        PAYLOAD_LOCATION_WRITE = valor;
        return this;
    }
    public AuthorizationScope PIX_Setar_Read(bool valor)
    {
        COB_READ = valor;
        COBV_READ = valor;
        LOTE_COBV_READ = valor;
        PIX_READ = valor;
        WEBHOOK_READ = valor;
        PAYLOAD_LOCATION_READ = valor;
        return this;
    }

    public AuthorizationScope CCorrente_Setar(bool valor)
    {
        OPENID = valor;
        CCO_EXTRATO = valor;
        CCO_SALDO = valor;
        return this;
    }
    public AuthorizationScope CPoupanca_Setar(bool valor)
    {
        POUPANCA_CONTAS = valor;
        POUPANCA_EXTRATO = valor;
        POUPANCA_SALDO = valor;
        return this;
    }

    public AuthorizationScope Cobranca_Setar(bool value)
    {
        COBRANCA_BOLETOS_CONSULTAR = value;
        COBRANCA_BOLETOS_INCLUIR = value;
        COBRANCA_BOLETOS_PAGADOR = value;
        COBRANCA_BOLETOS_SEGUNDA_VIA = value;
        COBRANCA_BOLETOS_DESCONTOS = value;
        COBRANCA_BOLETOS_ABATIMENTOS = value;
        COBRANCA_BOLETOS_VALOR_NOMINAL = value;
        COBRANCA_BOLETOS_SEU_NUMERO = value;
        COBRANCA_BOLETOS_ESPECIE_DOCUMENTO = value;
        COBRANCA_BOLETOS_BAIXA = value;
        COBRANCA_BOLETOS_RATEIO_CREDITO = value;
        COBRANCA_PAGADORES = value;
        COBRANCA_BOLETOS_NEGATIVACOES_INCLUIR = value;
        COBRANCA_BOLETOS_NEGATIVACOES_ALTERAR = value;
        COBRANCA_BOLETOS_NEGATIVACOES_BAIXAR = value;
        COBRANCA_BOLETOS_PROTESTOS_INCLUIR = value;
        COBRANCA_BOLETOS_PROTESTOS_ALTERAR = value;
        COBRANCA_BOLETOS_PROTESTOS_DESISTIR = value;
        COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_INCLUIR = value;
        COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_CONSULTAR = value;
        COBRANCA_BOLETOS_SOLICITACAO_MOVIMENTACAO_DOWNLOAD = value;
        COBRANCA_BOLETOS_PRORROGACOES_DATA_VENCIMENTO = value;
        COBRANCA_BOLETOS_PRORROGACOES_DATA_LIMITE_PAGAMENTO = value;
        COBRANCA_BOLETOS_ENCARGOS_MULTAS = value;
        COBRANCA_BOLETOS_ENCARGOS_JUROS_MORA = value;
        COBRANCA_BOLETOS_PIX = value;
        COBRANCA_BOLETOS_FAIXA_NN_DISPONIVEIS = value;
        return this;
    }

    public AuthorizationScope CobrancaV3_Setar(bool value)
    {
        COBRANCA_V3_BOLETOS_CONSULTA = value;
        COBRANCA_V3_BOLETOS_INCLUSAO = value;
        COBRANCA_V3_BOLETOS_ALTERACAO = value;
        COBRANCA_V3_WEBHOOKS_CONSULTA = value;
        COBRANCA_V3_WEBHOOKS_INCLUSAO = value;
        COBRANCA_V3_WEBHOOKS_ALTERACAO = value;
        return this;
    }

    public AuthorizationScope Convenio_Setar(bool value)
    {
        CONVENIO_PAGAMENTOS_CONSULTA = value;
        CONVENIO_PAGAMENTOS_ESCRITA = value;
        return this;
    }

    public AuthorizationScope RemoverTodos()
    {
        return setarTodosComo(false);
    }

    /// <summary>
    /// Seta todos independente de novas propriedades serem criadas
    /// </summary>
    private AuthorizationScope setarTodosComo(bool valor)
    {
        var t = typeof(AuthorizationScope);
        foreach (var p in t.GetProperties())
        {
            if (!p.CanRead) continue;
            if (!p.CanWrite) continue;
            if (p.PropertyType != typeof(bool)) continue;

            p.SetValue(this, valor);
        }
        return this;
    }

    public static AuthorizationScope PIX_SomenteLeitura()
    {
        return new AuthorizationScope().PIX_Setar_Read(true);
    }
    public static AuthorizationScope TodosPIX()
    {
        return new AuthorizationScope().PIX_Setar_Read(true).PIX_Setar_Write(true);
    }
    public static AuthorizationScope TodosContaCorrente()
    {
        return new AuthorizationScope().CCorrente_Setar(true);
    }
    public static AuthorizationScope TodosContaPoupanca()
    {
        return new AuthorizationScope().CPoupanca_Setar(true);
    }
    public static AuthorizationScope TodosCobranca(bool v3 = false)
    {
        if (v3)
            return new AuthorizationScope().CobrancaV3_Setar(true);
        return new AuthorizationScope().Cobranca_Setar(true);
    }
    
    public static AuthorizationScope TodosConvenioCobranca()
    {
        return new AuthorizationScope().Convenio_Setar(true);
    }
}
