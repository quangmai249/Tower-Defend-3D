using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;

public static class FileDownload
{
    public static string GetDownloadToken(WebClient webClient, string linkFirebase)
    {
        string res = string.Empty;
        Stream s = webClient.OpenRead(linkFirebase);
        StreamReader sr = new StreamReader(s);

        res = JsonConvert.DeserializeObject<FileMetaData>(sr.ReadToEnd()).DownloadTokens;

        s.Close();
        sr.Close();

        return res;
    }
    public static string GetAddress(WebClient webClient, string linkFirebase)
    {
        string res = string.Empty;
        Stream s = webClient.OpenRead(linkFirebase);
        StreamReader sr = new StreamReader(s);

        res = JsonConvert.DeserializeObject<FileMetaData>(sr.ReadToEnd()).Name;

        s.Close();
        sr.Close();

        return res;
    }
}
