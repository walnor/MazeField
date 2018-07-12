using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenTwo : MonoBehaviour {

    public GameObject m_RoomOBJ;

    public float m_RoomSize = 10f;

    public int m_RoomCountX = 10;
    public int m_RoomCountY = 10;

    Room[,] m_Rooms;
    Room current;

    bool isDone = false;

    Queue<Room> RoomStack;

    public GameObject m_Enemy;
    public GameObject m_Player;
    public DisplayAttackLog m_log;

    public float m_EnemySpawnrate = 0.3f;
    // Use this for initialization
    void Start ()
    {
        RoomStack = new Queue<Room>();

        m_Rooms = new Room[m_RoomCountX, m_RoomCountY];
        for (int i = 0; i < m_RoomCountX; i++)
        {
            for (int j = 0; j < m_RoomCountY; j++)
            {
                GameObject obj = Instantiate(m_RoomOBJ,transform);
                obj.transform.localPosition = new Vector3(m_RoomSize * i, m_RoomSize * j, 0);

                m_Rooms[i,j] = obj.GetComponent<Room>();
                m_Rooms[i, j].i = i;
                m_Rooms[i, j].j = j;

                SpawnEnemy(i, j);
            }
        }

        current = m_Rooms[0, 0];

        while (!isDone)
            step();

        {
            for (int i = 0; i < m_RoomCountX; i++)
            {
                float ChanceI = 50f - ((50 / m_RoomCountX) * i);
                for (int j = 0; j < m_RoomCountY; j++)
                {
                    float ChanceJ = 50f - ((50 / m_RoomCountY) * j);
                    float ran = Random.Range(0, 100);

                    if (ran < (ChanceI + ChanceJ))
                    {
                        m_Rooms[i, j].m_Visited = false;
                    }
                }
            }
            isDone = false;

            current = m_Rooms[0, 0];

            while (!isDone)
                step();
        }
	}

    void SpawnEnemy(int i, int j)
    {
        if (Random.Range(0f, 1f) < m_EnemySpawnrate)
        {
            Vector3 pos = m_Rooms[i, j].transform.position;

            GameObject NE = Instantiate(m_Enemy, transform.parent);
            Enemy E = NE.GetComponent<Enemy>();
            E.m_attLog = m_log;
            E.m_Target = m_Player;
            NE.transform.position = pos;

            StatFactory SF = NE.GetComponent<StatFactory>();
            SF.MakeRandom(150, 25, 10, 25, 25, 25, 25, 25, 1);
            E.Init();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    void step()
    {
        Room Next = GetNeighbor(current);

        if (Next != null)
        {
            Next.m_Visited = true;

            RemoveWalls(current, Next);

            current = Next;

            RoomStack.Enqueue(current);
        }
        else
        {
            if (RoomStack.Count > 0)
            {
                current = RoomStack.Dequeue();
            }
            else
                isDone = true;
        }
    }

    void RemoveWalls(Room A, Room B)
    {
        int RL = A.i - B.i;
        int UD = A.j - B.j;

        if (RL == 1)
        {
            A.m_Left.SetActive(false);
            B.m_Right.SetActive(false);
        }
        else if (RL == -1)
        {
            A.m_Right.SetActive(false);
            B.m_Left.SetActive(false);
        }
        else
        {
            if (UD == 1)
            {
                A.m_Bot.SetActive(false);
                B.m_Top.SetActive(false);
            }
            else
            {
                A.m_Top.SetActive(false);
                B.m_Bot.SetActive(false);
            }
        }
    }

    Room GetNeighbor(Room cell)
    {
        List<Room> Neighbors = GetUnvisitedNeighbors(cell);

        if (Neighbors.Count > 0)
        {
            int i = Random.Range(0, Neighbors.Count);

            return Neighbors[i];
        }
        else
            return null;
    }


    List<Room> GetUnvisitedNeighbors(Room cell)
    {
        List<Room> Neighbors = new List<Room>();

        if (cell.i + 1 < m_RoomCountX)
        {
            Room toAdd = m_Rooms[cell.i + 1, cell.j];
            if (!toAdd.m_Visited)
                Neighbors.Add(toAdd);
        }
        if (cell.i - 1 >= 0)
        {
            Room toAdd = m_Rooms[cell.i - 1, cell.j];
            if (!toAdd.m_Visited)
                Neighbors.Add(toAdd);
        }
        if (cell.j + 1 < m_RoomCountY)
        {
            Room toAdd = m_Rooms[cell.i, cell.j + 1];
            if (!toAdd.m_Visited)
                Neighbors.Add(toAdd);
        }
        if (cell.j - 1 >= 0)
        {
            Room toAdd = m_Rooms[cell.i, cell.j - 1];
            if (!toAdd.m_Visited)
                Neighbors.Add(toAdd);
        }

        return Neighbors;
    }
}
