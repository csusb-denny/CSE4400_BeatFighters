using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    GameObject opposingButton;
    Vector3 origin;
    Vector3 destination;

    float distanceTraveled;

    // 1 = orange, 2 = blue, 3 = green, 4 = red, 5 = crash
    public int whichNote;

    public float noteDamage;

    public float noteTimer;

    public bool canBePressed;

    public bool hit;

    public bool perfect;

    public KeyCode key;

    void Start()
    {
        // "Player1" = 6
        if (gameObject.layer == 6)
        {
            if (whichNote == 1) {
                opposingButton = GameManager.instance.P1Button1;
                key = GameManager.instance.P1Key1;
            } else if (whichNote == 2) {
                opposingButton = GameManager.instance.P1Button2;
                key = GameManager.instance.P1Key2;
            } else if (whichNote == 3) {
                opposingButton = GameManager.instance.P1Button3;
                key = GameManager.instance.P1Key3;
            } else if (whichNote == 4) {
                opposingButton = GameManager.instance.P1Button4;
                key = GameManager.instance.P1Key4;
            } else if (whichNote == 5) {
                opposingButton = GameManager.instance.crashPos1;
            }
        } 
        // "Player2" = 7
        else if (gameObject.layer == 7) 
        {
            if (whichNote == 1) {
                opposingButton = GameManager.instance.P2Button1;
                key = GameManager.instance.P2Key1;
            } else if (whichNote == 2) {
                opposingButton = GameManager.instance.P2Button2;
                key = GameManager.instance.P2Key2;
            } else if (whichNote == 3) {
                opposingButton = GameManager.instance.P2Button3;
                key = GameManager.instance.P2Key3;
            } else if (whichNote == 4) {
                opposingButton = GameManager.instance.P2Button4;
                key = GameManager.instance.P2Key4;
            } else if (whichNote == 5) {
                opposingButton = GameManager.instance.crashPos2;
            }
        }
        origin = this.transform.position;
        destination = opposingButton.transform.position;
        noteTimer = GameManager.instance.metronomeTimer;
        if (whichNote == 5) {
            noteDamage = GameManager.instance.crashNoteDamage;
        } else {
            Invoke("GetCurrentDamage", ((1f / GameManager.instance.beatTempo) / 2f));
        }
    }

    void Update()
    {
        if (!GameManager.instance.gameEnd) {
            if (Input.GetKeyDown(key) || 
                (whichNote == 5 && 
                (Input.GetKey(GameManager.instance.P1Key1) && Input.GetKey(GameManager.instance.P1Key2) && Input.GetKey(GameManager.instance.P1Key3) && Input.GetKey(GameManager.instance.P1Key4) && gameObject.layer == 6) || 
                (Input.GetKey(GameManager.instance.P2Key1) && Input.GetKey(GameManager.instance.P2Key2) && Input.GetKey(GameManager.instance.P2Key3) && Input.GetKey(GameManager.instance.P2Key4) && gameObject.layer == 7)))
            {
                if (canBePressed) 
                {
                    hit = true;

                    gameObject.SetActive(false);

                    // "Player1" = 6
                    if (gameObject.layer == 6)
                    {
                        if (whichNote == 1) {
                            GameManager.instance.P1hihat.Stop();
                            GameManager.instance.P1hihat.Play();
                        } else if (whichNote == 2) {
                            GameManager.instance.P1snare.Stop();
                            GameManager.instance.P1snare.Play();
                        } else if (whichNote == 3) {
                            GameManager.instance.P1snare2.Stop();
                            GameManager.instance.P1snare2.Play();
                        } else if (whichNote == 4) {
                            GameManager.instance.P1kick.Stop();
                            GameManager.instance.P1kick.Play();
                        } else if (whichNote == 5) {
                            GameManager.instance.P1crash.Stop();
                            GameManager.instance.P1crash.Play();
                        } 
                    } 
                    // "Player2" = 7
                    else if (gameObject.layer == 7) 
                    {
                        if (whichNote == 1) {
                            GameManager.instance.P2hihat.Stop();
                            GameManager.instance.P2hihat.Play();
                        } else if (whichNote == 2) {
                            GameManager.instance.P2snare.Stop();
                            GameManager.instance.P2snare.Play();
                        } else if (whichNote == 3) {
                            GameManager.instance.P2snare2.Stop();
                            GameManager.instance.P2snare2.Play();
                        } else if (whichNote == 4) {
                            GameManager.instance.P2kick.Stop();
                            GameManager.instance.P2kick.Play();
                        } else if (whichNote == 5) {
                            GameManager.instance.P2crash.Stop();
                            GameManager.instance.P2crash.Play();
                        } 
                    }
                    
                    if (perfect)
                    {
                        GameManager.instance.PerfectNoteHit();
                        // perfect hit 
                        // "Player1" = 6
                        if (gameObject.layer == 6)
                        {
                            if (whichNote == 5) {
                                GameManager.instance.p1Health -= GameManager.instance.crashChipDamage;
                            }
                            GameManager.instance.p1DefenseUltimate += 6f;
                        } 
                        // "Player2" = 7
                        else if (gameObject.layer == 7) 
                        {
                            if (whichNote == 5) {
                                GameManager.instance.p2Health -= GameManager.instance.crashChipDamage;
                            }
                            GameManager.instance.p2DefenseUltimate += 6f;
                        }
                    } 
                    else 
                    {
                        // okay hit
                        GameManager.instance.NoteHit();
                        // "Player1" = 6
                        if (gameObject.layer == 6)
                        {
                            if (whichNote == 5) {
                                GameManager.instance.p1Health -= GameManager.instance.crashChipDamage;
                            } else if (!(GameManager.instance.p1Health <= (noteDamage * GameManager.instance.chipPercentage))) {
                                GameManager.instance.p1Health -= (noteDamage * GameManager.instance.chipPercentage);
                            }
                            GameManager.instance.p1DefenseUltimate += 3f;
                        } 
                        // "Player2" = 7
                        else if (gameObject.layer == 7) 
                        {
                            if (whichNote == 5) {
                                GameManager.instance.p2Health -= GameManager.instance.crashChipDamage;
                            } else if (!(GameManager.instance.p2Health <= (noteDamage * GameManager.instance.chipPercentage))) {
                                GameManager.instance.p2Health -= (noteDamage * GameManager.instance.chipPercentage);
                            }
                            GameManager.instance.p2DefenseUltimate += 3f;
                        }
                    }
                }
            }
            if (GameManager.instance.interrupt) {
                gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.gameEnd) {
            if (noteTimer < ((1f / GameManager.instance.beatTempo) * GameManager.instance.barAmount)) {
                transform.position = Vector3.Lerp(origin, destination, noteTimer / ((1f / GameManager.instance.beatTempo) * GameManager.instance.barAmount));
                noteTimer += Time.deltaTime;
            } else {
                // speed = distance / time; distance = origin - destination; time = ((1 / tempo) * bars) / framerate.
                transform.position -= new Vector3((((origin.x - destination.x) / ((1f / GameManager.instance.beatTempo) * GameManager.instance.barAmount)) / 50f), 0f ,0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }

        if (other.tag == "Perfect")
        {
            perfect = true;
        }

        if (other.tag == "UnPerfect")
        {
            perfect = false;
        }

        if (other.tag == "Damage")
        {
            DestroyObjectDelayed();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }

    private void DestroyObjectDelayed()
    {
        Destroy(gameObject, 0.1f);
    }

    private void GetCurrentDamage()
    {
        noteDamage = GameManager.instance.currentNoteDamage;
    }
}