using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

/**
 ** Damayor- Tablero que representa un laberinto para ser memorizado
 **/
public class Board : MonoBehaviour
{
    public GameObject coordPrefab;
    
    public static int lengthW;
    public static int lengthH;

    public static int range;

    //para verlas en el editor
    public List<Coord> coords;

    public Coord[,] coordsArray;

    //public Vector3[,] canvasLocations;

    public Vector2[,] canvasLocations;

    //private Coord emptyCoord;

    // inicio final, [0], [length-1]
    public List<Coord> wayToWalk;



    private Vector3 lienzoDims; //joo xq no salia en debug? porque era public? wtf?

    private Vector3 initCoordPos;

    private int tries = 0;


    public Player player;
    public Transform goal;
    public Transform obstacle;



    public Material matStone;
    public Material matWay;

    private static readonly MovementCommand MoveUndo = new UndoCommand();
    private bool obsDone = false;

    public static ConfigJSON configData;

    
    

    // Inicialization of Config Data
    private void Awake()
    {
        string path = /*Application.dataPath +*/ "config.json"; //para que quede en la misma jerarquia del ejecutable

        string jsonString = File.ReadAllText(path);

        configData = JsonUtility.FromJson<ConfigJSON>(jsonString);

        Debug.Log("Datos de configuración cargados");

        
        for (int i = 0; i < configData.wayCoordsX.Length; i++)
        {
            Coord cW = new Coord();
            cW.pos = new Vector2(configData.wayCoordsX[i], configData.wayCoordsY[i]);
            wayToWalk.Add(cW);

        }

        ToolboxStaticData.SetCoordsWay(wayToWalk);
        ToolboxStaticData.rangeXMaze = configData.mazeW;
        ToolboxStaticData.rangeYMaze = configData.mazeH;
    }

    void Start()
    {

        lengthW = ToolboxStaticData.rangeXMaze;
        lengthH = ToolboxStaticData.rangeYMaze;

        range = lengthW * lengthH;

        //lienzoDims = getLienzo();

        //coords = new Coord[lengthW * lengthH];

        //coordsArray = new Coord[lengthW, lengthH];

        //Guardar las posiciones en 3D world
        canvasLocations = new Vector2[lengthW, lengthH];



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
        //        canvasLocations[s, t] = canvasPos;

        //        canvasPos.x = canvasPos.x + width + spaceBetwCoord;

        //    }

        //    canvasPos.y = canvasPos.y + height + spaceBetwCoord;
        //    canvasPos.x = initCoordPos.x;
        //}

        //PopulatePuzzle();

        GenerateLabyrinth();

        //ponga el player en la primera celda del way


       

    }

    // Update is called once per frame
    void Update()
    {

     
       


    }

    void CheckObstacle()
    {
        if (player.pos == obstacle.GetComponent<Coord>().pos )
        {

            Debug.Log("llegó al obstaculo!");

            player.Move(Direction.Undo);
            //player.MoveUndo();
            obsDone = true;
            ToolboxStaticData.SetObstacled(true);

            //ToDo desaparece sprite moon
            obstacle.GetComponent<RawImage>().enabled = false;

        }
    }
    
    IEnumerator AnimPosition(Vector2 nextPos) //emptyPos = Pos final
    {

        float lerp = 0;
        float duration = 0.1f;

        Vector2 initPos = player.transform.GetComponent<RectTransform>().anchoredPosition;

        while (nextPos != player.transform.GetComponent<RectTransform>().anchoredPosition)
        {
            lerp += Time.deltaTime / duration;
            player.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(initPos, nextPos, lerp);
            yield return null;
        }


        if (!obsDone)
        {
            CheckObstacle();
        }

    }

    public void UpdatePlayerPos(Vector2 location)
    {


        Vector2 newPos = canvasLocations[(int)location.x, (int)location.y] + new Vector2(-200, 200);

        //player.transform.GetComponent<RectTransform>().anchoredPosition = canvasLocations[(int)location.x, (int)location.y] + new Vector2(-200, 200);

        //Animate
        StartCoroutine("AnimPosition", newPos);


        player.pos = location; //esto pa que?
    }

