using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /**********************************************************************************************************/

    [SerializeField][Range(1, 7)] int difficult;
    void Start()
    {
        CenterCamera();
        SetMineNumber(difficult);
        board.SetUpNewGame();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) RevealCell();
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
        Vector2 position = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log(position);

        Cell cell = board.GetCell(position.x, position.y);


    }
}
