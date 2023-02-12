using System;
using System.Net;
using System.Collections.Generic;
using System.IO;

class SubdomainFinder
{
    static void Main(string[] args)
    {
        Console.WriteLine("________________________________________");
        Console.WriteLine("SubdomainFinder V1");
        Console.WriteLine("Created By Ali kareem ");
        Console.WriteLine("fb.com/Ali.KareemP");
        Console.WriteLine("________________________________________");

         
        Console.WriteLine("Enter the Main Dowmin example : site.com");
        //Read Main Domain
        string targetDomain = Console.ReadLine();

      

        List<string> subdomains = GetSubdomains(targetDomain);

        Console.WriteLine("Found subdomains:");
        foreach (string subdomain in subdomains)
        {
            Console.WriteLine(subdomain);
        }
    }

    static List<string> GetSubdomains(string targetDomain)
    {
        List<string> subdomains = new List<string>();

        // Create path if not exist
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TestFile/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // Check file if not exist
        var isExists = File.Exists(path + "wordlist.txt");
        var file = path + "wordlist.txt";
        if (!isExists)
        {
            Console.WriteLine("Welcome form Ammar Alasfer");
            // Download file if not exist
            Console.WriteLine("Download wordlist file");
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://s22.filetransfer.io/storage/download/nNDcTeYPraRM", path + "wordlist.txt");
            Console.WriteLine("Download completed in this path" + path + "Edit it if you want");
        }
        
        // Load the wordlist from a file
        string[] words = System.IO.File.ReadAllLines(file);

        // Check each word in the wordlist as a subdomain
        foreach (string word in words)
        {
            string url = word + "." + targetDomain;
            if (IsValidSubdomain(url))
            {
                subdomains.Add(url);
            }
        }

        return subdomains;
    }

    static bool IsValidSubdomain(string subdomain)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + subdomain);
            request.Timeout = 4000;
            request.Method = "HEAD";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch (WebException)
        {
            return false;
        }
    }
}
