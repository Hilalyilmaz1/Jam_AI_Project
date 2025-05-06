namespace Wordmorph_Jam.Services
{
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["GeminiApiKey"]; // appsettings.json'dan alacağız
        }

        public async Task<string> SimplifyTextAsync(string input)
        {
            var endpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-pro-latest:generateContent?key=" + _apiKey;

            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = $"Lütfen aşağıdaki metni Türkçe'de daha sade ve basit bir şekilde ifade et:\n\n{input}" }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();

            try
            {
                using JsonDocument doc = JsonDocument.Parse(responseString);

                // Eğer hata varsa önce onu kontrol et
                if (doc.RootElement.TryGetProperty("error", out var error))
                {
                    var message = error.GetProperty("message").GetString();
                    return $"⚠️ API Hatası: {message}";
                }

                // Eğer candidates varsa devam et
                if (doc.RootElement.TryGetProperty("candidates", out var candidates))
                {
                    var result = candidates[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return result ?? "🤷‍♂️ Yanıt boş geldi.";
                }

                return "❌ Beklenen formatta yanıt alınamadı.";
            }
            catch (Exception ex)
            {
                return $"💥 JSON işleme hatası: {ex.Message}\nYanıt: {responseString}";
            }
        }

    }
}
