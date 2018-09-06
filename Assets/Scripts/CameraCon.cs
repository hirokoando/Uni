using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour {

    private bool ScrollFlag = false; // スクロールが始まったかのフラグ
    private Vector2 ScrollStartPos = new Vector2(); // スクロールの起点となるタッチポジション

    [SerializeField]
    private static float ScrollMaxLeft = 0f; // 左側への移動制限(これ以上左には進まない)
    [SerializeField]
    public static float ScrollMaxRight = 51.0f; // 右側への移動制限(これ以上右には進まない)


    private static float ScrollDistance= 0.5f; // スクロール距離の調整

    private Vector2 touchPosition = new Vector2(); // タッチポジション初期化


    private Collider2D tageuni1 = null;// タッチ位置にあるオブジェクトの初期化
    
    //tage
    public GameObject uni1;
    [SerializeField]
    GameObject taget;


    // Use this for initialization
    void Start () {
	   
    }
	
	// Update is called once per frame
	void Update () {

        if (null != uni1)
        {
            taget.transform.position = uni1.transform.position;
        }
       

        if (Input.GetMouseButton(0))
        {

            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //（ウニのレイヤー番号１０）
            int layerMask = 1 << 10;
            tageuni1 = Physics2D.OverlapPoint(touchPosition, layerMask);
            

            //ウニが入っていればターゲットの処理
            if (ScrollFlag == false && null != tageuni1)
            {
                taget.GetComponent<SpriteRenderer>().enabled = true;

                uni1 = tageuni1.transform.gameObject;
                
                

            }
            else　//そうでなければスクロールの処理
            {
                
                // タッチした場所に何もない場合、スクロールフラグをtrueに
                ScrollFlag = true;
                if (ScrollStartPos.x == 0.0f)
                {
                    // スクロール開始位置を取得
                    ScrollStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    Vector2 touchMovePos = touchPosition;
                    if (ScrollStartPos.x != touchMovePos.x)
                    {
                        // 直前のタッチ位置との差を取得する
    
                        float diffPos = ScrollDistance * (touchMovePos.x - ScrollStartPos.x);

                        Vector3 pos = this.transform.position;
                        pos.x -= diffPos;
                        // スクロールが制限を超過する場合、処理を止める
                        if (pos.x > ScrollMaxRight || pos.x <  ScrollMaxLeft )
                        {
                            return;
                        }
                        this.transform.position = pos;
                        ScrollStartPos = touchMovePos;
                    }
                }
            }
        }
        else
        {
            // タッチを離したらフラグを落とし、スクロール開始位置も初期化する 
            ScrollFlag = false;
            ScrollStartPos = new Vector2();
        }
    }
    
}
