using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    public int gatheredJewelry = 0;
    public int maxPossibleJewelry = 0;
    public bool canMove = true;

    [SerializeField] private List<Sprite> bagSprites;
    [SerializeField] private Image bagSprite;
    [SerializeField] private ParticleSystem coinPS;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject victoryText;

    [SerializeField] private GameObject endGameButtons;

    private bool levelCompleted = false;
    private bool playerLost = false;

    public void fillingBag(int cost)
    {
        if (levelCompleted || playerLost)
            return;

        gatheredJewelry += cost;

        if (coinPS != null)
            coinPS.Play();

        float ratio = maxPossibleJewelry > 0 ? (float)gatheredJewelry / maxPossibleJewelry : 0f;

        if (ratio >= 1f)
        {
            levelCompleted = true;
            StartCoroutine(LevelCompletion());

            if (bagSprite != null && bagSprites.Count > 0)
                bagSprite.sprite = bagSprites[0];
        }
        else if (ratio >= 0.85f)
            SetBagSprite(1);
        else if (ratio >= 0.70f)
            SetBagSprite(2);
        else if (ratio >= 0.50f)
            SetBagSprite(3);
        else if (ratio >= 0.25f)
            SetBagSprite(4);
        else if (ratio >= 0.10f)
            SetBagSprite(5);
        else
            SetBagSprite(6);
    }

    private void SetBagSprite(int index)
    {
        if (bagSprite != null && bagSprites != null && bagSprites.Count > index)
            bagSprite.sprite = bagSprites[index];
    }

    public void Lost()
    {
        if (levelCompleted || playerLost)
            return;

        playerLost = true;
        StartCoroutine(Lose());
    }

    private IEnumerator Lose()
    {
        if (loseText != null)
            loseText.SetActive(true);

        canMove = false;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LevelCompletion()
    {
        canMove = false;

        if (levelText != null)
            levelText.SetActive(true);

        yield return new WaitForSeconds(3f);

        if (levelText != null)
            levelText.SetActive(false);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            if (AudioManager.instance != null)
                AudioManager.instance.sceneindex = 2;

            SceneManager.LoadScene(2);
        }
        else if (currentSceneIndex == 2)
        {
            if (victoryText != null)
                victoryText.SetActive(true);

            if (endGameButtons != null)
                endGameButtons.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        if (AudioManager.instance != null)
            AudioManager.instance.sceneindex = 0;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}