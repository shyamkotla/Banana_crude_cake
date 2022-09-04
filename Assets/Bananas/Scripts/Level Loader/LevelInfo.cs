using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public int level;
    public int wave;
    public Tilemap levelTileMap;
    public List<Vector2> enemies = new List<Vector2>();
    public List<Vector2> collectibles = new List<Vector2>();
}