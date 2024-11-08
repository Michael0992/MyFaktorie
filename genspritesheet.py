import os
from glob import glob
from PIL import Image

# Parameter
sprite_width = 128
sprite_height = 128
columns = 9
rows = 8
background_color = (255, 0, 255)  # Eine Farbe, die als transparent markiert werden soll
folder_path = "./Tiles/Grass"  # Ordnerpfad mit den Bildern

# Erstelle das GIF-Spritesheet
spritesheet_width = sprite_width * columns
spritesheet_height = sprite_height * rows
spritesheet = Image.new("RGBA", (spritesheet_width, spritesheet_height), background_color)

# Lade alle PNG-Dateien aus dem Ordner
image_paths = sorted(glob(os.path.join(folder_path, "*.png")))[:68]  # Die ersten 68 Bilder laden

for i, image_path in enumerate(image_paths):
    image = Image.open(image_path).convert("RGBA")
    
    # Position auf dem Spritesheet berechnen
    x = (i % columns) * sprite_width
    y = (i // columns) * sprite_height
    
    # Füge das Bild ins Spritesheet ein
    spritesheet.paste(image, (x, y))

# Konvertiere in GIF und wähle die Farbe als transparent
spritesheet = spritesheet.convert("P", palette=Image.ADAPTIVE, colors=256)
spritesheet.info["transparency"] = spritesheet.getpixel((0, 0))  # Setzt die obere linke Farbe als transparent

# Speichere das Spritesheet als GIF
spritesheet.save("idle.gif", transparency=spritesheet.info["transparency"])
