from gtts import gTTS
import uuid
import os

def synthesize_text(text: str) -> str:
    filename = f"output_{uuid.uuid4().hex}.mp3"
    path = os.path.join("static", filename)
    os.makedirs("static", exist_ok=True)
    try:
      tts = gTTS(text, lang='tr')
      tts.save(path)
      return f"/static/{filename}"
    except Exception as e:
      print("TTS error:", e)
      return ""