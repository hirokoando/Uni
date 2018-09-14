using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemortCon : MonoBehaviour {
    //prefab
    [SerializeField]
    GameObject unis;
    //Canvas
    [SerializeField]
    GameObject canvass;
    GameObject nomalB;
    GameObject stopB;
    GameObject bombB;
    GameObject climB;
    GameObject stepB;
    GameObject flyB;
    GameObject textAll;
    GameObject textGoal;
    GameObject textTimer;
    GameObject textMesage;
    //そのた
    [SerializeField]
    GameObject cameras;

    CameraCon camecon;

    [SerializeField]
    GameObject goal;

    //初期値
    public int alluniNum ;
    public int nomalNum ;
    public int stopNum ;
    public int bombNum ;
    public int climNum ;
    public int stepNum ;
    public int flyNum ;
    public float timerM;
    public float timerS;
    public float goalaim ;
    public float unispan ;
    //ウニゴール数
    public int goalunis = 0;
    //ウニ死亡数
    public int dieunis = 0;
    //ウニ発生数
    private int uniOk = 0;
    //タイム
    private float alltime = 0;
    private float unitime;
    //旗
    private bool gameOver = false;

    // Use this for initialization
    void Start () {

        //ボタンとテキスト
        nomalB = canvass.transform.Find("Nomal").gameObject;
        stopB = canvass.transform.Find("Stop").gameObject;
        bombB = canvass.transform.Find("Bomb").gameObject;
        climB = canvass.transform.Find("Clim").gameObject;
        stepB = canvass.transform.Find("Step").gameObject;
        flyB = canvass.transform.Find("Fly").gameObject;
        textAll = canvass.transform.Find("UniAllText").gameObject;
        textGoal = canvass.transform.Find("UniGoalText").gameObject;
        textTimer = canvass.transform.Find("TimerText").gameObject;
        textMesage = canvass.transform.Find("MesageText").gameObject;

        //kamera
        camecon = cameras.GetComponent<CameraCon>();

    }
	
	// Update is called once per frame
	void Update () {

       //ウニの発生
        unitime += Time.deltaTime;
        if (unitime >= unispan && uniOk < alluniNum )
        {
            GameObject UniUni = Instantiate(unis) as GameObject;
            UniUni.transform.position = this.transform.position;
            uniOk += 1;
            unitime = 0;
        }
           

        if (gameOver == false)
        {
            //タイマーテキスト表示
           

            //　一旦トータルの制限時間を計測；
            alltime = timerM * 60 + timerS;
            alltime -= Time.deltaTime;

            //　再設定
            timerM = (int)alltime / 60;
            timerS = alltime - timerM * 60;

            textTimer.GetComponent<Text>().text = "Time " + timerM.ToString("00") + " : " + ((int)timerS).ToString("00");

            if (alltime < 0 || alluniNum - dieunis < goalaim )
            {
                gameOver = true;
            }
        }

        //ボタンテキスト表示
        nomalB.transform.Find("Num").gameObject.GetComponent<Text>().text = nomalNum.ToString();
        stopB.transform.Find("Num").gameObject.GetComponent<Text>().text = stopNum.ToString();
        bombB.transform.Find("Num").gameObject.GetComponent<Text>().text = bombNum.ToString();
        climB.transform.Find("Num").gameObject.GetComponent<Text>().text = climNum.ToString();
        stepB.transform.Find("Num").gameObject.GetComponent<Text>().text = stepNum.ToString();
        flyB.transform.Find("Num").gameObject.GetComponent<Text>().text = flyNum.ToString();
        
        //Die　All　テキスト表示
        textAll.GetComponent<Text>().text = "Die / All " + dieunis.ToString() + " / " + alluniNum.ToString();
        //Goal　テキスト表示
        textGoal.GetComponent<Text>().text = "Goal " + goalunis.ToString() + " / " + goalaim.ToString();
        //メッセージテキスト表示
        if(goalunis >= goalaim)
        {
            textMesage.GetComponent<Text>().text = "Game Clear";
        }
        if (gameOver)
        {
            textMesage.GetComponent<Text>().text = "Game Over";
        }

        
    }

    

    public void NomalClick()
    {
        var unia = camecon.uni1.GetComponent<UniCon>();


        if (unia == null)
        {
            return;
        }

        if (UniCon.UniState.Nomal != unia.unistate && nomalNum >0)
        {
            unia.unistate = UniCon.UniState.Nomal;
            nomalNum+= -1;
        }
        
    }

    public void StopClick()
    {
        var unia = camecon.uni1.GetComponent<UniCon>();


        if (unia == null)
        {
            Debug.Log("null");

            return;
        }

        if (UniCon.UniState.Stop != unia.unistate && stopNum > 0)
        {
            unia.unistate = UniCon.UniState.Stop;
            stopNum += -1;
        }
        
    }

    public void BombClick()
    {
        var unia = camecon.uni1.GetComponent<UniCon>();

        if(unia == null)
        {
            return;
        }

        if( UniCon.UniState.Bomb != unia.unistate && bombNum>0)
        {
            unia.unistate = UniCon.UniState.Bomb;
            bombNum += -1;
        }
        
    }

    public void ClimClick()
    {
        var unia = camecon.uni1.GetComponent<UniCon>();


        if (unia == null)
        {
            return;
        }

        if (UniCon.UniState.Clim != unia.unistate && climNum>0)
        {
            unia.unistate = UniCon.UniState.Clim;
            climNum += -1;
        }
        
    }

    public void StepClick()
    {
        var unia = camecon.uni1.GetComponent<UniCon>();


        if (unia == null)
        {
            return;
        }

        if (UniCon.UniState.Step != unia.unistate && stepNum>0)
        {
            unia.unistate = UniCon.UniState.Step;
            stepNum += -1;
        }
        
    }

    public void FlyClick()
    {
        var unia = camecon.uni1.GetComponent<UniCon>();


        if (unia == null)
        {
            return;
        }

        if (UniCon.UniState.Fly != unia.unistate && flyNum>0)
        {
            unia.unistate = UniCon.UniState.Fly;
            flyNum += -1;
        }
        
    }

}
