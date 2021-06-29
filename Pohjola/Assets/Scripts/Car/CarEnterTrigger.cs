using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterTrigger : MonoBehaviour
{
    private const string PLAYER_NAME = "Player";

    private DisableCar disableCarScript;
    private GameObject player;

    private void Awake()
    {
        disableCarScript = gameObject.GetComponent<DisableCar>();
        player = GameObject.Find(PLAYER_NAME);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_NAME))
        {
            Debug.Log("Player entered the trigger");
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            disableCarScript.canEnterCar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_NAME))
        {
            if (player.activeSelf == false)
            {
                return;
            }
            Debug.Log("Player left the trigger");
            disableCarScript.canEnterCar = false;
            this.enabled = false;
        }
    }
}
