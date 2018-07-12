using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cell
{
    public int X,Y;

    public bool W_Top = true, W_Left = true, W_Right = true, W_Bottom = true;

    public bool Visited = false;    

    public Cell(int i, int j)
    {
        X = i;
        Y = j;
    }

    public void Draw(float w, float h, Vector3 Offset)
    {
        Color color = Visited ? Color.magenta : Color.white;


        if (W_Top)
            Debug.DrawLine(new Vector3(w*(X),h*(Y+1),0) + Offset, new Vector3(w * (X+1), h * (Y+1), 0) + Offset, color);         //*///Top
        if (W_Left)                                                                                           
            Debug.DrawLine(new Vector3(w*(X),h*(Y+1),0) + Offset, new Vector3(w * (X), h * (Y), 0) + Offset, color);                 //*/// Left
        if (W_Right)                                                                                          
            Debug.DrawLine(new Vector3(w * (X+1), h * (Y + 1), 0) + Offset, new Vector3(w * (X+1), h * (Y), 0) + Offset, color);         //*/// Right
        if (W_Bottom)
            Debug.DrawLine(new Vector3(w * (X), h * (Y), 0) + Offset, new Vector3(w * (X + 1), h * (Y), 0) + Offset, color);                 //*/// Bot

        float PlusSize = (w + h) / 4;

        if (!Visited)
        {
            Debug.DrawRay(new Vector3(w * X + (w / 2.0f), h * Y + (h / 2.0f), 0) + Offset - new Vector3(PlusSize / 2.0f, 0.0f, 0), new Vector3(PlusSize, 0.0f, 0), color);
            Debug.DrawRay(new Vector3(w * X + (w / 2.0f), h * Y + (h / 2.0f), 0) + Offset - new Vector3(0.0f, PlusSize / 2.0f, 0), new Vector3(0.0f, PlusSize, 0), color);
        }
    }

    public void DrawCurrent(float w, float h, Vector3 Offset)
    {
        float PlusSize = (w + h) / 4;
        if (W_Top)
            Debug.DrawLine(new Vector3(w * (X), h * (Y + 1), 0) + Offset, new Vector3(w * (X + 1), h * (Y + 1), 0) + Offset, Color.blue);         //*///Top
        if (W_Left)
            Debug.DrawLine(new Vector3(w * (X), h * (Y + 1), 0) + Offset, new Vector3(w * (X), h * (Y), 0) + Offset, Color.blue);                 //*/// Left
        if (W_Right)
            Debug.DrawLine(new Vector3(w * (X + 1), h * (Y + 1), 0) + Offset, new Vector3(w * (X + 1), h * (Y), 0) + Offset, Color.blue);         //*/// Right
        if (W_Bottom)
            Debug.DrawLine(new Vector3(w * (X), h * (Y), 0) + Offset, new Vector3(w * (X + 1), h * (Y), 0) + Offset, Color.blue);                 //*/// Bot

        Debug.DrawRay(new Vector3(w * X + (w / 2.0f), h * Y + (h / 2.0f), 0) + Offset - new Vector3(PlusSize / 2.0f, 0.0f, 0), new Vector3(PlusSize, 0.0f, 0), Color.blue);
        Debug.DrawRay(new Vector3(w * X + (w / 2.0f), h * Y + (h / 2.0f), 0) + Offset - new Vector3(0.0f, PlusSize / 2.0f, 0), new Vector3(0.0f, PlusSize, 0), Color.blue);
    }

    public void DrawEnd(float w, float h, Vector3 Offset)
    {
        float PlusSize = (w + h) / 4;
        if (W_Top)
            Debug.DrawLine(new Vector3(w * (X), h * (Y + 1), 0) + Offset, new Vector3(w * (X + 1), h * (Y + 1), 0) + Offset, Color.green);         //*///Top
        if (W_Left)
            Debug.DrawLine(new Vector3(w * (X), h * (Y + 1), 0) + Offset, new Vector3(w * (X), h * (Y), 0) + Offset, Color.green);                 //*/// Left
        if (W_Right)
            Debug.DrawLine(new Vector3(w * (X + 1), h * (Y + 1), 0) + Offset, new Vector3(w * (X + 1), h * (Y), 0) + Offset, Color.green);         //*/// Right
        if (W_Bottom)
            Debug.DrawLine(new Vector3(w * (X), h * (Y), 0) + Offset, new Vector3(w * (X + 1), h * (Y), 0) + Offset, Color.green);                 //*/// Bot

        Debug.DrawRay(new Vector3(w * X + (w / 2.0f), h * Y + (h / 2.0f), 0) + Offset - new Vector3(PlusSize / 2.0f, 0.0f, 0), new Vector3(PlusSize, 0.0f, 0), Color.green);
        Debug.DrawRay(new Vector3(w * X + (w / 2.0f), h * Y + (h / 2.0f), 0) + Offset - new Vector3(0.0f, PlusSize / 2.0f, 0), new Vector3(0.0f, PlusSize, 0), Color.green);
    }
}