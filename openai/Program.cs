using System.Text;
using Newtonsoft.Json;

namespace OpenAIChatExample
{
    class Program
    {
        // Substitua com sua chave de API da OpenAI
        private static string apiKey = "Sua chave";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Passe os dados para a API da OpenAI e receba uma resposta:");
            var retorno = Console.ReadLine();
            
            
            // Cria o cliente HTTP
            using (var client = new HttpClient())
            {
                // Define os cabeçalhos
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                // Define a URL da API
                string url = "https://api.openai.com/v1/chat/completions";

                // Define o corpo da requisição com o modelo e as mensagens
                var requestBody = new
                {
                    model = "gpt-4o-mini", // O modelo escolhido
                    messages = new[]
                    {
                        new
                        {
                            role = "system", // A primeira mensagem define o papel do assistente
                            content = "Você é um assistente que tem dominio sobre o Código Penal Brasileiro Art. 1º, Art. 12, Art. 13, Art. 14, Art. 15, Art. 16, Art. 17, Art. 18, Art. 20 - Lei 7716/89, Art. 21, Art. 96 - Lei 10741/03, Art. 99 - Lei 10741/03," +
                                      " Art. 121, Art. 121, Art. 121, Art. 121, Art. 122, Art. 123, Art. 125, Art. 129, Art. 129, Art. 129, Art. 129, Art. 129, Art. 130, Art. 131, Art. 132, Art. 133, Art. 135, Art. 136, Art. 146, Art. 147, Art. 148, Art. 149," +
                                      " Art. 150, Art. 155, Art. 155, Art. 155, Art. 157, Art. 158, Art. 159, Art. 160, Art. 163, Art. 163, Art. 168, Art. 171, Art. 171, Art. 178, Art. 180, Art. 180, Art. 180, Art. 211, Art. 213, Art. 214, Art. 215, Art. 215-A," +
                                      " Art. 216-A, Art. 216-B, Art. 217-A, Art. 218, Art. 218-A, Art. 218-B, Art. 227, Art. 228, Art. 229, Art. 230, Art. 231, Art. 231, Art. 231-A, Art. 233, Art. 234, Art. 240, Art. 241, Art. 241-A, Art. 241-B, Art. 241-C," +
                                      " Art. 241-D, Art. 244, Art. 244-A, Art. 244-B, Art. 249, Art. 250, Art. 251, Art. 252, Art. 253, Art. 262, Art. 264, Art. 268, Art. 269, Art. 270, Art. 270, Art. 272, Art. 272, Art. 273, Art. 278, Art. 286, Art. 287," +
                                      " Art. 288 e art. 2º da Lei nº 12.850/2013, Art. 288-A, Art. 289, Art. 290, Art. 290, Art. 291, Art. 293, Art. 297, Art. 298, Art. 299, Art. 301, Art. 301, Art. 302, Art. 303, Art. 303, Art. 304, Art. 304, Art. 305," +
                                      " Art. 305, Art. 306, Art. 307, Art. 307, Art. 308, Art. 309, Art. 310, Art. 311, Art. 311, Art. 312, Art. 312, Art. 313, Art. 313-A, Art. 313-B, Art. 330, Art. 331, Art. 341, Art. 342, Art. 348, Art. 349, Art. 351," +
                                      " Art. 370, Art. 383, Art. 385, Art. 386, Art. 400, Art. 403, Art. 404, Art. 405, Art. 407, Art. 408."
                        },
                        new
                        {
                            role = "user", // A mensagem do usuário
                            content = "Esse processo se enquadra em algum dos artigos : (" + retorno + "). Fale apenas Verdadeiro ou Falso e coloque o número do artigo nesse formato {status: , numArtigo: }."
                        }
                    }
                };

                // Serializa o corpo da requisição para JSON
                var jsonContent = JsonConvert.SerializeObject(requestBody);

                // Envia a requisição POST para a API
                var response = await client.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                // Verifica se a requisição foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Lê e exibe a resposta
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    Console.WriteLine("Resposta da API: ");
                    Console.WriteLine(result.choices[0].message.content.ToString());
                }
                else
                {
                    Console.WriteLine($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Detalhes do erro: ");
                    Console.WriteLine(errorContent);
                }
            }
        }
    }
}
