from PIL import Image

def reduce_colors_and_save_as_gif(image_path, output_path):
    # Bild öffnen
    image = Image.open(image_path)
    
    # Bild auf 256 Farben reduzieren
    image = image.convert("P", palette=Image.ADAPTIVE, colors=256)
    
    # Als GIF speichern
    image.save(output_path, format="GIF")

# Bild- und Zielpfade anpassen
image_path = r"C:\Users\miche\OneDrive\Documents\myfactorie\Tiles\Tileset1.png"
output_path = r"C:\Users\miche\OneDrive\Documents\myfactorie\Tiles\Tileset1.gif"

# Funktion aufrufen
reduce_colors_and_save_as_gif(image_path, output_path)
