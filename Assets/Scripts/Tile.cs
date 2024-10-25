using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int number;
    public int id;

    public Text ntxt;

    public GameManager gm;

    private void Update()
    {
        ntxt.text = number.ToString();
    }

    public void Click()
    {
        gm.TileMove(id);
    }
}
