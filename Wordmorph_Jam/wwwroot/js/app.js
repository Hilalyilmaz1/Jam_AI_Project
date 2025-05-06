function speakText(text) {
    const utterance = new SpeechSynthesisUtterance(text);
    utterance.lang = 'tr-TR';
    utterance.rate = 1.0;
    speechSynthesis.speak(utterance);
}
