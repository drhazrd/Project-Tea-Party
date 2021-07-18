using UnityEngine;
using System.Collections;

public class TripTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //GameManager._gameManager.TimerReset();
            //GameManager._gameManager.AddGold(1);
        }
        Debug.Log("This is right");
        Destroy(this.gameObject);
    }
}