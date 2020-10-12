using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*damayor - Toolbox with data needed from any script*/
public static class ToolboxStaticData 
{

    public static int rangeXMaze = 4;
    public static int rangeYMaze = 4;

    public static int memorizeTime = 6;

    public static List<Coord> way;
    public static Coord obstaclePosition;

    public static string loadingPath = "";

    public static bool hasObstacled = false;

    public static void SetLoadingPath(string path)
    {
        ToolboxStaticData.loadingPath = path;
    }
    public static string GetLoadingPath()
    {
        return loadingPath;
    }

    public static void SetCoordsWay(List<Coord> w)
    {
        ToolboxStaticData.way = w;
    }

    public static void AddCoordWay(Coord c)
    {
        ToolboxStaticData.way.Add(c);
    }

    public static List<Coord> GetCoordsWay()
    {
        return way;
    }

    public static void SetObstaclePosition(Coord o)
    {
        ToolboxStaticData.obstaclePosition = o;
    }

    public static Coord GetObstaclePosition()
    {
        return obstaclePosition;
    }

    public static void SetObstacled(bool b)
    {
        ToolboxStaticData.hasObstacled = b;
    }

    public static bool GetObstacled()
    {
        return hasObstacled;
    }

}
