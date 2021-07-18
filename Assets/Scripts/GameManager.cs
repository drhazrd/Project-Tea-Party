using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager _gameManager;
	LevelInit level;
	public int currentGold;
	public Text goldText;
	public bool playerCanMove;

	//Timer
	float currentTime;
    readonly float startTime = 10f;

	public bool timerActive;
	[SerializeField]
	Text timerText;

	//Countdown
	int countDowncounter, countDownstart;
	[SerializeField]
    private GameObject gameOverScreen;
    public bool gameStarted, gameEnded = false;

    private void Awake()
	{
		_gameManager = this;
		gameOverScreen.SetActive(false);
		level = FindObjectOfType<LevelInit>();
	}

	void Start() {

		currentTime = startTime;
	}

	// Update is called once per frame
	void Update() {


		if (currentTime <= 0.1f)
		{
			gameEnded = true;
			timerActive = false;
			timerText.gameObject.SetActive(false);
			StartCoroutine("GameOver");
		}
			timerText.text = currentTime.ToString();
		if (timerActive)
		{
		timerText.gameObject.SetActive(true);
		currentTime -= 1 * Time.deltaTime;
		}
		else { return; }
		
		if (gameEnded)
		{
		Debug.Log("GameOver");
		}
		//Text
	}

	public IEnumerator GameStart()
    {
		
		yield return new WaitForSeconds(1.5f);
		playerCanMove = true;
		yield return new WaitForSeconds(.5f);
		timerActive = true;
		
    }
    private IEnumerator GameOver()
    {
		playerCanMove = false;
		gameOverScreen.SetActive(true);
		yield return new WaitForSeconds(.5f);
		level.blackout = true;
		yield return null;
		Reset();
    }
	/*

    public void AddGold(int goldToAdd) {
		currentGold += goldToAdd;
		goldText.text = "Gold: " + currentGold + " g";
	}
	public IEnumerator Countdown()
    {
		countDowncounter = countDownstart;
		while(countDowncounter > 0)
        {
			countDowncounter-- ;
			yield return new WaitForSeconds(1f);

        }
	}*/
	public void Reset()
	{
		StopAllCoroutines();
    }
}
