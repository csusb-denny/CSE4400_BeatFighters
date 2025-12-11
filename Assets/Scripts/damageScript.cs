using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        NoteScript noteScript = other.GetComponent<NoteScript>();
        // "Player1" = 6
        if (gameObject.layer == 6)
        {
            GameManager.instance.p1Health -= noteScript.noteDamage;
            GameManager.instance.P1miss.Stop();
            GameManager.instance.P1miss.Play();
        }
        // "Player2" = 7
        else if (gameObject.layer == 7) 
        {
            GameManager.instance.p2Health -= noteScript.noteDamage;
            GameManager.instance.P2miss.Stop();
            GameManager.instance.P2miss.Play();
        }
    }
}
