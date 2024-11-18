using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionChecker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            //Destroy Player 
            Destroy(gameObject);
            GameObject.FindFirstObjectByType<GameManager>().RestartGame();
        }
    }
}
