using System;
using System.IO;
using Newtonsoft.Json;

namespace CurrencyConverter.Infrastructure
{
    public class FileParser<T>
    {
        public T Parse(string path, Type locator)
        {
            try
            {
                var stream = locator.Assembly.GetManifestResourceStream(locator, path);

                // ReSharper disable once AssignNullToNotNullAttribute
                using var sr   = new StreamReader(stream);
                var       json = sr.ReadToEnd();


                return JsonConvert.DeserializeObject<T>(json);
            }
            catch(Exception)
            {
                Console.WriteLine($"Unable to parse {path}!");
                return default;
            }
        }
    }
}
