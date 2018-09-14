using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMapController : MonoBehaviour {

    Tilemap tiles;
    int bombrange = 4;
  
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


    //uniコリダ―で送った座標を返還、タイルなしにする

    public void BombDelete(Vector3 pos)
    {
        Vector3Int bombPosition = tiles.WorldToCell(pos);

        for (int ix = -bombrange; ix <= bombrange; ix++)
        {
            for (int iy = -bombrange; iy <= bombrange; iy++)
            {
                Vector3Int bombPosition2 = new Vector3Int(bombPosition.x + ix, bombPosition.y + iy, bombPosition.z);
                tiles.SetTile(bombPosition2, null);
            }

        }


    }

   

}
