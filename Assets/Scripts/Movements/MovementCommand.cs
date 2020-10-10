using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*With the suppoert of Razeware LLC */
public abstract class MovementCommand 
{
   
    private readonly string commandName;  

    public MovementCommand(ExecuteCallback executeMethod, string name)
    {
        Execute = executeMethod;
        commandName = name;
    }

    public delegate void ExecuteCallback(Player player);

    public ExecuteCallback Execute { get; private set; }

    public override string ToString()
    {
        return commandName;
    }

}
