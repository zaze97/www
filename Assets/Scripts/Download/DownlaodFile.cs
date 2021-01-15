using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

//下载资源文件，支持断点续传
public class DownlaodFile : MonoBehaviour {

    private static DownlaodFile instance;
    public static DownlaodFile _Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("Download").AddComponent<DownlaodFile>();
            }
            return instance;
        }
    }

    public Dictionary<string, UnityWebRequest> listRequest = new Dictionary<string, UnityWebRequest>();    //下载请求的列表

    /// <summary>
    /// 开始下载资源
    /// </summary>
    /// <param name="url">资源的url</param>
    /// <param name="savePath">下载后保存文件的绝对路径，包含文件名称和扩展名</param>
    public _DownloadHandler StartDownload(string url, string savePath)
    {
        if (listRequest.ContainsKey(url))
        {
            Debug.Log("下载列表已经存在路径=>" + url);
            //return listRequest[url].downloadHandler as _DownloadHandler;
            return null;    //如果已经存在，则返回null
        }
        _DownloadHandler loadHandler = new _DownloadHandler(savePath);
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.chunkedTransfer = true;
        request.disposeDownloadHandlerOnDispose = true;
        request.SetRequestHeader("Range", "bytes=" + loadHandler.DownedLength + "-"); //断点续传设置读取文件数据流开始索引，成功会返回206
        request.downloadHandler = loadHandler;
        //request.useHttpContinue = true; //默认就是true
        request.Send(); //协程操作，可以在协程中调用等待
        listRequest.Add(url, request);   //保存下载的请求
        return loadHandler;
    }

    //停止下载操作
    public void StopDownload(string url)
    {
        UnityWebRequest request = null;
        if (!listRequest.TryGetValue(url, out request))
        {
            Debug.Log("不存在下载的请求=>" + url);
            return;
        }
        listRequest.Remove(url);
        (request.downloadHandler as _DownloadHandler).OnDispose();  //释放文件操作的资源
        request.Abort();    //中止下载
        request.Dispose();  //释放

    }

    private void Update()
    {
        //释放下载完成的操作
        List<string> removeList = new List<string>();
        foreach (string url in listRequest.Keys)
        {
            UnityWebRequest request = listRequest[url];
            if (request.isDone)
            {
                Debug.Log(request.responseCode);
                request.Dispose();
                removeList.Add(url);
            }
        }

        for (int i = 0; i < removeList.Count; i++)
        {
            listRequest.Remove(removeList[i]);
        }
        removeList.Clear();
    }

    void OnApplicationQuit()
    {
        //释放下载完成的操作
        foreach (string url in listRequest.Keys)
        {
            (listRequest[url].downloadHandler as _DownloadHandler).OnDispose();  //释放资源
            listRequest[url].Dispose();
        }
        listRequest.Clear();
    }

}
