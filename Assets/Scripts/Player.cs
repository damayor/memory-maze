using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //Pos actual en el puzzle
    public Vector2 pos;


    [SerializeField]
    //protected bool canMove;


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

    //solo actualiza la position en el laberinto, no en el espacio
    //public void setPosition(Vector2 nPos)
    //{
    //    pos = pos + nPos;

        
    //}


//    public void SetFinalPos(Vector2 v)
//    {
//        finalPos = v;
//    }

//    public Vector2 GetFinalPos()
//    {
//        return finalPos;
//    }

//    public void SetPos(Vector2 v)
//    {
//        pos = v;
//    }

//    public Vector2 GetPos()
//    {
//        return pos;
//    }

//    //Se puede mover porque tiene el espacio al lado
//    public bool CanIMove(Ficha espacio)
//    {

//        if (espacio.pos.y == pos.y
//                && Mathf.Abs(pos.x - espacio.pos.x) == 1)
//        {
//            canMove = true;
//        }
//        else if (espacio.pos.x == pos.x
//                && Mathf.Abs(pos.y - espacio.pos.y) == 1)
//        {
//            canMove = true;
//        }
//        else
//        {
//            canMove = false;
//        };

//        return canMove;

//    }


//    public void MoveToSpace(Ficha espacio)
//    {

//        //Ficha espacio = puzzleInfo.GetEmptyFicha();

//        if (CanIMove(espacio))
//        {
//            Vector2 spacePos = espacio.pos;
//            espacio.pos = pos;
//            pos = spacePos;

//            //y lo mismo con XYZ location
//            Vector3 empty3DLocation = espacio.transform.position;
//            Vector3 my3DLocation = transform.position;

//            //Animate
//            StartCoroutine("AnimPosition", empty3DLocation);


//            espacio.transform.position = my3DLocation; //no cambiaba!! R: por ser parte de un prefab instanceado

//            puzzleInfo.CheckWonAfterMove();
//        }

//    }

//    public void SetTap()
//    {
//        isTap = true;
//    }

//    public bool CheckLocated()
//    {
//        if (pos == finalPos)
//        {
//            isLocated = true;
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    public void SetLocated(bool b)
//    {
//        isLocated = b;
//    }

//    //My first Coroutine
//    IEnumerator AnimPosition(Vector3 emptyPos)
//    {

//        float lerp = 0;
//        float duration = 0.1f;

//        Vector3 myPos = transform.position;

//        while (emptyPos != transform.position)
//        {
//            lerp += Time.deltaTime / duration;
//            transform.position = Vector3.Lerp(myPos, emptyPos /* espacio.transform.position*/, lerp);
//            yield return null;
//        }

//        //yield return new WaitForSeconds(0.15f);
//        Debug.Log("Sí acabo el while :P");

//    }

//    //no corountine sino no se anima
//    void MoveAnimate(Vector3 emptyPos)
//    {

//        float lerp = 0;
//        float duration = 1f;

//        Vector3 myPos = transform.position;
//        bool located = false;

//        while (!located)
//        {
//            lerp += Time.deltaTime / duration;
//            transform.position = Vector3.Lerp(myPos, emptyPos /* espacio.transform.position*/, lerp);

//            located = emptyPos == transform.position ? true : false;

//            // yield return null;
//        }
//    }


//    public Transform SetEmptyPos()
//    {
//        return puzzleInfo.GetEmptyFicha().transform;
//    }
}

public enum Direction
{
    Up, Down, Right, Left, Undo
}
