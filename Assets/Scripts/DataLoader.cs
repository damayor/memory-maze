using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    //public static ConfigJSON configData;

    //// Inicialization of Config Data
    //private void Awake()
    //{
    //    string path = /*Application.dataPath +*/ "config.json"; //para que quede en la misma jerarquia del ejecutable

    //    string jsonString = File.ReadAllText(path);

    //    configData = JsonUtility.FromJson<ConfigJSON>(jsonString);

    //    Debug.Log("Datos de configuración cargados");


    //    for (int i = 0; i < configData.wayCoordsX.Length; i++)
    //    {
    //        Coord cW = new Coord();
    //        cW.pos = new Vector2(configData.wayCoordsX[i], configData.wayCoordsY[i]);
    //        wayToWalk.Add(cW);

    //    }

    //    ToolboxStaticData.SetCoordsWay(wayToWalk);
    //    ToolboxStaticData.rangeXMaze = configData.mazeW;
    //    ToolboxStaticData.rangeYMaze = configData.mazeH;
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// Clase de Configuración vista en el editor de Unity en el Script Json Config
//[System.Serializable]
//public class ConfigJSON
//{
//    public string Name;

//    public int mazeW;
//    public int mazeH;

//    public float[] defaultXYZOrientationRange;
//    public int[] wayCoordsX;
//    public int[] wayCoordsY;
//    public int indexObstacle;


//}

