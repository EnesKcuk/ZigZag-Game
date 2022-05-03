using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    delegate void TurnDelegate();
    TurnDelegate turnDelegate;

    public float moveSpeed = 3;
    bool lookingRight = true;
    GameManager gameManager;
    Animator anim;
    public Transform rayOrigin;
    public ParticleSystem effect;
    // Start is called before the first frame updateS

    public Text scoreTxt, hScoreTxt;

    public int Score { get; private set; }
    public int HScore { get; private set; }

    void Start()
    {
        //#region PLATFORM FOR TURNING
        //    #if UNITY_EDITOR
        //            turnDelegate = TurnPlayerUsingKeyboard;
        //    #endif

        //    #if UNITY_ANDROID
        //            turnDelegate = TurnPlayerUsingTouch;
        //    #endif
        //#endregion

        gameManager = GameObject.FindObjectOfType<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        LoadHighScore();

    }

    private void LoadHighScore()
    {
        HScore = PlayerPrefs.GetInt("hiscore");
        hScoreTxt.text = HScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameStarted) return;
        anim.SetTrigger("gameStarted");

        moveSpeed *= 1.0001f;
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
        //transform.Translate(new Vector3(0,0,1) * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            Turn();

        //turnDelegate();

        checkFalling();
    }

    //private void TurnPlayerUsingKeyboard()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        Turn();
    //}

    //private void TurnPlayerUsingTouch()
    //{
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //        Turn();
    //}




    float elapsedTime = 0;
    float freq = 1 / 5f;

    private void checkFalling()
    {
        if ((elapsedTime += Time.deltaTime) > freq) 
        {
            if(!Physics.Raycast(rayOrigin.position, new Vector3(0, -1, 0)))
            {
                anim.SetTrigger("falling");
                gameManager.RestartGame();
                elapsedTime = 0;
            }
        }   
    }

    public void OnCollisionEnter(Collision Collision)
    {
        Destroy(Collision.gameObject, 1.5f);
    }

    private void Turn()
    {
        if (lookingRight)
        {
            transform.Rotate(new Vector3(0, 1, 0), -90);
        }
        else
        {
            transform.Rotate(new Vector3(0, 1, 0), 90);
        }
        lookingRight =! lookingRight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("crystal"))
        {
            MakeScore();
            CreateEffect();
            Destroy(other.gameObject);
        }
    }

    private void CreateEffect()
    {
        var vfx = Instantiate(effect, new Vector3 (transform.position.x + 1f, transform.position.y+1f, transform.position.z),transform.rotation);
         
        Destroy(vfx, 1f);
    }

    private void MakeScore()
    {
        Score++;
        scoreTxt.text = Score.ToString();
        if(Score > HScore)
        {
            HScore = Score;
            hScoreTxt.text = HScore.ToString();
            PlayerPrefs.SetInt("hiscore", HScore);
        }
    }
}
