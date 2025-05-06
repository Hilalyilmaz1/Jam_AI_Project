
# Wordmorph: Disleksi Dostu Metin Sadeleştirici ve Görselleştirici

**Wordmorph**, disleksi yaşayan bireylerin metinleri daha kolay okuyup anlayabilmesi için geliştirilmiş web tabanlı bir yardımcı uygulamadır. Modern bir kullanıcı arayüzü, sadeleştirme motoru, görsel destek ve sesli okuma özellikleriyle kullanıcıların okuma deneyimini iyileştirmeyi hedefler.

## 🚀 Özellikler

### 🔡 Metin Sadeleştirme
Kullanıcının girdiği metin, arka planda bir yapay zeka servisi olan **Gemini** kullanılarak daha basit ve anlaşılır hâle getirilir.

### 🧠 Disleksi Dostu Görselleştirme
Sadeleştirilmiş metin, **OpenDyslexic font** ile gösterilir. Renkli biçimlendirme ile dikkat çekici ve kolay okunabilir hale gelir.

### 🔍 Kelimeye Göre Görsel Arama
Kullanıcı sadeleştirilmiş metin kutusundan bir kelime ya da ifade seçtiğinde:
- Seçilen içerik **MyMemory Çeviri API** ile İngilizce'ye çevrilir.
- İngilizce içerik kullanılarak **Unsplash API** ile görsel aranır ve gösterilir.

### 🔊 Metni Seslendirme
Sadeleştirilmiş metin veya seçilen kelimeler bir **TTS (Text-to-Speech)** kütüphanesinden gtts modeli ile sesli olarak okunabilir.

## 🛠️ Teknolojiler

| Katman           | Teknoloji / Araçlar                                   |
|------------------|--------------------------------------------------------|
| Frontend         | ASP.NET Core (Razor Pages), HTML, CSS, JavaScript     |
| Backend          | Python (FastAPI), Gemini API, TTS Unslash             |
| Font & Stil      | OpenDyslexic Font, modern HTML/CSS                    |
| API Servisleri   | Unsplash API, MyMemory Çeviri API                     |

## 📦 Kurulum

### .NET Core ve Python ortamını kurun

#### 1. .NET SDK Gereksinimi
> Bu proje .NET 8.0 hedefliyor. Eğer sizde sadece .NET 9 yüklüyse, aşağıdaki bağlantıdan .NET 8.0 Runtime indirip kurmalısınız:

🔗 [.NET 8.0 Runtime İndir](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

#### 2. Python ortamı ve FastAPI kurulumu
```bash
      pip install fastapi uvicorn requests
```

#### 3. OpenDyslexic Font yükleme
Fontu [resmi sitesinden](https://opendyslexic.org/) indirip sisteminize kurmanız yeterlidir.

## ▶️ Kullanım Adımları

1. Ana sayfaya girin ve dönüştürmek istediğiniz metni yazın.
2. "**Dönüştür**" butonuna tıklayarak sadeleştirilmiş halini elde edin.
3. Sadeleştirilmiş metin, disleksi dostu yazı tipiyle ve renkli biçimlendirmeyle görüntülenir.
4. Sadeleştirilen metin kutusunda seçilen kelimenin:
   - İngilizce çevirisi yapılır.
   - Seçilen kelime için bir **görsel** gösterilir.
   - Aynı kelime sesli olarak **okunur**.
5. Yeni bir metin dönüştürmek için "**Geridön**" butonunu kullanın.

## 📁 Proje Yapısı

```
Wordmorph_Jam/
├── Pages/
│   ├── Index.cshtml
│   ├── Index.cshtml.cs
...
├── Properties/
│   └── launchSettings.json
├── Program.cs
├── wwwroot/
│   ├── css/
│   └── js/
...
├── fastapi_backend/
│   ├── main.py
│   ├── gemini_simplify.py
│   ├── translate.py
│   └── image_generator.py
```

## 🌐 API Kullanımı

### 🔹 Gemini API
Kullanıcı metni sadeleştirmek için kullanılır.

### 🔹 Unsplash API
Görsel arama API’sidir. Aramalarda kelimenin İngilizce karşılığı kullanılır.

## 💬 Hedef Kullanıcı Kitlesi

- Disleksi yaşayan bireyler
- Eğitimciler ve özel eğitim uzmanları
- Görselle desteklenmiş metinle öğrenmeyi tercih eden kullanıcılar

## 🔒 Güvenlik ve Gizlilik

- API anahtarları `.env` dosyasında saklanmalı ve kod deposunda paylaşılmamalıdır.
- Kişisel veriler toplanmaz; tüm işlemler kullanıcı tarafında ve geçici olarak yapılır.

## ✨ Katkıda Bulunma

Katkı sağlamak isterseniz `Issues` veya `Pull Requests` bölümlerini kullanabilirsiniz.

## 🧑‍💻 Geliştirici

Bu proje bir hackathon/jam çalışması kapsamında geliştirilmiştir. Ana amaç, erişilebilirlik ve öğrenme kolaylığı sunmaktır.

## Gerekli Düzenlemeler
WordMorphBackend dizininde .env dosyası oluşturularak alınan API KEY'lerimizi ekliyoruz.
```bash
      GOOGLE_API_KEY= abc
      UNSPLASH_KEY= xyz
```

FastAPI sunucusunu aynı dizin içerisinde başlatıyoruz.
```bash
      uvicorn main:app --reload
```
