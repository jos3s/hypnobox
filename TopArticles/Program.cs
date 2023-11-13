using System.Text.Json;
using System.Text.Json.Serialization;
using TopArticles.Entites;

namespace TopArticles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            try
            {
                int numberOfArticles = getNumberOfArticles();

                int apiPage = 1;

                (List<Article> articlesWithTitles, bool outOfApiRange, int numberOfArticlesRead) = GetValidArticles(numberOfArticles, apiPage);

                IEnumerable<Article> articlesOrder = articlesWithTitles.OrderByDescending(x => x.NumComments).ThenBy(x => x.Title);

                var displayArticles = articlesOrder.Take(numberOfArticles).ToList();
                DisplayArticles(displayArticles);
                DisplayComplementaryMensage(numberOfArticles, displayArticles.Count, outOfApiRange, numberOfArticlesRead);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.WriteLine("Digite qualquer tecla para encerrar o programa."); }
        }

        private static int getNumberOfArticles()
        {
            Console.Write("Digite o número limite de artigos:");
            var inputLines = Console.ReadLine();

            try
            {
                return int.Parse(inputLines);
            }
            catch (Exception)
            {
                throw new Exception("O valor inserido não é um número válido");
            }
        }

        public static async Task<ArticlesPage> GetArticlesPageAsync(int page)
        {
            ArticlesPage articlesPage = new ArticlesPage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://hypnocore.api.hypnobox.com.br/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"teste/api/articles?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    articlesPage = JsonSerializer.Deserialize<ArticlesPage>(responseContent);
                }
            }
            return articlesPage;
        }

        private static (List<Article>, bool, int) GetValidArticles(int lines, int page)
        {
            List<Article> articlesWithTitles = new();
            bool outOfApiRange = false;
            int numberOfArticlesRead = 0;
            while (!outOfApiRange)
            {
                List<Article> articles = GetArticlesPageAsync(page).Result.Data;

                if (articles == null)
                {
                    outOfApiRange = true;
                    break;
                }

                foreach (var article in articles)
                {
                    numberOfArticlesRead++;

                    if (article.Title == null && article.StoryTitle == null)
                        continue;

                    if (article.Title != null)
                        articlesWithTitles.Add(article);

                    if (article.StoryTitle != null)
                    {
                        article.Title = article.StoryTitle;
                        articlesWithTitles.Add(article);
                    }
                }
                page++;
            }

            return (articlesWithTitles, outOfApiRange, numberOfArticlesRead);
        }

        private static void DisplayComplementaryMensage(int lines, int numberArticlesWithTitles, bool outOfApiRange, int numberOfArticlesRead)
        {
            Console.WriteLine("");

            if (numberArticlesWithTitles < lines)
            {
                if (outOfApiRange)
                    Console.WriteLine($"O número de artigos específicados está fora do range da API.(API contém {numberOfArticlesRead} artigos.)");
                Console.WriteLine($"Apenas {numberArticlesWithTitles} de {lines} artigos experados satisfazem as regras estabelecidas.");
            }
            Console.WriteLine($"{numberArticlesWithTitles} de {lines} artigos exibidos.");
        }

        private static void DisplayArticles(List<Article> articlesOrder)
        {
            for (int i = 0; i < articlesOrder.Count(); i++)
            {
                Console.WriteLine($"Artigo N°{i + 1}: '{articlesOrder[i].Title}' tem {articlesOrder[i].NumComments} comentários");
            }
        }
    }
}