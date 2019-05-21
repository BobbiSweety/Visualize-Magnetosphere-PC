using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;



#region 入口类
public class GetLineFile : MonoBehaviour
{

    //List<Vector3> points;/
    private Color color1 = Color.blue;
    private Color color2 = Color.white;
    private GameObject Line;
    private LineRenderer LineRen;
    public GameObject LineFather;

    //二维数组，第一元素为线条数，第二元素为所有数据
    private string[][] ArrayLine;
    private int m;
    float Time_f;

    private PinTuGameObjectManger gom1;
    private string texPath;

    private void Start()
    {
        gom1 = gameObject.GetComponent<PinTuGameObjectManger>();

        gom1.btnLoad.onClick.AddListener(OnLoadButtonClick);

        //生成一个叫“Resources”的文件夹
       /* string dirPath = Application.dataPath + "/" + "Resources";
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
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/LineFile" + ".txt", true);
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
}
#endregion