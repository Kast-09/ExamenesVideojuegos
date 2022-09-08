using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public Text bulletText;
    private int bullets;


    // Start is called before the first frame update
    void Start()
    {
        bullets = 5;
        PrintBulletsInScreen();
    }

    public int Bullets()
    {
        return bullets;
    }

    public void RestarBalas()
    {
        bullets -= 1;
        PrintBulletsInScreen();
    }

    private void PrintBulletsInScreen()
    {
        bulletText.text = "Balas: " + bullets;
    }
}
