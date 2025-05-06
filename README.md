# Wordmorph: Disleksi Dostu Metin Sadeleştirici ve Görselleştirici

**Wordmorph**, disleksi yaşayan bireylerin metinleri daha kolay okuyup anlayabilmesi için geliştirilmiş web tabanlı bir yardımcı uygulamadır. Modern bir kullanıcı arayüzü, sadeleştirme motoru, görsel destek ve sesli okuma özellikleriyle kullanıcıların okuma deneyimini iyileştirmeyi hedefler.

Bu proje bir hackathon/jam çalışması kapsamında geliştirilmiştir. Ana amaç, erişilebilirlik ve öğrenme kolaylığı sunmaktır.

### Proje Sahipleri
- Hilal Yılmaz
- Hümeyra Öztürk
- Berke Berkay Tekçe
- Furkan Erdoğan

## 🚀 Özellikler

### 🔡 Metin Sadeleştirme
- Kullanıcının girdiği metin, **Gemini** yapay zeka servisi kullanılarak daha basit ve anlaşılır hâle getirilir
- Karmaşık cümleler daha basit yapılara dönüştürülür
- Uzun paragraflar daha kısa ve öz hale getirilir

### 🧠 Disleksi Dostu Görselleştirme
- **OpenDyslexic font** ile özel tasarlanmış metin görünümü
- Renkli biçimlendirme ile dikkat çekici ve kolay okunabilir arayüz
- Yüksek kontrast ve okunabilirlik için optimize edilmiş tasarım

### 🔍 Kelimeye Göre Görsel Arama
- Seçilen kelime/ifade **MyMemory Çeviri API** ile İngilizce'ye çevrilir
- **Unsplash API** ile ilgili görseller otomatik olarak gösterilir
- Görsel destekli öğrenme imkanı

### 🔊 Metni Seslendirme
- Sadeleştirilmiş metin veya seçilen kelimeler **gTTS** ile seslendirilir
- Türkçe dil desteği
- Ayarlanabilir okuma hızı

## 🛠️ Teknolojiler

| Katman           | Teknoloji / Araçlar                                   |
|------------------|--------------------------------------------------------|
| Frontend         | ASP.NET Core (Razor Pages), HTML, CSS, JavaScript     |
| Backend          | Python (FastAPI), Gemini API, gTTS                    |
| Font & Stil      | OpenDyslexic Font, modern HTML/CSS                    |
| API Servisleri   | Unsplash API, MyMemory Çeviri API                     |

## 📦 Kurulum

### Gereksinimler

#### 1. .NET SDK
- .NET 8.0 Runtime kurulumu gerekli
- [.NET 8.0 Runtime İndir](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

#### 2. Python Ortamı
```bash
# Gerekli Python paketlerinin kurulumu
pip install fastapi uvicorn requests gtts python-dotenv
```

### Proje Kurulumu

1. Projeyi klonlayın:
```bash
git clone [proje-url]
cd Wordmorph_Jam
```

2. Backend için .env dosyası oluşturun:
```bash
cd WordMorphBackend
touch .env
```

3. .env dosyasına API anahtarlarınızı ekleyin:
```env
GOOGLE_API_KEY=your_gemini_api_key
UNSPLASH_KEY=your_unsplash_api_key
```

4. Backend sunucusunu başlatın:
```bash
uvicorn main:app --reload
```

5. Frontend uygulamasını başlatın:
```bash
dotnet run
```

## ▶️ Kullanım

1. Ana sayfaya girin
2. Dönüştürmek istediğiniz metni girin
3. "Dönüştür" butonuna tıklayın
4. Sadeleştirilmiş metni görüntüleyin
5. Kelimeleri seçerek görsel ve sesli destek alın
6. Yeni metin için "Geri Dön" butonunu kullanın

## 📁 Proje Yapısı

```
Wordmorph_Jam/
├── Pages/                    # Frontend sayfaları
│   ├── Index.cshtml         # Ana sayfa
│   └── Index.cshtml.cs      # Sayfa mantığı
├── wwwroot/                 # Statik dosyalar
│   ├── css/                # Stil dosyaları
│   └── js/                 # JavaScript dosyaları
└── WordMorphBackend/        # Backend servisi
    ├── main.py             # FastAPI ana dosyası
    ├── gemini_simplify.py  # Metin sadeleştirme
    ├── translate.py        # Çeviri işlemleri
    └── image_generator.py  # Görsel arama
```

## 🔒 Güvenlik ve Gizlilik

- API anahtarları `.env` dosyasında güvenli şekilde saklanır
- Kişisel veri toplanmaz
- Tüm işlemler kullanıcı tarafında gerçekleşir
- SSL/TLS şifreleme kullanılır

## 💬 Hedef Kullanıcı Kitlesi

- Disleksi yaşayan bireyler
- Eğitimciler ve özel eğitim uzmanları
- Görsel destekli öğrenmeyi tercih edenler
- Okuma güçlüğü yaşayan öğrenciler


## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.
