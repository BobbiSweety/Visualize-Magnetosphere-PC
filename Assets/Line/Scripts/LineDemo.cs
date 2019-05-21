using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/*
 * 绘制场线
*/
public class LineDemo : MonoBehaviour
{
   
    public TextAsset file;
    //List<Vector3> points;
    private string[][] ArrayLine;
    private int[][] intArrayLine;

    // Use this for initialization
    void Start()
    {

        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        //读取csv二进制文件  
        TextAsset file = Resources.Load("FiledLine", typeof(TextAsset)) as TextAsset;

        //读取每一行的内容  
        string[] lineArray = file.text.Split("\r"[0]);

        //创建二维数组  
        ArrayLine = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            ArrayLine[i] = lineArray[i].Split(',');
        }

        //得出线上点的个数
             int j = ArrayLine[0].Length / 5;
             lineRenderer.positionCount = j;
             var points = new Vector3[j];

             for (int k = 0; k < j; k++)
             {
                 points[k] = new Vector3(Convert.ToSingle(ArrayLine[0][k * 5 + 2]), Convert.ToSingle(ArrayLine[0][k * 5 + 4]), Convert.ToSingle(ArrayLine[0][k * 5 + 3]));
                 //Debug.Log(points[k].ToString("F4"));
             }
             //绘制
             lineRenderer.SetPositions(points);
    }
}





