using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuInGame : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject pauseMenu;
    public GameObject endMenu;
    public GameObject winMenu;
    public TextMeshProUGUI announce;
    public GameObject winEffect;
    public GameObject loseEffect;
    public TextMeshProUGUI mine;

    void Start()
    {
        GameManager.instance.endgameAction += EndMenu;
        GameManager.instance.wingameAction += WinMenu;

        SetUpNewGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu();

        DisplayMine();
    }
    void SetUpNewGame()
    {
        inGameMenu.SetActive(true);
        pauseMenu.SetActive(false);

        winEffect.SetActive(false);
        winEffect.transform.position = new Vector3(Camera.main.transform.position.x, -5, 0);

        loseEffect.SetActive(false);
        loseEffect.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 1, 0);

        announce.enabled = false;
    }

    public void PauseMenu()
    {
        Time.timeScale = 0f;

        inGameMenu.SetActive(false);
        pauseMenu.SetActive(true);

        GameManager.instance.StopInput();
    }
    void EndMenu()
    {
        inGameMenu.SetActive(false);
        loseEffect.SetActive(true);
        loseEffect.GetComponentInChildren<ParticleSystem>().transform.localScale = Vector3.one * Camera.main.orthographicSize / 10;
        loseEffect.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + Camera.main.orthographicSize, 0);

        announce.text = $"Game Over!";

        StartCoroutine(ShowAnnounce(2f));
    }

    void WinMenu()
    {
        inGameMenu.SetActive(false);
        winEffect.SetActive(true);

        List<ParticleSystem> effects = new List<ParticleSystem>();

        ParticleSystem[] x = winEffect.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem e in x)
        {
            ParticleSystem y = e.GetComponentInChildren<ParticleSystem>();
            effects.Add(e);
            effects.Add(y);
        }

        foreach (ParticleSystem e in effects)
        {
            e.transform.localScale = Vector3.one * Camera.main.orthographicSize / 10;
        }
        
        winEffect.transform.localScale = Vector3.one * Camera.main.orthographicSize / 10;
        winEffect.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - Camera.main.orthographicSize * 1.5f, 0);

        announce.text = $"You're win!";

        StartCoroutine(ShowAnnounce(3f));
    }

    void DisplayMine()
    {
        int x = GameManager.instance.GetMineNumber();
        mine.text = $"{x}";
    }
    public void ResumeButton()
    {
        GameManager.instance.StartInput();
        inGameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NewgameButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public AnimationCurve curve;
    IEnumerator ShowAnnounce(float time)
    {
        announce.enabled = true;

        float t = 0;
        while (t < 1)
        {
            announce.transform.localScale = Vector3.one * curve.Evaluate(t) * 7;
            t += Time.deltaTime;
            yield return null;
        }

        // yield return new WaitForSecondsRealtime(time);

        // while (announce.color.a > 0)
        // {
        //     Color color = announce.color;
        //     color.a -= 0.01f;
        //     announce.color = color;
        //     yield return new WaitForSecondsRealtime(0.1f);
        //     Debug.Log(announce.color.a);

        // }

        // announce.enabled = false;
    }
}
