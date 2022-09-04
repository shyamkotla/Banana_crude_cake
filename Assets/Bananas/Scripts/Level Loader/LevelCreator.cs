using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour
{
    #region Variables
    [SerializeField] string filename = "sample";
    [SerializeField] int level;
    [SerializeField] int wave;
    [SerializeField] Tilemap levelTileMap;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] List<Coin> collectibles;
    #endregion

    #region UnityMethods
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            CreateLevelInfo();
    }
    #endregion

    #region PublicMethods
    public void CreateLevelInfo()
    {
        LevelInfo levelInfo = ScriptableObject.CreateInstance<LevelInfo>();
        AssignLevelInfo(levelInfo);
        AssetDatabase.CreateAsset(levelInfo, "Assets/Bananas/Levels Info/" + filename + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
    #endregion

    #region PrivateMethods
    void AssignLevelInfo(LevelInfo lv)
    {
        lv.wave = this.wave;
        lv.level = this.level;
        lv.levelTileMap = this.levelTileMap.GetComponent<Tilemap>();
        for(int i = 0; i < this.enemies.Count; i++)
        {
            lv.enemies.Add((Vector2)this.enemies[i].transform.position);
        }
        for (int i = 0; i < this.enemies.Count; i++)
        {
            lv.collectibles.Add((Vector2)this.collectibles[i].transform.position);
        }
    }
    #endregion

    #region GameEventListeners

    #endregion
}