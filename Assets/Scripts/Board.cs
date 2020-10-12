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

    
    public Vector2[,] canvasLocations;
    public List<Coord> wayToWalk;


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

        //Guardar las posiciones en 3D world
        canvasLocations = new Vector2[lengthW, lengthH];

        GetComponent<GridLayoutGroup>().constraintCount = lengthW;
        ToolboxStaticData.SetObstacled(false);

        //GenerateLabyrinth();



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

        //ToDo try catch IndexOutOfRangeException
        Vector2 newPos = canvasLocations[(int)location.x, (int)location.y] + new Vector2(-200, 200);

        

        //Animate
        StartCoroutine("AnimPosition", newPos);


        
    }

    public void GenerateLabyrinth()
    {
        Coord newCoord;


        for (int i = 0; i < lengthW * lengthW; i++)
        {
            GameObject go = Instantiate(coordPrefab, this.transform) as GameObject;

            newCoord = go.GetComponent<Coord>();



            //ToDebug
            int x = (i % lengthW);
            int y = (i / lengthW);

            newCoord.pos = new Vector2(x, y);


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


            //ToDevelop: way from Resources file or generated random DONE
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
        player.pos = wayToWalk[0].pos; 

        goal.GetComponent<RectTransform>().anchoredPosition = canvasLocations[(int)wayToWalk[wayToWalk.Count - 1].pos.x, 
                                                                                (int)wayToWalk[wayToWalk.Count - 1].pos.y]
                                                            + new Vector2(-200, 200);

        obstacle.GetComponent<RectTransform>().anchoredPosition = canvasLocations[(int) obstacle.GetComponent<Coord>().pos.x,
                                                                                  (int) obstacle.GetComponent<Coord>().pos.y ]
                                                            + new Vector2(-200, 200);
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
    public int memorizeSecs;
    public float[] defaultXYZOrientationRange;
    public int[] wayCoordsX;
    public int[] wayCoordsY;
    public int indexObstacle;


}

