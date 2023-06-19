using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Board : MonoBehaviour
{
    [SerializeField] Sprite[] cellNumberSpt;
    [SerializeField] Sprite[] cellSpecialSpt;
    [SerializeField] Cell cellPrefab;
    int width, height, mine;
    Cell[,] cells;
    Cell[,] coverCells;
    void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpNewGame()
    {
        GetBoardSize();
        DrawBoard();
        SetMine();
    }
    void GetBoardSize()
    {
        width = GameManager.instance.GetWidth();
        height = GameManager.instance.GetHeight();
        mine = GameManager.instance.GetMineNumber();
        cells = new Cell[width, height];
    }

    /**********************************************************************************************************/

    void DrawBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);
                cells[x, y].type = Cell.Type.Unknown;
            }
        }
    }
    /**********************************************************************************************************/

    void SetMine()
    {
        int c = 0;

        while (c < mine)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (cells[x, y].type == Cell.Type.Mine) continue;

            cells[x, y].type = Cell.Type.Mine;
            cells[x, y].spriteRdr.sprite = cellSpecialSpt[1];
            c++;
        }
    }

    /**********************************************************************************************************/

    void SetNumber()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (CheckMine(x, y)) continue;

                int count = 0;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (CheckMine(x + i, y + j)) count++;
                    }
                }

                CreateCell(x, y, cellNumberSpt[count], Cell.Type.Number);
            }
        }
    }

    /**********************************************************************************************************/

    void CreateCell(int x, int y, Sprite sprite, Cell.Type type)
    {
        cells[x, y] = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);
        cells[x, y].GetComponent<SpriteRenderer>().sprite = sprite;
        cells[x, y].type = type;
        cells[x, y].transform.parent = transform;
    }

    /**********************************************************************************************************/

    bool CheckMine(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return false;

        if (cells[x, y] == null) return false;

        if (cells[x, y].type == Cell.Type.Mine)
            return true;
        else
            return false;
    }

    /**********************************************************************************************************/
    /**********************************************************************************************************/

    public Cell GetCell(float x, float y)
    {
        return cells[(int)x, (int)y];
    }
}
