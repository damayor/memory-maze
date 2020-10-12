using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coord : MonoBehaviour, System.IEquatable<Coord>
{

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
            //GetComponent<RawImage>().material = Resources.Load("/Materials/grass_2", typeof(Material)) as Material;
        }
        else
        {
            GetComponent<RawImage>().color = new Color(0.27f, 0.24f, 0.15f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Equals(Coord other)
    {
        //returns true only if both bools in classes are equal AND both myObj point to the same thing.
        return this.pos == other.pos;
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<RawImage>().material = mat;
    }

    public void SetMazePosition(Vector2 v)
    {
        pos = v;
    }







    //public void SetTap()
    //{
    //    isTap = true;
    //}

    
    




}
