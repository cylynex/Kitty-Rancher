using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerActions : MonoBehaviour {

    [SerializeField] GameObject net;
    [SerializeField] float netTimerMax = 1f;
    [SerializeField] float netTimer = 0;
    [SerializeField] bool netDeployed = false;
    [SerializeField] TMP_Text score;
    [SerializeField] int numberCats = 0;
    [SerializeField] int numberCatsCaptured = 0;

    [Header("Timer")]
    [SerializeField] TMP_Text timerDisplay;
    [SerializeField] float timerStart;
    [SerializeField] float timer;

    [Header("Sounds")]
    AudioSource audioSource;
    [SerializeField] AudioClip meow;

    [Header("UI")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text gameOverText;

    private void Start() {
        Time.timeScale = 1;
        GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
        numberCats = cats.Length;
        UpdateCatScore();
        audioSource = GetComponent<AudioSource>();
        timer = timerStart;
        UpdateTimer();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightControl)) {
            if (!netDeployed) {
                net.SetActive(true);
                netTimer = netTimerMax;
                netDeployed = true;
            }
        }

        if (netDeployed) {
            netTimer -= Time.deltaTime;
            if (netTimer <= 0) {
                RetractNet();
            }
        }

        UpdateTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (netDeployed) {
            if (collision.gameObject.tag == "Cat") {
                Destroy(collision.gameObject);
                numberCatsCaptured++;
                UpdateCatScore();
                audioSource.PlayOneShot(meow);
                RetractNet();
            }
        }
    }

    void RetractNet() {
        netDeployed = false;
        Invoke("HideNet", 0.5f);
    }

    void HideNet() {
        net.SetActive(false);
    }

    void UpdateCatScore() {
        score.text = numberCatsCaptured.ToString() + " / " + numberCats.ToString();
        if (numberCatsCaptured == numberCats) {
            EndGame("You won!  You captured all the kitties.");
        }
    }

    void UpdateTimer() {
        timer -= Time.deltaTime;
        timerDisplay.text = Mathf.RoundToInt(timer).ToString();
        if (timer <= 0) {
                EndGame("Game Over.  You lost.");            
        }
    }

    void EndGame(string endGameText) {
        Time.timeScale = 0;
        gameOverText.text = endGameText;
        gameOverScreen.SetActive(true);
    }

}
