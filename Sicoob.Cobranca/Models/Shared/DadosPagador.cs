namespace Sicoob.Cobranca.Models.Shared;

public class DadosPagador
{
    public string numeroCpfCnpj { get; set; }
    public string nome { get; set; }
    public string endereco { get; set; }
    public string bairro { get; set; }
    public string cidade { get; set; }
    public string cep { get; set; }
    public string uf { get; set; }
    public string email { get; set; }
}

public class DadosPagadorRequest : DadosPagador
{
    public long numeroCliente { get; set; }
}