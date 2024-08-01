using csv.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace csv.Controllers
{
    public class CsvManipulationController : Controller
    {
        public IActionResult Index(IFormFile arquivocsv)
        {
            if (arquivocsv is not null)
            {
                ViewBag.ArquivoCsvString = CsvLeitor(arquivocsv);
            }
            return View(ViewBag.arquivoCsvString);
        }

        public async Task<List<Produto>> CsvLeitor(IFormFile arquivoscsv)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            string path;
            path = $"wwwrot/Content/{arquivoscsv.Name}.csv";
            try
            {
                if (arquivoscsv is not null)
                {
                    using var stream = new MemoryStream();
                    await arquivoscsv.CopyToAsync(stream);
                    stream.Position = 0;
                    using var fileStream = new FileStream($"{path}", FileMode.OpenOrCreate);
                    await stream.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    fileStream.Close();


                    using (var reader = new StreamReader(path))
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Context.RegisterClassMap<ProdutoMap>();

                        return csv.GetRecords<Produto>().ToList();
                    }
                }

                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }

        public void CsvEscritor(string filepath, List<Produto> produtos)
        {
            filepath = "./Dowloads";
            var finalPath = Path.Combine(filepath, nameof(produtos) + ".csv");

            using (var writer = new StreamWriter(finalPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(produtos);
                }
            }
        }
    }
}
