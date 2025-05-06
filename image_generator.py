import google.generativeai as genai
import os
from dotenv import load_dotenv
import requests


load_dotenv()
UNSPLASH_KEY = os.getenv("UNSPLASH_API_KEY")


# Görsel alma fonksiyonu (Unsplash API ile)
def get_image_url(keyword: str) -> str:
    try:
        response = requests.get(
            f"https://api.unsplash.com/search/photos",
            params={"query": keyword, "per_page": 1},
            headers={"Authorization": f"Client-ID {UNSPLASH_KEY}"}
        )
        response.raise_for_status()
        data = response.json()
        return data["results"][0]["urls"]["small"]
    except Exception as e:
        print("Unsplash error:", e)
        return "https://via.placeholder.com/150"    
