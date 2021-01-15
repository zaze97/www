// ========================================================
// 描述：基于UnityWebReqest的断点续传 
// ========================================================

using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChinarBreakpointRenewal : MonoBehaviour
{
    private bool _isStop;       //是否暂停

    public Slider ProgressBar; //进度条
    public Text SliderValue; //滑动条值
    private Button startBtn;    //开始按钮
    private Button pauseBtn;    //暂停按钮
    private string Url = "http://images0.cnblogs.com/blog2015/686199/201505/311920537358907.jpg";

    /// <summary>
    /// 初始化UI界面及给按钮绑定方法
    /// </summary>
    void Start()
    {
        //初始化进度条和文本框
        ProgressBar.value = 0;
        SliderValue.text = "0.0%";
        startBtn = GameObject.Find("Start Button").GetComponent<Button>();
        startBtn.onClick.AddListener(OnClickStartDownload);
        pauseBtn = GameObject.Find("Pause Button").GetComponent<Button>();
        pauseBtn.onClick.AddListener(OnClickStop);
    }


    /// <summary>
    /// 回调函数：开始下载
    /// </summary>
    public void OnClickStartDownload()
    {
        StartCoroutine(DownloadFile(Url, Application.streamingAssetsPath + "/MP4/00-效果.mp4", CallBack));
    }


    /// <summary>
    /// 协程：下载文件
    /// </summary>
    /// <param name="url">请求的Web地址</param>
    /// <param name="filePath">文件保存路径</param>
    /// <param name="callBack">下载完成的回调函数</param>
    /// <returns></returns>
    IEnumerator DownloadFile(string url, string filePath, Action callBack)
    {
        UnityWebRequest huwr = UnityWebRequest.Head(url); //Head方法可以获取到文件的全部长度
        yield return huwr.SendWebRequest();
        if (huwr.isNetworkError || huwr.isHttpError) //如果出错
        {
            Debug.Log(huwr.error); //输出 错误信息
        }
        else
        {
            Debug.Log("开始下载"); //输出 下载信息
            long totalLength = long.Parse(huwr.GetResponseHeader("Content-Length")); //首先拿到文件的全部长度
            string dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath)) //判断路径是否存在
            {
                Directory.CreateDirectory(dirPath);
            }

            //创建一个文件流，指定路径为filePath,模式为打开或创建，访问为写入
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                long nowFileLength = fs.Length; //当前文件长度
                print(fs.Length);
                if (nowFileLength < totalLength)
                {
                    print("还没下载完成");
                    fs.Seek(nowFileLength, SeekOrigin.Begin);       //从头开始索引，长度为当前文件长度
                    UnityWebRequest uwr = UnityWebRequest.Get(url); //创建UnityWebRequest对象，将Url传入
                    uwr.SetRequestHeader("Range", "bytes=" + nowFileLength + "-" + totalLength);
                    uwr.SendWebRequest();                      //开始请求
                    if (uwr.isNetworkError || uwr.isHttpError) //如果出错
                    {
                        Debug.Log(uwr.error); //输出 错误信息
                    }
                    else
                    {
                        long index = 0;     //从该索引处继续下载
                        while (!uwr.isDone) //只要下载没有完成，一直执行此循环
                        {
                            if (_isStop) break;
                            yield return null;
                            byte[] data = uwr.downloadHandler.data;
                            if (data != null)
                            {
                                long length = data.Length - index;
                                fs.Write(data, (int)index, (int)length); //写入文件
                                index += length;
                                nowFileLength += length;
                                ProgressBar.value = (float)nowFileLength / totalLength;
                                SliderValue.text = Math.Floor((float)nowFileLength / totalLength * 100) + "%";
                                if (nowFileLength >= totalLength) //如果下载完成了
                                {
                                    ProgressBar.value = 1; //改变Slider的值
                                    SliderValue.text = 100 + "%";
                                    callBack?.Invoke();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 下载完成后的回调函数
    /// </summary>
    void CallBack()
    {
        print("下载完成");
    }

    /// <summary>
    /// 暂停下载
    /// </summary>
    public void OnClickStop()
    {
        if (_isStop)
        {
            pauseBtn.GetComponentInChildren<Text>().text = "暂停下载";
            print("继续下载");
            _isStop = !_isStop;
            OnClickStartDownload();
        }
        else
        {
            pauseBtn.GetComponentInChildren<Text>().text = "继续下载";
            print("暂停下载");
            _isStop = !_isStop;
        }
    }
}