﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coord : MonoBehaviour {

    //Pos actual en el puzzle
    public Vector2 pos;

    //Pos final que deberá tener
    //public Vector2 finalPos;

    //[SerializeField]
    //protected bool canMove;

    //protected bool isLocated;

    //public bool isRandomed;

    //private bool isTap;

    public bool isEmpty = false;

    

    //public Board puzzleInfo;



    // Use this for initialization
    void Start()
    {
        //canvasPos = GetComponent<RectTransform>().rect.center;
        //pff solo canvas Debug.Log(GetComponent<RectTransform>().anchoredPosition3D + ";;;" + GetComponent<RectTransform>().anchoredPosition);

        //anchoredposition No..

        if (isEmpty)
        {
            GetComponent<RawImage>().color = Color.white; 
        }
        else
        {
            GetComponent<RawImage>().color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsEmptySpace()
    {
        return isEmpty;
    }

    public void SetAsEmpty(bool b)
    {
        isEmpty = b;
    }

    //public void SetFinalPos(Vector2 v)
    //{
    //    finalPos = v;
    //}

    //public Vector2 GetFinalPos()
    //{
    //    return finalPos;
    //}

    //public void SetPos(Vector2 v)
    //{
    //    pos = v;
    //}

    //public Vector2 GetPos()
    //{
    //    return pos;
    //}

    ////Se puede mover porque tiene el espacio al lado
    //public bool CanIMove(Coord espacio)
    //{

    //    if (espacio.pos.y == pos.y
    //            && Mathf.Abs(pos.x - espacio.pos.x) == 1)
    //    {
    //        canMove = true;
    //    }
    //    else if (espacio.pos.x == pos.x
    //            && Mathf.Abs(pos.y - espacio.pos.y) == 1)
    //    {
    //        canMove = true;
    //    }
    //    else
    //    {
    //        canMove = false;
    //    };

    //    return canMove;

    //}


    //public void MoveToSpace(Coord espacio)
    //{

    //    //Coord espacio = puzzleInfo.GetEmptyCoord();

    //    if (CanIMove(espacio))
    //    {
    //        Vector2 spacePos = espacio.pos;
    //        espacio.pos = pos;
    //        pos = spacePos;

    //        //y lo mismo con XYZ location
    //        Vector3 empty3DLocation = espacio.transform.position;
    //        Vector3 my3DLocation = transform.position;

    //        //Animate
    //        StartCoroutine("SwapPositions", empty3DLocation);


    //        espacio.transform.position = my3DLocation; //no cambiaba!! R: por ser parte de un prefab instanceado

    //        puzzleInfo.CheckWonAfterMove();
    //    }

    //}

    //public void SetTap()
    //{
    //    isTap = true;
    //}

    //public bool CheckLocated()
    //{
    //    if (pos == finalPos)
    //    {
    //        isLocated = true;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public void SetLocated(bool b)
    //{
    //    isLocated = b;
    //}

    //public void MoveButton(Coord espacio)
    //{
    //    Vector2 spacePos = espacio.pos;
    //    espacio.pos = pos;
    //    pos = spacePos;

    //    //y lo mismo con XYZ location
    //    Vector2 empty3DLocation = espacio.GetComponent<RectTransform>().anchoredPosition;
    //    Vector2 my3DLocation = GetComponent<RectTransform>().anchoredPosition;

    //    GetComponent<RectTransform>().anchoredPosition = empty3DLocation; //yeahh
    //                                                                      //no cambia!!
    //    espacio.ChangePos(new Vector2(300, 100)); //nada...

    //    //Puzzle.CheckMovables, CheckIfPuzzleDone

    //    Debug.Log("Movido el " + GetComponentInChildren<Text>().text + " al espacio " + my3DLocation);
    //}

    //public Transform SetEmptyPos()
    //{
    //    return puzzleInfo.GetEmptyCoord().transform;
    //}

    




}
