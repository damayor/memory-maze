using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCommand : MovementCommand
{

 
    public DownCommand() : base(delegate (Player bot) { bot.Move(Direction.Down); }, "moveDown")
    { }
}
