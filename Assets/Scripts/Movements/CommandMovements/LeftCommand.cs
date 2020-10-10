using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : MovementCommand
{

    public LeftCommand() : base(delegate (Player bot) { bot.Move(Direction.Left); }, "moveLeft")
    { }
}
