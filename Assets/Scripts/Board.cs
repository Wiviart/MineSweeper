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
        SetNumber();
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
                cells[x, y].transform.parent = transform;
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

                cells[x, y].type = Cell.Type.Number;
                cells[x, y].index = count;
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

    public void RevealCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return;
        if (cells[x, y].isFlagged) return;

        Cell.Type type = cells[x, y].type;

        switch (type)
        {
            case Cell.Type.Number:
                {
                    FloodCell(x, y);
                    break;
                }
            case Cell.Type.Mine:
                {
                    Explode(x, y);
                    break;
                }
        }
    }

    public void FlagCell(int x, int y)
    {
        if (cells[x, y].isRevealed) return;

        if (cells[x, y].isFlagged)
        {
            cells[x, y].spriteRdr.sprite = cellSpecialSpt[0];
            cells[x, y].isFlagged = false;
        }
        else
        {
            cells[x, y].spriteRdr.sprite = cellSpecialSpt[2];
            cells[x, y].isFlagged = true;
        }
    }
    public void FloodCell(int x, int y)
    {
        if (cells[x, y].isRevealed) return;

        if (cells[x, y].isFlagged || cells[x, y].type == Cell.Type.Mine) return;

        cells[x, y].spriteRdr.sprite = cellNumberSpt[cells[x, y].index];
        cells[x, y].isRevealed = true;

        if (cells[x, y].index == 0)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || y + j < 0 || x + i >= width || y + j >= height) continue;

                    if (x + i == x && y + j == y) continue;

                    FloodCell(x + i, y + j);
                }
            }

        }
    }

    void SetCellStatus(int x, int y)
    {
        Cell.Type type = cells[x, y].type;

        switch (type)
        {
            case Cell.Type.Number:
                {
                    if (cells[x, y].index == 0)

                        cells[x, y].spriteRdr.sprite = cellNumberSpt[cells[x, y].index];
                    break;
                }
            case Cell.Type.Mine:
                {
                    cells[x, y].spriteRdr.sprite = cellSpecialSpt[1];
                    break;
                }
        }
    }

    void Explode(int x, int y)
    {
        foreach (Cell c in cells)
        {
            if (c.type == Cell.Type.Mine)
            {
                if (c.isFlagged) continue;

                c.spriteRdr.sprite = cellSpecialSpt[1];
                c.isRevealed = true;
            }
        }

        cells[x, y].spriteRdr.sprite = cellSpecialSpt[3];

        GameManager.instance.EndgameTrigger();
    }

    public bool AllNumberCellReveal()
    {
        int count = mine;

        foreach (Cell c in cells)
        {
            if (c.type == Cell.Type.Mine && c.isFlagged) count--;
        }

        if (count == 0) return true;

        foreach (Cell c in cells)
        {
            if (c.type == Cell.Type.Mine) continue;
            if (!c.isRevealed) return false;
        }
        return true;
    }
}
