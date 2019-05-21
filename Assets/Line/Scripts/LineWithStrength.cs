using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LineWithStrength : MonoBehaviour
{
    //List<Vector3> points;/
    private Color color1 = Color.black;
    private Color color2 = Color.red;
    private Color color3 = Color.green;
    private Color color4 = Color.blue;

    /*
     * 线集合
     */
    private GameObject[] LineSet = new GameObject[255];
    private LineRenderer[] LineRen = new LineRenderer[255];
    public GameObject LineFather;
    public TextAsset file;

    //二维数组，第一元素为线条数，第二元素为所有数据
    private string[][] ArrayLine;

  
    // Use this for initializations
    void Start()
    {
        //读取csv二进制文件  
        TextAsset file = Resources.Load("FieldLinewithStrength", typeof(TextAsset)) as TextAsset;

        //读取每一行的内容  
        string[] lineArray = file.text.Split("\r"[0]);

        //创建二维数组  
        ArrayLine = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            ArrayLine[i] = lineArray[i].Split(',');
        }

        for (int m = 0; m < LineSet.Length; m++)
        {
            int j = ArrayLine[m].Length / 7;  //每条线上点的个数
            String x = "Line" + m;
            LineSet[m] = new GameObject(x);
            LineSet[m].transform.parent = LineFather.transform; //设置生成的场线的父物体为指定物体
            LineRen[m] = (LineRenderer)LineSet[m].AddComponent<LineRenderer>();
            /*
             * 设置线属性
             */
            LineRen[m].material = new Material(Shader.Find("Sprites/Default"));
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
               new GradientColorKey[] { new GradientColorKey(color1, 0.20f), new GradientColorKey(color2,0.25f ), new GradientColorKey(color3, 0.50f),new GradientColorKey(color4, 0.65f), new GradientColorKey(color3, 0.85f), new GradientColorKey(color2, 0.9f), new GradientColorKey(color1, 1f) },
               new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.0f) }
               );
            if (m <= 36)
            {
                LineRen[m].startColor = Color.black;
                LineRen[m].endColor = Color.black;
            }
            else         
                LineRen[m].colorGradient = gradient;           
            LineRen[m].widthMultiplier = 0.05f;

            LineRen[m].positionCount = j;

            var points = new Vector3[j];
            for (int k = 0; k < j; k++)
            {
                points[k] = new Vector3(Convert.ToSingle(ArrayLine[m][k * 7 + 2]), Convert.ToSingle(ArrayLine[m][k * 7 + 4]), Convert.ToSingle(ArrayLine[m][k * 7 + 3]));         
            }
            LineRen[m].SetPositions(points);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

