using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileStepCon : MonoBehaviour {
    Tilemap tiles;
    [SerializeField]
    TileBase[] tileRight;
    [SerializeField]
    TileBase[] tileLeft;


    // Use this for initialization
    void Start () {
        tiles = GetComponent<Tilemap>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StepCreate(Vector3 posi, bool RL)
    {

        Vector3Int nowPos = tiles.WorldToCell(posi);
        Vector3Int stepPos0;
        Vector3Int stepPos1;

        if (RL)
        {
            stepPos0 = new Vector3Int(nowPos.x + 1, nowPos.y - 2, nowPos.z);
            stepPos1 = new Vector3Int(nowPos.x + 2, nowPos.y - 2, nowPos.z);

            var positionArray = new[]
            {
                 stepPos0,
                 stepPos1,
            };

            tiles.SetTiles(positionArray, tileRight);
        }
        else
        {
            stepPos0 = new Vector3Int(nowPos.x - 1, nowPos.y - 2, nowPos.z);
            stepPos1 = new Vector3Int(nowPos.x - 2, nowPos.y - 2, nowPos.z);

            var positionArray = new[]
            {
                 stepPos0,
                 stepPos1,
            };

            tiles.SetTiles(positionArray, tileLeft);
        }



    }

}
