from fastapi import FastAPI, Request
from pydantic import BaseModel
from schemas import TextInput
from gemini_simplifield import simplify_text_with_gemini
from image_generator import get_image_url
from tts_synthesizer import synthesize_text
from fastapi.staticfiles import StaticFiles
from fastapi.middleware.cors import CORSMiddleware


app = FastAPI()
app.mount("/static", StaticFiles(directory="static"), name="static")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Dilersen sadece http://localhost:xxxx şeklinde kısıtlayabilirsin
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"]
)

class TextInput(BaseModel):
    text: str

#metin sadeleştirme
@app.post("/simplify")
async def simplify(input: TextInput):
    simplified_text = simplify_text_with_gemini(input.text)
    return {"simplified_text": simplified_text}

#sese çevirme
@app.post("/tts")
async def tts_endpoint(input: TextInput):
  audio_url = synthesize_text(input.text)
  return {"audio_url": audio_url}

#görsel oluşturma
@app.post("/generate-image")
async def image_endpoint(input: TextInput):
  image_url = get_image_url(input.text)
  return {"image_url": image_url}