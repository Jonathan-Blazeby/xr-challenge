using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Camera mainCam;
    private GameObject player;
    private TMPro.TextMeshProUGUI scoreText;
    private TMPro.TextMeshProUGUI messageText;
    private float damageElapsedTime;
    private float damageTimeDelay = 1.0f;
    private int maxScore = 600;
    private int score;
    private bool finishAllowed;

    public static GameController GameCon;
    public enum damageSources { nothing = 0, lava = 10 }
    
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        scoreText = GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
        messageText = GameObject.Find("MessageText").GetComponent<TMPro.TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (GameCon == null)
        {
            GameCon = this;
        }
        else
        {
            Destroy(gameObject); //Prevents there being 2 menu controller objects when reseting
        }      

        scoreText = GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
        messageText = GameObject.Find("MessageText").GetComponent<TMPro.TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        score = 0;
        finishAllowed = false;
    }

    void Update()
    {
        if(player.transform.position.y < -5.0f)
        {
            Death();
        }

        damageElapsedTime += Time.deltaTime;

    }

    /// <summary>
    /// Called when Pickup picked up, updates score GUI, when all Pickups collected outputs relevant message
    /// </summary>
    public void UpdateScore(int scoreChange)
    {
        if(scoreChange != -1)
        {
            score += scoreChange;
            scoreText.text = "SCORE: " + score;

            if(score >= maxScore)
            {
                finishAllowed = true;
                messageText.text = "You Collected all the Stars!\nNow head to the Rocket!";
                Invoke("ResetText", 3);
            }
        }
    }

    /// <summary>
    /// Called if player enters EndZone, checks if all stars have been collected, outputs relevant message, calls reset if victory
    /// </summary>
    public void TryWin()
    {
        if(finishAllowed)
        {
            messageText.text = "You won! Thank you for playing!";
            Invoke("ResetText", 5);
            Invoke("Reset", 5);
        }    
        else
        {
            messageText.text = "You still need more stars";
            Invoke("ResetText", 3);
        }
    }

    /// <summary>
    /// Called if player's health reduced to zero (or falls off map)
    /// </summary>
    public void Death()
    {
        messageText.text = "You Died!";
        Invoke("ResetText", 3);
        Invoke("Reset", 3);
    }

    /// <summary>
    /// Sets message text to empty
    /// </summary>
    private void ResetText()
    {
        messageText.text = "";
    }

    /// <summary>
    /// Reloads the scene and sets GameContoller variables back to zero
    /// </summary>
    private void Reset()
    {
        score = 0;
        UpdateScore(0);
        finishAllowed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    /// <summary>
    /// Checks for if victory condition is met
    /// </summary>
    public bool CheckFinishAllowed()
    {
        return finishAllowed;
    }

}
