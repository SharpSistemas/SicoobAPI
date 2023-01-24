using Sicoob.PIX;
using System;
using System.Threading.Tasks;

namespace Sicoob.Testes;

public static class TestesApiPIX
{
    public static async Task Run(Shared.Models.ConfiguracaoAPI? cfg)
    {
        // Cria Objeto
        var sicoob = new SicoobPIX(cfg);
        // Configura acesso das APIs
        await sicoob.SetupAsync();
        // A autenticação dura 300 segundos (5min) e é auto renovado pela biblioteca
        // Cada chamada verifica e, se necessário, atualiza o token
        // Chame AtualizarCredenciaisAsync() para renovar manualmente
        // É possível consultar dados da vigência do Token nas propriedades:
        //  * sicoob.ExpiresIn
        //  * sicoob.ExpiresAtUTC
        //  * sicoob.Expired

        /* COB */

        var cobs = await sicoob.ListarCobrancasAsync(new Sicoob.PIX.Models.Cobranca.ConsultaRequest
        {
            inicio = DateTime.UtcNow.Date.AddDays(-1),
            fim = DateTime.UtcNow.AddDays(1).Date,
        });


        /* PIX */
        var pixPeriodo = await sicoob.ListarPIXAsync(new Sicoob.PIX.Models.Pix.ConsultaRequest()
        {
            inicio = DateTime.UtcNow.Date.AddDays(-1),
            fim = DateTime.UtcNow.AddDays(1).Date,
        });
        var ultimoPix = await sicoob.ConsultarPIXAsync(pixPeriodo.pix[^1].endToEndId);


    }
}
