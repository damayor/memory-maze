using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //Pos actual en el puzzle
    public Vector2 pos;

    public Board boardRequest;
    public Vector2 previousPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
    }


    /*in developing mode*/
    public void MoveUpdate()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (pos.y < ToolboxStaticData.rangeYMaze - 1)
            {
                Debug.Log("up");
                previousPos = pos;
                pos = pos + Vector2.up;
                boardRequest.UpdatePlayerPos(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (pos.y > 0)
            {
                previousPos = pos;
                pos = pos + Vector2.down ;
                boardRequest.UpdatePlayerPos(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (pos.x < ToolboxStaticData.rangeXMaze - 1)
            {
                previousPos = pos;
                pos = pos + Vector2.right;
                boardRequest.UpdatePlayerPos(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (pos.x > 0)
            {
                previousPos = pos;
                pos = pos + Vector2.left;
                boardRequest.UpdatePlayerPos(pos);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveUndo();

        }

        ////Animate
        //StartCoroutine("AnimPosition", empty3DLocation);
        //SendMessage("UpdatePlayerPos", pos);
    }


    public void MoveUndo()
    {
        var lastPos = pos;
        pos = previousPos;
        previousPos = lastPos;
        boardRequest.UpdatePlayerPos(pos);

    }



    public void Move(Direction dir )
    {
        Debug.Log("Go "+ dir);
        

        switch (dir)
        {
            //to confirm si: vector.down si lo baja en mi arreglo de pos, si
            // ojo porque si no lo deja andar estando fuera del arreglo, pero lo deja en la misma pos, no nota que perdip
            case Direction.Down:
                previousPos = pos;
                pos = pos + Vector2.down;
                break;
            case Direction.Up:
                previousPos = pos;
                pos = pos + Vector2.up;
                break;
            case Direction.Left:
                previousPos = pos;
                pos = pos + Vector2.left;
                break;
            case Direction.Right:
                previousPos = pos;
                pos = pos + Vector2.right;
                break;
            case Direction.Undo:
                pos = previousPos;
                break;
        }

        boardRequest.UpdatePlayerPos(pos);

    }

    public void playerReset()
    {

    }

}

public enum Direction
{
    Up, Down, Right, Left, Undo
}
