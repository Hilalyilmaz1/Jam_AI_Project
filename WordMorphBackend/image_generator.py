import os
import requests
from dotenv import load_dotenv

load_dotenv()
UNSPLASH_KEY = os.getenv("UNSPLASH_KEY")

def translate_to_english(text):
    try:
        resp = requests.get(
            "https://api.mymemory.translated.net/get",
            params={"q": text, "langpair": "tr|en"}
        )
        data = resp.json()
        return data.get("responseData", {}).get("translatedText", text)
    except Exception as e:
        print("Çeviri hatası:", e)
        return text

def get_image_url(keyword: str) -> str:
    if not UNSPLASH_KEY:
        print("Unsplash API anahtarı bulunamadı!")
        return "https://via.placeholder.com/150?text=No+API+Key"
    try:
        english_keyword = translate_to_english(keyword)
        print(f"[Görsel Arama] Türkçe: '{keyword}' | İngilizce: '{english_keyword}'")
        response = requests.get(
            "https://api.unsplash.com/search/photos",
            params={"query": english_keyword, "per_page": 1},
            headers={"Authorization": f"Client-ID {UNSPLASH_KEY}"}
        )
        print(f"[Unsplash API URL] {response.url}")
        response.raise_for_status()
        data = response.json()
        if data["results"]:
            image_url = data["results"][0]["urls"]["small"]
            print(f"[Unsplash Sonuç] {image_url}")
            return image_url
        else:
            print("[Unsplash Sonuç] Görsel bulunamadı.")
            return "https://via.placeholder.com/150?text=No+Image"
    except Exception as e:
        print("Unsplash error:", e)
        return "https://via.placeholder.com/150?text=Error"    
