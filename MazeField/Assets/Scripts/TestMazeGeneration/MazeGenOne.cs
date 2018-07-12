using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenOne : MonoBehaviour {

    public bool Generate = true;    

    public int CountX = 5, CountY = 5;

    public float Width = 10.0f, Height = 10.0f;

    List<Cell> grid = new List<Cell>();
    Queue<Cell> CellStack;

    public int GenSpeed = 1;

    Cell current;
    Cell end;

    bool isDone = false;

    private void GenGrid()
    {
        int CurrentX = 0;
        isDone = false;

        end = null;

        while (CurrentX < CountX)
        {
            int CurrentY = 0;

            while (CurrentY < CountY)
            {
                Cell newCell = new Cell(CurrentX, CurrentY);

                grid.Add(newCell);
                CurrentY++;
            }
            CurrentX++;
        }

        int checkX = Random.Range(0, CountX), checkY = Random.Range(0, CountY);

        current = grid[index(checkX, checkY)];

        end = current;

        current.Visited = true;

        CellStack = new Queue<Cell>();
        CellStack.Enqueue(current);
    }

    float Rate = 0.00f;

    float timer = 0.0f;
    
    void Update ()
    {
        if (Generate)
        {
            grid.Clear();
            GenGrid();
            Generate = false;
        }
        if (!isDone)
        {

            timer += Time.deltaTime;

            if (timer > Rate)
            {
                for (int i = 0; i < GenSpeed; i++)
                {
                    step();
                    if (isDone)
                    {
                        int checkX = Random.Range(0, CountX), checkY = Random.Range(0, CountY);
                        //end = grid[index(checkX, checkY)];
                        break;
                    }
                }

                timer = 0;
            }
        }

        float W = Width / (float)CountX;
        float H = Height / (float)CountY;
        foreach (Cell c in grid)
        {
            c.Draw(W,H, transform.position);
        }

        current.DrawCurrent(W, H, transform.position);

        if (end != null)
        {
            end.DrawEnd(W, H, transform.position);
        }
	}

    void step()
    {
        Cell Next = GetNeighbor(current);

        if (Next != null)
        {
            Next.Visited = true;


            RemoveWalls(current, Next);

            current = Next;

            CellStack.Enqueue(current);
        }
        else
        {
            if (CellStack.Count > 0)
            {
                current = CellStack.Dequeue();
            }
            else
                isDone = true;
        }
    }

    void RemoveWalls(Cell A, Cell B)
    {
        int RL = A.X - B.X;
        int UD = A.Y - B.Y;

        if (RL == 1)
        {
            A.W_Left = false;
            B.W_Right = false;
        }
        else if (RL == -1)
        {
            B.W_Left = false;
            A.W_Right = false;
        }
        else
        {
            if (UD == 1)
            {
                A.W_Bottom = false;
                B.W_Top = false;
            }
            else
            {
                B.W_Bottom = false;
                A.W_Top = false;
            }
        }
    }

    int index(int x, int y)
    {
        return (x * CountY) + y;
    }

    Cell GetNeighbor(Cell cell)
    {
        List<Cell> Neighbors = GetUnvisitedNeighbors(cell);

        if (Neighbors.Count > 0)
        {
            int i = Random.Range(0, Neighbors.Count);

            return Neighbors[i];
        }
        else
            return null;
    }

    List<Cell> GetUnvisitedNeighbors(Cell cell)
    {
        List<Cell> Neighbors = new List<Cell>();

        if (cell.X + 1 < CountX)
        {
            Cell toAdd = grid[index(cell.X + 1, cell.Y)];
            if (!toAdd.Visited)
                Neighbors.Add(toAdd);
        }
        if (cell.X - 1 >= 0)
        {
            Cell toAdd = grid[index(cell.X - 1, cell.Y)];
            if (!toAdd.Visited)
                Neighbors.Add(toAdd);
        }
        if (cell.Y + 1 < CountY)
        {
            Cell toAdd = grid[index(cell.X, cell.Y + 1)];
            if (!toAdd.Visited)
                Neighbors.Add(toAdd);
        }
        if (cell.Y - 1 >= 0)
        {
            Cell toAdd = grid[index(cell.X, cell.Y - 1)];
            if (!toAdd.Visited)
                Neighbors.Add(toAdd);
        }

        return Neighbors;
    }
}
