using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
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
            // Ýlk sayfa yüklenirken yapýlacak þeyler varsa buraya
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(UserInputText))
                return Page();

            using var client = new HttpClient();

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(new { text = UserInputText }),
                Encoding.UTF8, "application/json");

            // 1. Metni sadeleþtir
            var simplifyResponse = await client.PostAsync("http://localhost:8000/simplify", jsonContent);
            if (!simplifyResponse.IsSuccessStatusCode)
                return Page();

            var simplifyResult = JsonConvert.DeserializeObject<dynamic>(await simplifyResponse.Content.ReadAsStringAsync());
            SimplifiedText = simplifyResult.simplified_text;

            // 2. Renklendir ve ayýr
            ColoredSimplifiedText = RenklendirVeAyir(SimplifiedText);

            // 3. Görseli sadeleþtirilmiþ metne göre al
            var simplifiedJson = new StringContent(
                JsonConvert.SerializeObject(new { text = SimplifiedText }),
                Encoding.UTF8, "application/json");

            var imageResponse = await client.PostAsync("http://localhost:8000/generate-image", simplifiedJson);
            if (imageResponse.IsSuccessStatusCode)
            {
                var imageResult = JsonConvert.DeserializeObject<dynamic>(await imageResponse.Content.ReadAsStringAsync());
                ImageUrl = imageResult.image_url;
            }

            // 4. TTS - sadeleþtirilmiþ metni sesli oku
            var ttsResponse = await client.PostAsync("http://localhost:8000/tts", simplifiedJson);
            if (ttsResponse.IsSuccessStatusCode)
            {
                var ttsResult = JsonConvert.DeserializeObject<dynamic>(await ttsResponse.Content.ReadAsStringAsync());
                AudioUrl = ttsResult.audio_url;
            }

            return Page();
        }

        public string RenklendirVeAyir(string input)
        {
            var sesliHarfler = new HashSet<char> { 'a', 'e', 'ý', 'i', 'o', 'ö', 'u', 'ü',
                                           'A', 'E', 'I', 'Ý', 'O', 'Ö', 'U', 'Ü' };

            var renkler = new[] { "#FF6B6B", "#6BCB77", "#4D96FF", "#FFD93D", "#845EC2", "#00C9A7" };
            int renkIndex = 0;

            var builder = new StringBuilder();
            var parca = new StringBuilder();

            foreach (char c in input)
            {
                parca.Append(c);
                if (sesliHarfler.Contains(c))
                {
                    var renk = renkler[renkIndex % renkler.Length];
                    builder.Append($"<span style='color:{renk}; font-weight:bold;'>{parca}</span> ");
                    parca.Clear();
                    renkIndex++;
                }
            }

            if (parca.Length > 0)
            {
                var renk = renkler[renkIndex % renkler.Length];
                builder.Append($"<span style='color:{renk}; font-weight:bold;'>{parca}</span>");
            }

            return builder.ToString();
        }

    }
}
