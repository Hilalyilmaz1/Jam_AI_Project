using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Wordmorph_Jam.Services;
using System.Threading.Tasks;
using System.Text;

namespace Wordmorph_Jam.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GeminiService _geminiService;

        public IndexModel(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [BindProperty]
        public string UserInputText { get; set; }

        [BindProperty]
        public string ProcessedText { get; set; }

        public void OnGet()
        {
            // Ýlk sayfa yüklenirken yapýlacak þeyler varsa buraya
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserInputText))
            {
                ProcessedText = await _geminiService.SimplifyTextAsync(UserInputText);
                ProcessedText = RenklendirVeAyir(ProcessedText);
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

            // Kalan varsa ekle
            if (parca.Length > 0)
            {
                var renk = renkler[renkIndex % renkler.Length];
                builder.Append($"<span style='color:{renk}; font-weight:bold;'>{parca}</span>");
            }

            return builder.ToString();
        }

    }
}
