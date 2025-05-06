using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;

namespace Wordmorph_Jam.Pages
{
    public class IndexModel : PageModel
    { 

        [BindProperty]
        public string UserInputText { get; set; }
        public string ColoredSimplifiedText { get; set; }
        public string SimplifiedText { get; set; }
        public string ImageUrl { get; set; }
        public string AudioUrl { get; set; }      

        public void OnGet()
        {
            // Sayfa yenilendiğinde tüm değerleri sıfırla
            UserInputText = null;
            ColoredSimplifiedText = null;
            SimplifiedText = null;
            ImageUrl = null;
            AudioUrl = null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(UserInputText))
                return Page();

            using var client = new HttpClient();

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(new { text = UserInputText }),
                Encoding.UTF8, "application/json");

            // 1. Metni sadeleştir
            var simplifyResponse = await client.PostAsync("http://localhost:8000/simplify", jsonContent);
            if (!simplifyResponse.IsSuccessStatusCode)
                return Page();

            var simplifyResult = JsonConvert.DeserializeObject<dynamic>(await simplifyResponse.Content.ReadAsStringAsync());
            SimplifiedText = simplifyResult.simplified_text;

            // 2. Renklendir ve ayır
            ColoredSimplifiedText = RenklendirVeAyir(SimplifiedText);

            // 3. TTS - sadeleştirilmiş metni sesli oku
            var ttsJson = new StringContent(
                JsonConvert.SerializeObject(new { text = SimplifiedText }),
                Encoding.UTF8, "application/json");
            var ttsResponse = await client.PostAsync("http://localhost:8000/tts", ttsJson);
            if (ttsResponse.IsSuccessStatusCode)
            {
                var ttsResult = JsonConvert.DeserializeObject<dynamic>(await ttsResponse.Content.ReadAsStringAsync());
                AudioUrl = ttsResult.audio_url;
            }

            return Page();
        }

        public string RenklendirVeAyir(string input)
        {
            var sesliHarfler = new HashSet<char> { 'a', 'e', 'ı', 'i', 'o', 'ö', 'u', 'ü',
                                           'A', 'E', 'I', 'İ', 'O', 'Ö', 'U', 'Ü' };

            var renkler = new[] { 
                "#000000", // Siyah
                "#0000FF", // Mavi
                "#006400", // Koyu Yeşil
                "#8B0000", // Koyu Kırmızı
                "#4B0082", // İndigo
                "#800080"  // Mor
            };
            int renkIndex = 0;

            var builder = new StringBuilder();
            var parca = new StringBuilder();
            var kelime = new StringBuilder();

            foreach (char c in input)
            {
                parca.Append(c);
                kelime.Append(c);

                if (sesliHarfler.Contains(c))
                {
                    var renk = renkler[renkIndex % renkler.Length];
                    var kelimeStr = kelime.ToString().Trim();
                    
                    builder.Append($"<span class='colored-word' style='color:{renk}; font-weight:bold;' data-word='{kelimeStr}'>{parca}</span>");
                    parca.Clear();
                    kelime.Clear();
                    renkIndex++;
                }
            }

            if (parca.Length > 0)
            {
                var renk = renkler[renkIndex % renkler.Length];
                var kelimeStr = kelime.ToString().Trim();
                
                builder.Append($"<span class='colored-word' style='color:{renk}; font-weight:bold;' data-word='{kelimeStr}'>{parca}</span>");
            }

            return builder.ToString();
        }

    }
}
