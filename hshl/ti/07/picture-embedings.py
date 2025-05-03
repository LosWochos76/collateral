import torch
from torchvision import transforms
from PIL import Image
from transformers import CLIPProcessor, CLIPModel
from scipy.spatial.distance import cosine, euclidean
import os

# Bildpfade
image_files = ["thomas-franz.jpg", "alexander-stuckenholz.jpg", "katze.jpg"]

# Modell und Prozessor laden (CLIP von OpenAI)
model_name = "openai/clip-vit-base-patch32"
model = CLIPModel.from_pretrained(model_name)
processor = CLIPProcessor.from_pretrained(model_name)

# Bilder laden und in Embeddings umwandeln
def get_image_embedding(image_path):
    image = Image.open(image_path).convert("RGB")
    inputs = processor(images=image, return_tensors="pt")
    with torch.no_grad():
        outputs = model.get_image_features(**inputs)
    return outputs[0] / outputs[0].norm()  # normalisiertes Embedding (für Cosinus)

# Embeddings erzeugen
embeddings = {}
for file in image_files:
    if not os.path.exists(file):
        raise FileNotFoundError(f"{file} nicht gefunden.")
    embeddings[file] = get_image_embedding(file)

# Abstände berechnen
def print_distances(embeddings):
    keys = list(embeddings.keys())
    for i in range(len(keys)):
        for j in range(i + 1, len(keys)):
            emb1 = embeddings[keys[i]]
            emb2 = embeddings[keys[j]]
            cos_dist = cosine(emb1, emb2)
            euc_dist = euclidean(emb1, emb2)
            print(f"Distanz zwischen '{keys[i]}' und '{keys[j]}':")
            print(f"  - Kosinusdistanz: {cos_dist:.4f}")
            print(f"  - Euklidische Distanz: {euc_dist:.4f}\n")

# Ausgabe
print_distances(embeddings)