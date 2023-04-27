using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continents : ContinentManager
{
    public void Restart()
    {
        slotmananger.Restart();
        UpdateText();
    }
}
