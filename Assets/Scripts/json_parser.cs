using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class json_parser : MonoBehaviour
{
    [Header("Choose json file (1-4)")]
    public int jsonFile;

    string path;
    string jsonString;
    [HideInInspector]
    public Vector3[] coordinates;

    void Start()
    {
        switch(jsonFile)
        {
            case 1:
                path = Application.streamingAssetsPath + "/ball_path.json";
                break;
            case 2:
                path = Application.streamingAssetsPath + "/ball_path2.json";
                break;
            case 3:
                path = Application.streamingAssetsPath + "/ball_path3.json";
                break;
            case 4:
                path = Application.streamingAssetsPath + "/ball_path4.json";
                break;
            default:
                path = Application.streamingAssetsPath + "/ball_path.json";
                break;
        }
        
        jsonString = File.ReadAllText(path);
        ball_path Ball = JsonUtility.FromJson<ball_path>(jsonString);

        coordinates = new Vector3[Ball.x.Length];

        for (int i = 0; i < Ball.x.Length; i++)
            coordinates[i] = new Vector3(Ball.x[i], Ball.y[i], Ball.z[i]);
    }
}

[System.Serializable]
public class ball_path
{
    public float[] x;
    public float[] y;
    public float[] z;
}

