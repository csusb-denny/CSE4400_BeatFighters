using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Debug")]
    public float metronomeTimer;
    public float flashTimer;
    public int barCounter;
    public int displayBarCounter;
    // 0: No one's, 1: Player 1, 2: Player 2
    public int whoseTurn = 0;
    public int prevTurn;
    public bool canCreate;
    public bool isPlaying;
    public bool gameEnd;
    public int buttonsPressed;
    public bool interrupt;

    [Header("Keybinds")]
    public KeyCode P1Key1;
    public KeyCode P1Key2;
    public KeyCode P1Key3;
    public KeyCode P1Key4;
    public KeyCode P1Key5;
    public KeyCode P2Key1;
    public KeyCode P2Key2;
    public KeyCode P2Key3;
    public KeyCode P2Key4;
    public KeyCode P2Key5;

    [Header("Match Settings")]
    public float flashBrightness;
    public float beatTempo;
    public int barAmount;

    [Header("Health")]
    public float p1Health = 100f;
    public float p2Health = 100f;

    [Header("Damage")]
    public float baseDamage;
    public float chipPercentage;
    public float currentNoteDamage;
    public float crashNoteDamage;
    public float crashChipDamage;

    [Header("References")]
    public AudioSource music;
    public AudioSource metronomeSound;

    public AudioSource P1kick;
    public AudioSource P1hihat;
    public AudioSource P1snare;
    public AudioSource P1snare2;
    public AudioSource P1miss;
    public AudioSource P1crash;

    public AudioSource P2kick;
    public AudioSource P2hihat;
    public AudioSource P2snare;
    public AudioSource P2snare2;
    public AudioSource P2miss;
    public AudioSource P2crash;

    public GameObject P1Button1;
    public GameObject P1Button2;
    public GameObject P1Button3;
    public GameObject P1Button4;
    public GameObject P2Button1;
    public GameObject P2Button2;
    public GameObject P2Button3;
    public GameObject P2Button4;
    public Image P1Flash;
    public Image P2Flash;
    public Image p1hpBar;
    public Image p2hpBar;
    public Image p1TurnIcon;
    public Image p2TurnIcon;
    public Sprite offTurn;
    public Sprite defTurn;
    public TMP_Text barCounterText;
    public TMP_Text victoryText;
    
    [Header("Ultimate meters")]
    public Image p1OffenseUltMeter;
    public Image p1DefenseUltMeter;
    public Image p2OffenseUltMeter;
    public Image p2DefenseUltMeter;

    public GameObject crashNote1;
    public GameObject crashPos1;
    public GameObject crashNote2;
    public GameObject crashPos2;

    public float p1OffenseUltimate = 0f;
    public float p1DefenseUltimate = 0f;
    public float p2OffenseUltimate = 0f;
    public float p2DefenseUltimate = 0f;
    public float maxOffenseUltimate = 100f;
    public float maxDefenseUltimate = 100f;


    public static GameManager instance;

    void Awake()
    {
        instance = this;
        beatTempo = (beatTempo / 60f) * 2f;
        P1Flash.color = Color.clear;
        P2Flash.color = Color.clear;
        metronomeTimer = 0f;
        barCounter = 1;
        gameEnd = false;
    }

    void Update()
    {
        if (!gameEnd) {
            // Metronome Stuff
            if (!isPlaying)
            {
                if (Input.anyKeyDown)
                {
                    isPlaying = true;

                    music.Play();
                }
            } else {
                // Timing
                if (metronomeTimer > (1f / beatTempo)) 
                {
                    metronomeTimer = 0f;
                    if (barCounter < barAmount - 1) {
                        barCounter += 1;
                    } else {
                        barCounter = 0;
                        displayBarCounter = 0;

                        // Turn Determiner
                        if (whoseTurn == 0) {
                            if (prevTurn == 1) {
                                whoseTurn = 2;
                            } else if (prevTurn == 2) {
                                whoseTurn = 1;                        
                            }
                        } else {
                            prevTurn = whoseTurn;
                            whoseTurn = 0;
                        }
                    }
                } 
                else 
                {
                    metronomeTimer += Time.deltaTime;
                }
                
                // Note Timing
                if (metronomeTimer != 0) {
                    canCreate = true;
                } else {
                    canCreate = false;
                    buttonsPressed = 0;
                }

                // Damage Balancing
                if (buttonsPressed != 0) currentNoteDamage = baseDamage / buttonsPressed;

                // Metronome Flash
                if (flashTimer > ((1f / beatTempo) * 2f)) {
                    flashTimer = 0f;
                    displayBarCounter += 1;
                    metronomeSound.Stop();
                    metronomeSound.Play();
                }
                else 
                {
                    flashTimer += Time.deltaTime;
                }

                if (whoseTurn == 1) {
                    p1TurnIcon.sprite = offTurn;
                    p2TurnIcon.sprite = defTurn;
                } else if (whoseTurn == 2) {
                    p2TurnIcon.sprite = offTurn;
                    p1TurnIcon.sprite = defTurn;
                }

                if (whoseTurn == 1) {
                    P1Flash.color = Color.Lerp(Color.clear, new Color(1f, 0.5f, 0f, flashBrightness), 1f - (flashTimer / ((1f / beatTempo) * 2)));
                    P2Flash.color = Color.clear;
                } else if (whoseTurn == 2) {
                    P2Flash.color = Color.Lerp(Color.clear, new Color(1f, 0.5f, 0f, flashBrightness), 1f - (flashTimer / ((1f / beatTempo) * 2)));
                    P1Flash.color = Color.clear;
                } else if (whoseTurn == 0) {
                    if (prevTurn == 1)  {
                        P2Flash.color = Color.Lerp(Color.clear, new Color(0f, 0.75f, 0.75f, flashBrightness / 10f), 1f - (flashTimer / ((1f / beatTempo) * 2)));
                        P1Flash.color = Color.clear;
                    } else if (prevTurn == 2) {
                        P1Flash.color = Color.Lerp(Color.clear, new Color(0f, 0.75f, 0.75f, flashBrightness / 10f), 1f - (flashTimer / ((1f / beatTempo) * 2)));
                        P2Flash.color = Color.clear;
                    }
                }
            }

            // Health Bar Stuff
            p1hpBar.fillAmount = p1Health / 100f;
            p2hpBar.fillAmount = p2Health / 100f;
            int temp = (barAmount / 2) - displayBarCounter;
            barCounterText.text = temp.ToString();

            // Ultimate bar visual update method
            p1OffenseUltMeter.fillAmount = p1OffenseUltimate / maxOffenseUltimate;
            p1DefenseUltMeter.fillAmount = p1DefenseUltimate / maxDefenseUltimate;

            p2OffenseUltMeter.fillAmount = p2OffenseUltimate / maxOffenseUltimate;
            p2DefenseUltMeter.fillAmount = p2DefenseUltimate / maxDefenseUltimate;

            // Ultimate bar effect
            // p1 interrupt
            if (p1DefenseUltimate >= maxDefenseUltimate && (whoseTurn == 2 || ((whoseTurn == 0) && prevTurn == 2))) {
                if (Input.GetKey(P1Key5)) {
                    interrupt = true; 
                    whoseTurn = 1;
                    metronomeTimer = 0f;
                    flashTimer = 0f;
                    barCounter = 0;
                    displayBarCounter = 0;
                    p1DefenseUltimate = 0f;
                    Invoke("interruptReset", 0.1f);
                }
            }
            // p2 interrupt
            if (p2DefenseUltimate >= maxDefenseUltimate && (whoseTurn == 1 || ((whoseTurn == 0) && prevTurn == 1))) {
                if (Input.GetKey(P2Key5)) {
                    interrupt = true; 
                    whoseTurn = 2;
                    metronomeTimer = 0f;
                    flashTimer = 0f;
                    barCounter = 0;
                    displayBarCounter = 0;
                    p2DefenseUltimate = 0f;
                    Invoke("interruptReset", 0.1f);
                }
            }
            // p1 crash
            if (p1OffenseUltimate >= maxDefenseUltimate && (whoseTurn == 1)) {
                if (Input.GetKey(P1Key1) && Input.GetKey(P1Key2) && Input.GetKey(P1Key3) && Input.GetKey(P1Key4)) {
                    Instantiate(crashNote1, crashPos1.transform.position, Quaternion.identity);
                    P1crash.Stop();
                    P1crash.Play();
                    p1OffenseUltimate = 0f;
                }
            } 
            // p2 crash
            if (p2OffenseUltimate >= maxDefenseUltimate && (whoseTurn == 2)) {
                if (Input.GetKey(P2Key1) && Input.GetKey(P2Key2) && Input.GetKey(P2Key3) && Input.GetKey(P2Key4)) {
                    Instantiate(crashNote2, crashPos2.transform.position, Quaternion.identity);
                    P2crash.Stop();
                    P2crash.Play();
                    p2OffenseUltimate = 0f;
                }
            } 



            // End game condition
            if (p1Health < 0f) {
                victoryText.text = "P2 Win!";
                gameEnd = true;
            } else if (p2Health < 0f) {
                victoryText.text = "P1 Win!";
                gameEnd = true;
            } else {
                victoryText.text = "";
            }
        } else {
            // Reset game
            if (Input.GetKeyDown("escape")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PerfectNoteHit()
    {
        Debug.Log("Perfect");
    }

    public void NoteHit()
    {
        Debug.Log("Okay");
    }

    public void NoteMiss()
    {
        Debug.Log("Miss");
    }

    public void interruptReset()
    {
        interrupt = false;
    }
}
