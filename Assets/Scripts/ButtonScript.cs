using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private SpriteRenderer button;
    public Sprite buttonDefault;
    public Sprite buttonPressed;

    public KeyCode key;
    public int whichButton;
    public bool created;

    public GameObject notePrefab;

    void Start()
    {
        // "Player1" = 6
        if (gameObject.layer == 6)
        {
            if (whichButton == 1) {
                key = GameManager.instance.P1Key1;
            } else if (whichButton == 2) {
                key = GameManager.instance.P1Key2;
            } else if (whichButton == 3) {
                key = GameManager.instance.P1Key3;
            } else if (whichButton == 4) {
                key = GameManager.instance.P1Key4;
            } 
        } 
        // "Player2" = 7
        else if (gameObject.layer == 7) 
        {
            if (whichButton == 1) {
                key = GameManager.instance.P2Key1;
            } else if (whichButton == 2) {
                key = GameManager.instance.P2Key2;
            } else if (whichButton == 3) {
                key = GameManager.instance.P2Key3;
            } else if (whichButton == 4) {
                key = GameManager.instance.P2Key4;
            } 
        }

        button = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!GameManager.instance.gameEnd) {
            
            if (Input.GetKey(key))
            {
                button.sprite = buttonPressed;
            } else {
                button.sprite = buttonDefault;
            }

            
            if (Input.GetKeyDown(key))
            {
                if (GameManager.instance.canCreate && !created) {  
                    // "Player1" = 6
                    if (gameObject.layer == 6)
                    {
                        if (GameManager.instance.whoseTurn == 1)
                        {
                            if (whichButton == 1) {
                                GameManager.instance.P1hihat.Stop();
                                GameManager.instance.P1hihat.Play();
                            } else if (whichButton == 2) {
                                GameManager.instance.P1snare.Stop();
                                GameManager.instance.P1snare.Play();
                            } else if (whichButton == 3) {
                                GameManager.instance.P1snare2.Stop();
                                GameManager.instance.P1snare2.Play();
                            } else if (whichButton == 4) {
                                GameManager.instance.P1kick.Stop();
                                GameManager.instance.P1kick.Play();
                            } 
                            created = true;
                            GameManager.instance.buttonsPressed += 1;
                            if (GameManager.instance.buttonsPressed > 1) {
                                if (GameManager.instance.p1OffenseUltimate > 0f) GameManager.instance.p1OffenseUltimate -= 1f;
                            } else {
                                GameManager.instance.p1OffenseUltimate += 3f;
                            }
                            Instantiate(notePrefab, transform.position, Quaternion.identity);
                        }
                    }
                    // "Player2" = 7
                    else if (gameObject.layer == 7) 
                    {
                        if (GameManager.instance.whoseTurn == 2)
                        {
                            if (whichButton == 1) {
                                GameManager.instance.P2hihat.Stop();
                                GameManager.instance.P2hihat.Play();
                            } else if (whichButton == 2) {
                                GameManager.instance.P2snare.Stop();
                                GameManager.instance.P2snare.Play();
                            } else if (whichButton == 3) {
                                GameManager.instance.P2snare2.Stop();
                                GameManager.instance.P2snare2.Play();
                            } else if (whichButton == 4) {
                                GameManager.instance.P2kick.Stop();
                                GameManager.instance.P2kick.Play();
                            } 
                            created = true;
                            GameManager.instance.buttonsPressed += 1;
                            if (GameManager.instance.buttonsPressed > 1) {
                                if (GameManager.instance.p2OffenseUltimate > 0f) GameManager.instance.p2OffenseUltimate -= 1f;
                            } else {
                                GameManager.instance.p2OffenseUltimate += 3f;
                            }
                            Instantiate(notePrefab, transform.position, Quaternion.identity);
                        }
                    }
                } else {
                    // "Player1" = 6
                    if (gameObject.layer == 6 && GameManager.instance.whoseTurn == 1)
                    {
                        GameManager.instance.P1miss.Stop();
                        GameManager.instance.P1miss.Play();
                    }
                    // "Player2" = 7
                    else if (gameObject.layer == 7  && GameManager.instance.whoseTurn == 2) 
                    {
                        GameManager.instance.P2miss.Stop();
                        GameManager.instance.P2miss.Play();
                    }
                }
            } 

            if (GameManager.instance.canCreate == false) created = false;
        }
    }
}
