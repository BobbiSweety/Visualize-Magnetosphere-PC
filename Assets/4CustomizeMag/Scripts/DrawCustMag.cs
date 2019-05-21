using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DrawCustMag : MonoBehaviour
{
    //List<Vector3> points;/
    private Color color1 = Color.cyan;
    private Color color2 = Color.white;

    /*
     * 线集合
     */
    private GameObject[] LineSet = new GameObject[500];
    private LineRenderer[] LineRen = new LineRenderer[500];

    // public TextAsset file;

    //二维数组，第一元素为线条数，第二元素为所有数据
    private string[][] ArrayLine;

    // Use this for initializations

    public void DrawMag()
    {
        StartCoroutine(LoadMag());
    }
    
      IEnumerator LoadMag() 
    {
        //读取csv二进制文件  
        // TextAsset file = Resources.Load("MagFile", typeof(TextAsset)) as TextAsset;
        string mPath = Application.persistentDataPath + "/MagFile.txt";
        mPath = "file://" + mPath;
        WWW www = new WWW(mPath);
        yield return www;
       
            //读取每一行的内容  
        string[] lineArray = www.text.Split("\r"[0]);

            //创建二维数组  
            ArrayLine = new string[lineArray.Length][];

            //把csv中的数据储存在二位数组中  
            for (int i = 0; i < lineArray.Length; i++)
            {
                ArrayLine[i] = lineArray[i].Split(',');
            }

            for (int m = 0; m <LineSet.Length; m++)
            {
                int j = ArrayLine[m].Length / 5;  //每条线上点的个数
                String x = "Line" + m;
                LineSet[m] = new GameObject(x);
                LineRen[m] = (LineRenderer)LineSet[m].AddComponent<LineRenderer>();
                /*
                 * 设置线属性
                 */
                LineRen[m].material = new Material(Shader.Find("Sprites/Default"));
                float alpha = 1.0f;
                Gradient gradient = new Gradient();
                gradient.SetKeys(
                   new GradientColorKey[] { new GradientColorKey(color1, 0.0f), new GradientColorKey(color2, 1.0f) },
                   new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                   );
                LineRen[m].colorGradient = gradient;
                LineRen[m].widthMultiplier = 0.025f;

                LineRen[m].positionCount = j;

                var points = new Vector3[j];
                for (int k = 0; k < j; k++)
                {
                    points[k] = new Vector3(Convert.ToSingle(ArrayLine[m][k * 5 + 2]), Convert.ToSingle(ArrayLine[m][k * 5 + 4]), Convert.ToSingle(ArrayLine[m][k * 5 + 3]));
                    //Debug.Log(points[k].ToString("F4"));
                }
                LineRen[m].SetPositions(points);
            }
        
    }
    // Update is called once per framez
    void Update()
    {

    }
}

