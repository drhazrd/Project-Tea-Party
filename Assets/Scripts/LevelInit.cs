using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelInit : MonoBehaviour
{
    public GameObject BlackoutScreen;
    public bool blackout = false;
    [SerializeField]
    Animator black_out_anim;
    float fadeInSpeed, fadeOutSpeed;
    GameManager _gm;
    

    void Start()
    {
        _gm = GameManager._gameManager;
        black_out_anim = BlackoutScreen.GetComponent<Animator>();
        //BlackoutScreen.SetActive(false);
    }

    void Update()
    {
        black_out_anim.SetBool("BlackOut", blackout);
        if(black_out_anim.GetCurrentAnimatorClipInfo(0).Length > .9f)
        {
            LevelWasStarted();
        }
    }
    private void LevelWasStarted()
    {
        
            GameManager._gameManager.StartCoroutine("GameStart");
            GameManager._gameManager.gameStarted = true;

    }
    public void Blackout()
    {
        Debug.Log("BlackCalled");
    }

}