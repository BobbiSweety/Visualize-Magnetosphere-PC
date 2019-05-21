using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using UnityEngine.UI;
using System.Text;

#region OpenFileName数据接收类
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}
#endregion

#region 系统函数调用类
public class WindowDll
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);
    }
}
#endregion

#region 入口类
public class GetMagFile : MonoBehaviour
{
    //List<Vector3> points;/
    private Color color1 = Color.blue;
    private Color color2 = Color.white;

    /*
     * 线集合
     */
    private GameObject[] LineSet = new GameObject[252];
    private LineRenderer[] LineRen = new LineRenderer[252];
    //二维数组，第一元素为线条数，第二元素为所有数据
    private string[][] ArrayLine;

    private PinTuGameObjectManger gom;
    private string texPath;
 
    private void Start()
    {
        gom = gameObject.GetComponent<PinTuGameObjectManger>();

        gom.btnLoad.onClick.AddListener(OnLoadButtonClick);
      
        //生成一个叫“Resources”的文件夹
        /*string dirPath = Application.dataPath + "/" + "Resources";
        DirectoryInfo mydir = new DirectoryInfo(dirPath);
        if (!mydir.Exists)
            Directory.CreateDirectory(dirPath);*/

        FileStreamLoadFile();
    }
    /// <summary>
    /// 加载文件按钮点击事件
    /// </summary>
    private void OnLoadButtonClick()
    {
        OpenFileWin();
    }

    private void OpenFileWin()
    {
        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "All Files\0*.*\0\0";
        ofn.filter = "CSV File(*.txt)\0*.txt";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        string path = Application.streamingAssetsPath;
        path = path.Replace('/', '\\');
        //默认路径  
        ofn.initialDir = path;
        ofn.title = "Load Flie";
        ofn.defExt = "txt";//显示文件的类型      
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  


        if (WindowDll.GetOpenFileName(ofn))
        {
            texPath = ofn.file;
            Debug.Log("Selected file with full path: {0}" + ofn.file);
            FileStreamLoadFile();
        }
    }
    /// <summary>
    /// 文件流加载文件
    /// </summary>
    private void FileStreamLoadFile()
    {
        FileStream fs = null;
        StreamReader sr = null;
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/MagFile" + ".txt", true);
        try
        {

            String content = String.Empty;
            fs = new FileStream(texPath, FileMode.Open);
            sr = new StreamReader(fs);
            while ((content = sr.ReadLine()) != null)
            {
                content = content.Trim().ToString();
                sw.WriteLine(content);
            }
        }
        catch
        {
            Console.WriteLine("读取内容到文件方法错误");
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
    /*private void DrawMag()
    {
        //读取csv二进制文件  
        TextAsset file = Resources.Load("MagFile", typeof(TextAsset)) as TextAsset;

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
            int j = ArrayLine[m].Length / 5;  //每条线上点的个数
            String x = "Line" + m;
            LineSet[m] = new GameObject(x);
            LineRen[m] = (LineRenderer)LineSet[m].AddComponent<LineRenderer>();
          
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
    } */
}
#endregion