using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerController : MonoBehaviour
{
    public Text bulletText;
    private int bullets;

    public Text MonedaBronzeText;
    private int bronze;

    public Text MonedaPlataText;
    private int Plata;

    public Text MonedaOroText;
    private int Oro;

    public Text PuntajeText;
    private int Puntaje;

    private float X;
    private float Y;
    private float Z;

    // Start is called before the first frame update
    void Start()
    {
        bullets = 5;
        bronze = 0;
        Plata = 0;
        Oro = 0;
        Puntaje = 0;
        X = 0.0f;
        Y = 0.0f;
        Z = 0.0f;
        LoadGame();
        PrintBronzeInScreen();
        PrintPlataInScreen();
        PrintOroInScreen();
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
        data.MonedasBronze = bronze;
        data.MonedasPlata = Plata;
        data.MonedasOro = Oro;
        data.Puntaje = Puntaje;
        data.X = X;
        data.Y = Y;
        data.Z = Z;
        //data.lastCheckPointPosition = lastCheckPointPosition;

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
        Plata = data.MonedasPlata;
        PrintPlataInScreen();
        bronze = data.MonedasBronze;
        PrintBronzeInScreen();
        Oro = data.MonedasOro;
        PrintOroInScreen();
        Puntaje = data.Puntaje;
        PrintPuntajeInScreen();
        X = data.X;
        Y = data.Y;
        Z = data.Z;
        //lastCheckPointPosition = data.lastCheckPointPosition;
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

    public void SumarOro()
    {
        Oro += 1;
        PrintOroInScreen();
    }

    private void PrintOroInScreen()
    {
        MonedaOroText.text = "Monedas Oro: " + Oro;
    }
    public void SumarPlata()
    {
        Plata += 1;
        PrintPlataInScreen();
    }

    private void PrintPlataInScreen()
    {
        MonedaPlataText.text = "Monedas Plata: " + Plata;
    }

    public void SumarBronze()
    {
        bronze += 1;
        PrintBronzeInScreen();
    }

    private void PrintBronzeInScreen()
    {
        MonedaBronzeText.text = "Monedas Bronze: " + bronze;
    }

    public void SumarPuntaje()
    {
        Puntaje += 10;
        PrintPuntajeInScreen();
    }

    private void PrintPuntajeInScreen()
    {
        PuntajeText.text = "Puntaje: " + Puntaje;
    }

    public float PositionX()
    {
        return X;
    }

    public float PositionY()
    {
        return Y;
    }

    public float PositionZ()
    {
        return Z;
    }

    public void SetLastCheckPosition(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
