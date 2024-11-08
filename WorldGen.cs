using System;
using Godot;

public partial class WorldGen : TileMapLayer
{

    private FastNoiseLite noise;
    private Camera2D camera;
    private int[] lastchunk;

    public override void _Ready()
    {  
        camera = GetNodeOrNull<Camera2D>("../Camera2D");
        noise = new FastNoiseLite();
        noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
        noise.Frequency = 0.1f;    
    }

    public override void _Process(double delta)
    {
        int x = (int)camera.Position.X , y = (int)camera.Position.Y;
 
        int[] currentchunk = {x/1280,y/1280};

        
        if(currentchunk != lastchunk){
            lastchunk = new int[] {currentchunk[0],currentchunk[1]};
            GenerateTiles(currentchunk);
        }
        
    }

    private void GenerateTiles(int[] chunk)
    {
        
        int sourceId = 0; // Die ID des Tiles im TileSet
        Vector2I atlasCoords = new Vector2I(3, 1); 

        // Erstellen eines 10x10-Rasters an Tiles
        for (int x = -20; x < 20; x++)
        {
            for (int y = -20; y < 20; y++)
            {
                float noiseValue = noise.GetNoise2D(x+), y+(chunk[1]*40));
                atlasCoords = noiseValue == 0 ? new Vector2I(6, 1) : new Vector2I(3, 1);
                Vector2I coords = new Vector2I(x+(chunk[0]*40), y+(chunk[1]*40));
                SetCell(coords, sourceId, atlasCoords); 
                
            
            }
        }
    }
}
