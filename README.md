WordMorphBackend dizininde .env dosyası oluşturularak alınan API KEY'lerimizi ekliyoruz.
```bash
GOOGLE_API_KEY= abc
UNSPLASH_KEY= xyz
```
Gerekli Python kütüphanelerini yüklüyoruz.
```bash
cd WordMorphBackend
pip install -r .\requirements.txt
```
FastAPI sunucusunu aynı dizin içerisinde başlatıyoruz.
```bash
uvicorn main:app --reload
```
