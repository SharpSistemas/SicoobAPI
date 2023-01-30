namespace CS.BCB.PIX.Models
{
    public class DadosDevedor : NomeCpfCnpj
    {
        public string email { get; set; }
        public string logradouro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
    }
}
