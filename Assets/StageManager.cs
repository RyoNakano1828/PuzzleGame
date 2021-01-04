using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] TextAsset stageFile;
    [SerializeField] GameObject[] prefabs;

    enum STAGE_TYPE
    {
        WALL,
        GROUND,
        BLOCK_POINT,
        BLOCK,
        PLAYER
    }
    STAGE_TYPE[,] stageTable;

    void LoadStageData()
    {
        string[] lines = stageFile.text.Split(new[] { '\n', '\r' });
        int rows = lines.Length;
        int columns = lines[0].Split(new[] { ',' }).Length;
        stageTable = new STAGE_TYPE[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            string[] values = lines[x].Split(new[] { ',' });
            for (int y = 0; y < columns; y++)
            {
                stageTable[x, y] = (STAGE_TYPE)int.Parse(values[y]);
                Debug.Log($"{x}:{y} => {stageTable[x, y]}");
            }
        }
    }

    float tileSize;
    void CreateStage()
    {
        tileSize = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

        for(int x = 0; x < stageTable.GetLength(0); x++)
        {
            for(int y = 0; y < stageTable.GetLength(1); y++)
            {
                STAGE_TYPE stageType = stageTable[x, y];
                GameObject obj = Instantiate(prefabs[(int)stageType]);
                obj.transform.position = new Vector2(y, x);
            }
        }
    }

    Vector2 GetScreenPositionFromTileTable(Vector2Int position)
    {
        return new Vector2(position.x * tileSize, position.y * tileSize);
    }

    void Start()
    {
        LoadStageData();
        CreateStage();
    }


}
