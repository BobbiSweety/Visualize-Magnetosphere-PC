using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawCustLine : MonoBehaviour
{

    //List<Vector3> points;/
    private Color color1 = Color.cyan;
    private Color color2 = Color.white;
    private GameObject Line;
    private LineRenderer LineRen;
    public GameObject LineFather;

    //二维数组，第一元素为线条数，第二元素为所有数据
    private string[][] ArrayLine;
    private int m;
    float Time_f;

    // Use this for initializations
    public void DrawLine()
    {
        StartCoroutine(LoadLine());
    }

    IEnumerator LoadLine()
    {
        m = 0;
        Time_f = 0f;
        //读取csv二进制文件  
        // TextAsset file = Resources.Load("LineFile", typeof(TextAsset)) as TextAsset;
        string mPath = Application.persistentDataPath + "/LineFile.txt";
        mPath = "file://" + mPath;
        WWW www = new WWW(mPath);
        yield return www;

        //读取每一行的内容  
        string[] lineArray = www.text.Split("\r"[0]);

        //创建二维数组  
        ArrayLine = new string[lineArray.Length][];

        for (int i = 0; i < lineArray.Length; i++)
        {
            ArrayLine[i] = lineArray[i].Split(',');
        }
        string x = "TimeLine";
        Line = new GameObject(x);
        Line.transform.parent = LineFather.transform;
        LineRen = (LineRenderer)Line.AddComponent<LineRenderer>();
        LineRen.useWorldSpace = false;
        LineRen.material = new Material(Shader.Find("Sprites/Default"));
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
           new GradientColorKey[] { new GradientColorKey(color1, 0.0f), new GradientColorKey(color2, 1.0f) },
           new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
           );
        LineRen.colorGradient = gradient;
        LineRen.widthMultiplier = 0.025f;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Time.frameCount % 30 == 0)
        // {
        if (m <= 359)
        {
            int j = 149;
            LineRen.positionCount = j;
            var points = new Vector3[j];
            for (int k = 0; k < j; k++)
            {
                points[k] = new Vector3(Convert.ToSingle(ArrayLine[m][k * 4 + 2]), Convert.ToSingle(ArrayLine[m][k * 4 + 4]), Convert.ToSingle(ArrayLine[m][k * 4 + 3]));
                // Debug.Log(points[k].ToString("F4"));
            }
            LineRen.SetPositions(points);
            m++;
        }
        else
            m = 0;
        // }
    }
}
// LineRen.enabled = false;


