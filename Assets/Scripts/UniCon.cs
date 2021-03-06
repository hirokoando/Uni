﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniCon : MonoBehaviour {

    //状態
  　public enum UniState : int
    {
        Nomal = 0,
        Stop = 1,
        Bomb = 2,
        Clim = 3,
        Step = 4,
        Fly = 5,
      
    }
    public UniState unistate = UniState.Nomal;

    //接地
    bool isGround;
    // 着地できるレイヤー
    public LayerMask Ground;
    //　落ちた場所
    private float fallPosition;
    //　落ちた地点を設定したかどうか
    private bool fallFlag;
    //　落下してから地面に落ちるまでの距離
    private float distance;
    //　どのぐらいの高さからダメージを与えるか
    private float damageDistance = 10f;

    //死亡判定
    public bool dead = false;

    //移動スピード
    private float Nspeed = 0.05f;

    //turn=true right 
    public bool turn;
    //登る
    public bool climR = false;
    public bool climL = false;
    public bool climFin = false; 
    
    //  登る最大地点
    public float maxClim = 18;

    //各子オブジェクト
    [SerializeField]
    GameObject turnRC;
    [SerializeField]
    GameObject turnLC;
    [SerializeField]
    GameObject bombT;
    [SerializeField]
    GameObject stopC;
   
    //Bomb
    //時間計測
    public float alltime = 0.0f;
    //爆発まで
    private float bombtimer = 5.0f;
    private float animetimer = 4.0f;
    //ゲームオブジェクト
    Collider2D[] targets;


    //戒壇
    //時間経過保存
    float steptime;
    //span
    float stepspan = 0.8f;
    //時間制限
    float steplimit =20.0f;
    //階段モード　レイキャスト角度
    float rey;
    //初回フラグ
    bool stepF = true;
    //ターンの値保存
    bool oldRL;

    //アニメーションするためのコンポーネントを入れる
    Animator animator;
    //アニメボム
    bool bombanime = false;

    //タイルマップ取得
    GameObject tilemap;
    TileMapController tilemapcon;
    GameObject tilemapStep;
    TileStepCon tscon;
    //階段プレハブ
    [SerializeField]
    GameObject stepPrefab;



    //rimortCon取得
    RemortCon remocon;

    // Use this for initialization
    void Start () {

        // アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        // タイルマップ取得
        tilemap = GameObject.Find("Tilemap");
        this.tilemapcon = tilemap.GetComponent<TileMapController>();

        tilemapStep = GameObject.Find("TilemapStep");
        this.tscon = tilemapStep.GetComponent<TileStepCon>();

        //リモート取得
        this.remocon = GameObject.Find("Remort").GetComponent<RemortCon>();

        //落下判定変数たち
        distance = 0f;
        fallPosition = transform.position.y;
        fallFlag = false;

        //右へ進む
        turn = true;
    }
	
	// Update is called once per frame
	void Update () {

        // 地面との衝突を検知する箱
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,// 始点 
            new Vector2(1.5f,0.5f), // さいず　
            0.0f,//角度
            Vector2.down,//向き
            0.6f,//距離
            Ground);//感知するレイヤー
        
        //設置判定
        if (hit.collider)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }


        //死亡
        if (dead)
        {
            remocon.dieunis += 1;
            Destroy(gameObject);
        }



        //状態による各動作切り替え
        switch (unistate)
        {
            case UniState.Nomal:
                NomalMove();
                DeadHigh();
                break;

            case UniState.Stop:
                StopMove();
                DeadHigh();
                break;

            case UniState.Bomb:
                BombMove();
                DeadHigh();
                break;

            case UniState.Clim:
                ClimMove();
                DeadHigh();
                break;

            case UniState.Step:
                StepMove();
                DeadHigh();
                break;

            case UniState.Fly:
                NomalMove();
                break;

        }


    }

    //ノーマルの動作
    void NomalMove()
    {
        this.GetComponent<CircleCollider2D>().enabled = true;
        turnRC.GetComponent<BoxCollider2D>().enabled = true;
        turnLC.GetComponent<BoxCollider2D>().enabled = true;
        stopC.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


        climR = false;
        climL = false;
        climFin = false;

        if (isGround)
        {
            if (turn)
            {
                this.transform.position += new Vector3(Nspeed, 0);
            }
            else
            {
                this.transform.position += new Vector3(-Nspeed, 0);
            }
        }

        
    }


    //ストップの動作
    void StopMove()
    {
        this.GetComponent<CircleCollider2D>().enabled = true;
        turnRC.GetComponent<BoxCollider2D>().enabled = false;
        turnLC.GetComponent<BoxCollider2D>().enabled = false;
        stopC.GetComponent<CircleCollider2D>().enabled = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll ;
       
    }

    //ボムの動作
    void BombMove ()
    {
       
        this.GetComponent<CircleCollider2D>().enabled = true;
        turnRC.GetComponent<BoxCollider2D>().enabled = false;
        turnLC.GetComponent<BoxCollider2D>().enabled = false;
        stopC.GetComponent<CircleCollider2D>().enabled = false;
        TextMesh bombt = bombT.GetComponent<TextMesh>();
        this.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //時間経過
        alltime += Time.deltaTime;
        //爆発まで残り秒
        float limit = animetimer+0.5f - alltime;

        if (limit > 0)
        {
           //爆発までタイマー表示
            bombt.text = limit.ToString("F0");

        }

        if (bombtimer + 0.3f <= alltime)
        {　
            //5秒経ったら自分(親ごと)を消去
            alltime = 0.0f;
            
            dead = true;

        }
        else if (bombtimer <= alltime)
        {
            
            
            //5秒でタイルマップにposition伝える(タイル消去)
            tilemapcon.BombDelete(this.gameObject.transform.position);


            //（ステップのレイヤー番号１０）
            int layerMask = 1 << 11;
            targets = Physics2D.OverlapCircleAll(transform.position, 1, layerMask);

            // GameObject型の変数に、中身を順番に取り出す。
            // foreachは配列の要素の数だけループします。
            foreach (Collider2D st in targets)
            {
                // 消す！
                Destroy(st.gameObject);
            }

            //タイマー表示消す
            bombT.SetActive(false);

        }
        else if (animetimer <= alltime){
            
            //４秒でアニメ開始
            if (bombanime == false)
            {
                animator.Play("Bomb");
               
                bombanime = true;
            }
        }
      

    }

    //Climの動作
    void ClimMove()
    {

        this.GetComponent<CircleCollider2D>().enabled = true;
        turnRC.GetComponent<BoxCollider2D>().enabled = true;
        turnLC.GetComponent<BoxCollider2D>().enabled = true;
        stopC.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        if (isGround)
        {
            if (turn)
            {
                this.transform.position += new Vector3(Nspeed, 0);
            }
            else
            {
                this.transform.position += new Vector3(-Nspeed, 0);
            }
        }

        if(maxClim >= this.transform.position.y)
        {
            if (climR)
            {
                this.transform.position += new Vector3(Nspeed, 0.1f);
            }

            if (climL)
            {
                this.transform.position += new Vector3(-Nspeed, 0.1f);
            }
        }
        else
        {
            unistate = UniState.Nomal;
        }

        if(climFin == true)
        {
            //時間経過
            alltime += Time.deltaTime;

            if (alltime >= 0.3f)
            {
                climR = false;
                climL = false;
                unistate = UniCon.UniState.Nomal;
                alltime = 0;

                climFin = false;

            }
        }
       

    }

    //階段の動作
    void StepMove()
    {
        this.GetComponent<CircleCollider2D>().enabled = true;
        turnRC.GetComponent<BoxCollider2D>().enabled = true;
        turnLC.GetComponent<BoxCollider2D>().enabled = true;
        stopC.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //時間経過
        alltime += Time.deltaTime;
       

        if (turn)
        {
            rey = 20.0f;
        }
        else
        {
            rey = -20.0f;
        }

        // 地面との衝突を検知する箱
        RaycastHit2D hitstep = Physics2D.BoxCast(
            transform.position,// 始点 
            new Vector2(1.0f, 1.0f), // さいず　
            rey,//角度
            Vector2.down,//向き
            1.5f,//距離
            Ground);//感知するレイヤー

        //設置判定
        if (hitstep.collider)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if(stepF == true)
        {
            oldRL = turn;
            stepF = false;
        }

        if (steplimit >= steptime)
        {
            if (isGround)
            {
                    if(turn == oldRL)
                    {
                        if (turn)
                        {
                            this.transform.position += new Vector3(0.02f, 0.01f);
                        }
                        else
                        {
                            this.transform.position += new Vector3(-0.02f, 0.01f);
                        }
                       
                    }
                    else
                    {
                        steptime = 0;
                        stepF = true;
                        unistate = UniCon.UniState.Nomal;
                    }
                
            }

            if (stepspan <= alltime)
            {
               
                steptime += alltime;
                //tscon.StepCreate(this.transform.position, turn);
                GameObject road = Instantiate(stepPrefab) as GameObject;
                if (turn)
                {
                    road.transform.position = new Vector2(this.transform.position.x +1.5f, this.transform.position.y - 1.0f);
                }
                else
                {
                    road.transform.position = new Vector2(this.transform.position.x -1.5f, this.transform.position.y - 1.0f);
                }
                
                road.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rey);

                alltime = 0;
            }

        }
        else
        {
            steptime = 0;
            stepF = true;
            unistate = UniCon.UniState.Nomal;
        }


    }

    void DeadHigh()
    {
        //　落ちている状態
        if (fallFlag)
        {

            //　落下地点と現在地の距離を計算（ジャンプ等で上に飛んで落下した場合を考慮する為の処理）
            fallPosition = Mathf.Max(fallPosition, transform.position.y);
           

            //　地面にレイが届いていたら
            if(isGround)
            {
                //　落下距離を計算
                distance = fallPosition - transform.position.y;
                
                //　落下によるダメージが発生する距離を超える場合ダメージを与える
                if (distance >= damageDistance)
                {

                    dead = true;
                   

                }
                fallFlag = false;
            }
        }
        else
        {
            //　地面にレイが届いていなければ落下地点を設定
            if (!isGround)
            {
                //　最初の落下地点を設定
                fallPosition = transform.position.y;
                distance = 0;
                fallFlag = true;
            }
        }
    }

  
  

    //deadobjに触れたら
    private void OnTriggerEnter2D(Collider2D othercoll)
    {
        if(othercoll.tag == "DamageObject")
        {
            dead = true;
            
        }

       
    }

}
