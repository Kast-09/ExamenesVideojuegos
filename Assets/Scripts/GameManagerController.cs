using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerController : MonoBehaviour
{
    //public Text scoreText;
    //private int score;
    public Text cantZombiesText;
    private int zombies;
    public Text cantMonedasText;
    private int monedas;
    public Text cantVidasText;
    private int vidas;
    // Start is called before the first frame update
    void Start()
    {
        //score = 0;
        zombies = 0;
        monedas = 0;
        vidas= 3;
        PrintMonedasInScreen();
        PrintVidasInScreen();
        PrintZombiesInScreen();
        //PrintScoreInScreen();
        //LoadGame();
    }

    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/save.dat";

        FileStream file;

        if (File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        //data.Score = score;
        data.vidas = vidas;
        data.monedas = monedas;
        data.zombies = zombies;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/save.dat";
        FileStream file;
        if (File.Exists(filePath))
            file = File.OpenRead(filePath);
        else
        {
            Debug.LogError("No se encontro el archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        //utilizar los datos guardados
        //score = data.Score;
        monedas = data.monedas;
        zombies = data.zombies;
        vidas = data.vidas;
        //PrintScoreInScreen();
    }

    //public int Score()
    //{
    //    return score;
    //}

    //public void GanarPuntos(int puntos)
    //{
    //    score += puntos;
    //    PrintScoreInScreen();
    //}

    public int cantZombies()
    {
        return zombies;
    }

    public void contZombies()
    {
        zombies += 1;
        PrintZombiesInScreen();
    }

    public void PrintZombiesInScreen()
    {
        cantZombiesText.text = "Cant Zombies: " + zombies;
    }

    public int cantVidas()
    {
        return vidas;
    }

    public void contVidas()
    {
        zombies -= 1;
        PrintVidasInScreen();
    }

    public void PrintVidasInScreen()
    {
        cantVidasText.text = "Cant Vidas: " + vidas;
    }

    public int cantMonedas()
    {
        return monedas;
    }

    public void contMonedas()
    {
        monedas += 1;
        PrintMonedasInScreen();
    }

    public void PrintMonedasInScreen()
    {
        cantMonedasText.text = "Cant Monedas: " + monedas;
    }

    //private void PrintScoreInScreen()
    //{
    //    scoreText.text = "Puntaje: " + score;
    //}
}
