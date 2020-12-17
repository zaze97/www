using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LoadBinary : MonoBehaviour
{
    private Button button_save;
    private Button button_load;
    public Image image;
    public Text text;
    string dirPath;

    int index = 0;
    private void Awake()
    {
        dirPath = Application.dataPath + "/Data.text";
        button_save =GameObject.Find("Button_Save").GetComponent<Button>();
        button_load = GameObject.Find("Button_Load").GetComponent<Button>();
        image= GameObject.Find("Image").GetComponent<Image>();
        text = GameObject.Find("StrText").GetComponent<Text>();
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(index);
            index += 2;
            text.text = (index).ToString();
        }
    }
    private void Init()
    {
        button_save.onClick.AddListener(SaveBinary);
        button_load.onClick.AddListener(Load);
    }

    public Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.image = image.sprite.name;
        save.Num = GameManager.instance.num;
        return save;
    }
    public void SaveBinary()
    {
        Debug.Log("保存成功");
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(dirPath))  //若已经有存档文件则删除旧文件
            File.Delete(dirPath);
        FileStream fileStream = File.Create(dirPath);
        bf.Serialize(fileStream, save);
        fileStream.Close();
    }
    private void Load()
    {
        if (File.Exists(dirPath))
        {
            Debug.Log("加载成功");
            BinaryFormatter bf = new BinaryFormatter();
            //FileStream fileStream = File.Open(Application.persistentDataPath + "/Data.text", FileMode.Open);
            FileStream fileStream = File.Open(dirPath, FileMode.Open);
            Save save = bf.Deserialize(fileStream) as Save;
            fileStream.Close();
            Debug.Log(save.Num+"----"+ save.image);
            text.text = save.Num.ToString();
            image.sprite = Resources.Load(save.image) as Sprite;
        }
        else
        {
            Debug.Log("读取失败");
        }
    }
}
