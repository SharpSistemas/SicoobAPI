/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Sicoob.Testes;

System.Console.WriteLine("Escolha o Teste");

//await TestesApiPIX.Run();
//await TestesApiConta.Run_ContaCorrenteV2();
await TestesApiConta.Run_ContaCorrenteV4();
//await TestesApiConta.Run_ContaPoupanca();
//await TestesApiCobranca.Run_Cobranca();

