using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/**
 ** Damayor- Tablero que representa un laberinto para ser memorizado
 **/
public class Board : MonoBehaviour
{
    public GameObject coordPrefab;
    private bool won;

    public static int lengthW;
    public static int lengthH;

    public static int range;

    //para verlas en el editor
    public Coord[] coords;

    public Coord[,] coordsArray;

    public Vector3[,] positionsInCanvas;

    //private Coord emptyCoord;

    // inicio final, [0], [length-1]
    public Coord[] emptyWay;

    private GameObject lienzo;

    private float height;
    private float width;
    public float spaceBetwCoord = 0.05f;
   // public float spaceY;

    private Vector3 lienzoDims; //joo xq no salia en debug? porque era public? wtf?

    private Vector3 initCoordPos;

    private int tries = 0;

    // Use this for initialization
    void Start()
    {

        lengthW = 4;
        lengthH = 4;

        range = lengthW * lengthH;

        //lienzoDims = getLienzo();

        coords = new Coord[lengthW * lengthH];

        coordsArray = new Coord[lengthW, lengthH];

        //Guardar las posiciones en 3D world
        positionsInCanvas = new Vector3[lengthW, lengthH];


        //width = lienzoDims.x / sizeX;
        //height = lienzoDims.y / lengthH;


        //initCoordPos = lienzo.transform.position - new Vector3(lienzoDims.x / 2, lienzoDims.y / 2, lienzo.transform.position.z)
        //                                            + new Vector3(width / 2, height / 2, coordPrefab.transform.position.z);
        //   // coordPrefab.transform.position;

        //Vector3 canvasPos = initCoordPos;
        //for (int s = 0; s < sizeX; s++)
        //{
        //    for (int t = 0; t < lengthH; t++)
        //    {
        //        positionsInCanvas[s, t] = canvasPos;

        //        canvasPos.x = canvasPos.x + width + spaceBetwCoord;

        //    }

        //    canvasPos.y = canvasPos.y + height + spaceBetwCoord;
        //    canvasPos.x = initCoordPos.x;
        //}

        //PopulatePuzzle();

        GenerateLabyrinth();


    }

    // Update is called once per frame
    void Update()
    {

        //ser usado en el UI de memoria
//#if UNITY_EDITOR 
//        if (Input.GetMouseButtonDown(0))
//#else
//        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
//#endif
//        {
//            Camera cam = Camera.main;

//#if UNITY_EDITOR
//            Vector3 posMouse = Input.mousePosition;
//            Ray raycast = cam.ScreenPointToRay(posMouse);
//#else
//            Vector3 posTouch = Input.GetTouch(0).position;
//            Ray raycast = cam.ScreenPointToRay(posTouch);
//#endif

//            RaycastHit raycastHit;

//            if (Physics.Raycast(raycast, out raycastHit))
//            {
//                Transform objectHit = raycastHit.transform;
//                if (objectHit.tag == "Piece")
//                {
//                    objectHit.SendMessage("MoveToSpace", emptyCoord);
//                }
//                tries++;
//            }
//            else
//            {
//                Debug.Log("RELA, nada tocado");
//            }

//        }




    }

    void GenerateLabyrinth()
    {
        Coord newCoord;

        int fichaImg = 1;

        //coordPrefab.transform.localScale = new Vector3(lienzoDims.x / sizeX / 10f, 0.2f, lienzoDims.y / lengthH / 10f);

        
               
        for (int i = 0; i < lengthW*lengthW; i++)
        {
            GameObject go = Instantiate(coordPrefab, this.transform) as GameObject;

            newCoord = go.GetComponent<Coord>();

            

            //ToDebug
            int x = (i % lengthW); 
            int y = (i / lengthW);

            newCoord.pos = new Vector2(x, y);

            if (y == 2)
            {
                newCoord.isEmpty = true;
            }

        }

    }

    //Genera el puzzle y asigna las variables de las coords
    void PopulatePuzzle()
    {

        Coord newCoord;

        int fichaImg = 1;

        coordPrefab.transform.localScale = new Vector3(lienzoDims.x / lengthW / 10f, 0.2f, lienzoDims.y / lengthH / 10f);


        //for (int i = 0; i < sizeX; i++)
        //{
        //    for (int j = 0; j < lengthH; j++)
        //    {
        //        GameObject go = Instantiate(fichaPrefab, positionsInCanvas[i, j], Quaternion.Euler(-90, 0, 0), this.transform) as GameObject;


        //        newCoord = go.GetComponent<Coord>();
        //        newCoord.finalPos = new Vector2(i, j);
        //        newCoord.pos = new Vector2(i, j);
        //        go.GetComponentInChildren<TextMesh>().text = fichaImg + "";

        //        if (i == sizeX - 1 && j == lengthH - 1)
        //        {
        //            newCoord.SetAsEmpty(true);
        //            newCoord.GetComponentInChildren<TextMesh>().text = " ";
        //            emptyCoord = newCoord;
        //            newCoord.gameObject.name = "Vacio";
        //            newCoord.GetComponent<Renderer>().enabled = false;
        //        }
        //        //si no específica una asignacion en un prefab, lo aplica a todos
        //        else
        //        {
        //            newCoord.SetAsEmpty(false);
        //            newCoord.GetComponentInChildren<TextMesh>().text = fichaImg + "";
        //        }


        //        fichas[fichaImg - 1] = newCoord;
        //        fichasArray[i, j] = newCoord;

        //        newCoord.puzzleInfo = this;

        //        fichaImg++;

        //    }
        //}
    }

    void MoveToEmptySpace(Vector2 f)
    {


    }


    public Coord[] GetCoordsWay()
    {
        return emptyWay;
    }

    public Coord GetStartPos()
    {
        return emptyWay[0];
    }

    public Coord GetFinalPos()
    {
        return emptyWay[emptyWay.Length - 1];
    }


    //public void OnDrawGizmos()
    //{
    //    GameObject lienz = transform.Find("Lienzo").gameObject;

    //    Bounds bs = lienz.GetComponent<MeshRenderer>().bounds;
    //    Vector3 c = bs.center;

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(c, bs.size);

    //    Debug.Log(bs.size);
    //}
}