    void GenerateLabyrinth()
    {
        Coord newCoord;

        int fichaImg = 1;

        //coordPrefab.transform.localScale = new Vector3(lienzoDims.x / sizeX / 10f, 0.2f, lienzoDims.y / lengthH / 10f);



        for (int i = 0; i < lengthW * lengthW; i++)
        {
            GameObject go = Instantiate(coordPrefab, this.transform) as GameObject;

            newCoord = go.GetComponent<Coord>();



            //ToDebug
            int x = (i % lengthW);
            int y = (i / lengthW);

            newCoord.pos = new Vector2(x, y);


            //
            if (wayToWalk.Contains(newCoord))
            {
                Debug.Log("se le encontro desde el json" + newCoord.pos);

                //  wayToWalk.

                newCoord.isEmpty = true;
                //wayToWalk.Add(newCoord);
            }


            //foreach (Coord c in wayToWalk)
            //{
            //    if (c.Equals(newCoord))
            //    {
            //        newCoord.isEmpty = true;
            //        Debug.Log("se le encontro desde el json" + newCoord.pos);
            //        break;
            //    }
            //}

            //wayToWalk.ForEach(c =>
            //{
            //    if (c.Equals(newCoord))
            //    {
            //        newCoord.isEmpty = true;
            //        break;
            //    }
            //});


            //ToDevelop: way from Resources file or generated random
            //if (y == 2)
            //{
            //    newCoord.isEmpty = true;
            //    wayToWalk.Add(newCoord);
            //    //newCoord.SetMaterial(matWay);


            //    //Add osbtacle
            //    if (x == 2)
            //    {
            //        Coord obsC = obstacle.GetComponent<Coord>();
            //        obsC.SetMazePosition(new Vector2(x, y));
            //        ToolboxStaticData.SetObstaclePosition(obsC);
            //    }

            //}
            //else {
            //    //newCoord.SetMaterial(matStone);
            //}

            //Locate Obstacle
            if (wayToWalk[configData.indexObstacle].Equals(newCoord))
            {
                Coord obsC = obstacle.GetComponent<Coord>();
                obsC.SetMazePosition(new Vector2(x, y));
                ToolboxStaticData.SetObstaclePosition(obsC);
            }
            

            coords.Add(newCoord);
            ToolboxStaticData.SetCoordsWay(wayToWalk);

        }

        StartCoroutine("UpdateLayoutPositions");
        

    }


    //las posiciones de las coordennadas dentro de un LayoutGroup se demoran un poco en actualizarse
    IEnumerator UpdateLayoutPositions ()
    {  
        yield return new WaitForEndOfFrame();
    
        foreach (Coord c in this.coords)
        {
            Vector2 cellPos = c.transform.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(cellPos);

            canvasLocations[(int)c.pos.x, (int)c.pos.y] = cellPos;

        }

        //firs locate
        //player.transform.GetComponent<RectTransform>().anchoredPosition = canvasLocations[(int)wayToWalk[0].pos.x, (int)wayToWalk[0].pos.y] + new Vector2(-200, 200);

        UpdatePlayerPos(wayToWalk[0].pos);

        goal.GetComponent<RectTransform>().anchoredPosition = canvasLocations[(int)wayToWalk[wayToWalk.Count - 1].pos.x, 
                                                                                (int)wayToWalk[wayToWalk.Count - 1].pos.y]
                                                            + new Vector2(-200, 200);

        obstacle.GetComponent<RectTransform>().anchoredPosition = canvasLocations[(int) obstacle.GetComponent<Coord>().pos.x,
                                                                                  (int) obstacle.GetComponent<Coord>().pos.y ]
                                                            + new Vector2(-200, 200);
    }

    //Genera el puzzle y asigna las variables de las coords
    void PopulatePuzzle()
    {

        //Coord newCoord;

        //int fichaImg = 1;

        //coordPrefab.transform.localScale = new Vector3(lienzoDims.x / lengthW / 10f, 0.2f, lienzoDims.y / lengthH / 10f);


        //for (int i = 0; i < sizeX; i++)
        //{
        //    for (int j = 0; j < lengthH; j++)
        //    {
        //        GameObject go = Instantiate(fichaPrefab, canvasLocations[i, j], Quaternion.Euler(-90, 0, 0), this.transform) as GameObject;


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


    public List<Coord> GetCoordsWay()
    {
        return wayToWalk;
    }

    public Coord GetStartPos()
    {
        return wayToWalk[0];
    }

    public Coord GetFinalPos()
    {
        return wayToWalk[wayToWalk.Count - 1];
    }


}

// Clase de Configuración vista en el editor de Unity en el Script Json Config
[System.Serializable]
public class ConfigJSON
{
    public string Name;

    public int mazeW;
    public int mazeH;

    public float[] defaultXYZOrientationRange;
    public int[] wayCoordsX;
    public int[] wayCoordsY;
    public int indexObstacle;


}

