using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class httpTest : MonoBehaviour
{
    private Action<UnityWebRequest> onSuccess;

    [Obsolete]
    private void Start()
    {

        this.onSuccess += this.SuccessMethod;
        HttpWrapper hw = GetComponent<HttpWrapper>();
        // hw.GET("http://www.baidu.com", this.onSuccess);
         //hw.POST("http://www.baidu.com",null,this.onSuccess);
        hw.Put("http://www.baidu.com", "Chinar的测试数据", this.onSuccess);

    }

    private void SuccessMethod(UnityWebRequest request)
    {
        //if (request == null)
        //    return;
        //Texture tex = DownloadHandlerTexture.GetContent(request);
        //GetComponent<MeshRenderer>().material.mainTexture = tex;
        Debug.Log("发送成功");
    }

}

