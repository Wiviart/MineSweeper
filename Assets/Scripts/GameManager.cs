using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /**********************************************************************************************************/

    public static GameManager instance;

    void Awake()
    {
        instance = this;

        GetGameComponents();
    }


    Board board;
    void GetGameComponents()
    {
        board = FindObjectOfType<Board>();
    }

    /**********************************************************************************************************/

    public Action endgameAction;
    public void EndgameTrigger()
    {
        endgameAction?.Invoke();
    }

    public Action wingameAction;
    public void WingameTrigger()
    {
        wingameAction?.Invoke();
    }

    /**********************************************************************************************************/

    [SerializeField][Range(1, 7)] int difficult;
    void Start()
    {
        SetSizeAndDifficult();
        CenterCamera();
        SetMineNumber(difficult);
        board.SetUpNewGame();

        endgameAction += StopInput;
        wingameAction += StopInput;
    }

    public void SetSizeAndDifficult()
    {
        difficult = Difficult.instance.difficult;
        width = Difficult.instance.width;
        height = Difficult.instance.height;
    }
    public bool canInput = true;
    void Update()
    {
        if (!canInput) return;

        if (Input.GetMouseButtonDown(0)) RevealCell();
        if (Input.GetMouseButtonDown(1)) FlagCell();
        if (Input.GetKeyDown(KeyCode.Q)) WingameTrigger();


        if (board.AllNumberCellReveal())
        {
            Debug.Log("Win");
            WingameTrigger();
        }
    }
    void CenterCamera()
    {
        Camera.main.transform.position = new Vector3(width - 1, height / 2, -10);
        Camera.main.orthographicSize = Mathf.Max(width, height) * 0.6f;
    }

    /**********************************************************************************************************/
    /**********************************************************************************************************/

    [SerializeField] int height;
    public int GetHeight()
    {
        return height;
    }

    /**********************************************************************************************************/

    [SerializeField] int width;
    public int GetWidth()
    {
        return width;
    }

    /**********************************************************************************************************/

    [SerializeField] int mine;
    void SetMineNumber(int difficult)
    {
        mine = (int)(height * width * difficult) / 10;
    }
    public int GetMineNumber()
    {
        return mine;
    }

    /**********************************************************************************************************/
    /**********************************************************************************************************/

    void RevealCell()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        board.RevealCell(x, y);
    }
    void FlagCell()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        board.FlagCell(x, y);
    }

    public void StopInput()
    {
        canInput = false;
    }
    public void StartInput()
    {
        canInput = true;
    }
    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(0);
    }
}
