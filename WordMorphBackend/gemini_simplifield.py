import google.generativeai as genai
from dotenv import load_dotenv
import os
import requests

load_dotenv()

GOOGLE_API_KEY = os.getenv("GOOGLE_API_KEY")

genai.configure(api_key=GOOGLE_API_KEY)

model = genai.GenerativeModel("gemini-1.5-pro")

def simplify_text_with_gemini(text: str) -> str:
    prompt = f"""
    Simplify the following Turkish text to make it easier for students with dyslexia to understand.
    Use plain language, short sentences, and avoid complex vocabulary.
    Text: {text}
    """
    
    try:
        response = model.generate_content(prompt)
        return response.text
    except Exception as e:
        print("Gemini Error:", e)
        return "An error occurred during text simplification."
    
