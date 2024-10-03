namespace Sicoob.Cobranca;

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using Sicoob.Cobranca.Models.Shared;

public static class Helpers
{
    public static IEnumerable<MovimentacoesArquivo> ProcessarArquivoMovimentacao(string zipBase64)
    {
        var bytes = Convert.FromBase64String(zipBase64);
        return ProcessarArquivoMovimentacao(bytes);
    }
    public static IEnumerable<MovimentacoesArquivo> ProcessarArquivoMovimentacao(byte[] bytesZip)
    {
        Stream data = new MemoryStream(bytesZip);

        ZipArchive archive = new ZipArchive(data);
        foreach (ZipArchiveEntry entry in archive.Entries)
        {
            using var sr = new StreamReader(entry.Open());
            var registros = Newtonsoft.Json.JsonConvert.DeserializeObject<MovimentacoesArquivo[]>(sr.ReadToEnd());
            if (registros == null) continue;
            foreach (var r in registros) yield return r;
        }
    }
}
