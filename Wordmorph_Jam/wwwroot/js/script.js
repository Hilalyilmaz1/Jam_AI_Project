function highlightText() {
    const input = document.getElementById("userInput").value;
    const words = input.split(" ");
    const output = words.map(word => {
        if (word.length > 5) {
            return `<span style="color:#d32f2f; font-weight:bold;">${word}</span>`;
        } else {
            return word;
        }
    }).join(" ");

    const outputDiv = document.getElementById("outputArea");
    outputDiv.innerHTML = output;
    outputDiv.classList.remove("hidden");
}

function speakText() {
    const outputText = document.getElementById("outputArea").innerText;
    const utterance = new SpeechSynthesisUtterance(outputText);
    utterance.lang = "tr-TR";
    speechSynthesis.speak(utterance);
}

function resetOutput() {
    document.getElementById("userInput").value = "";
    document.getElementById("outputArea").innerHTML = "";
    document.getElementById("outputArea").classList.add("hidden");
}
