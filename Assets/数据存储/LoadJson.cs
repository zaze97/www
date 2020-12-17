using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadJson : MonoBehaviour
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
        button_save = GameObject.Find("Button_Save").GetComponent<Button>();
        button_load = GameObject.Find("Button_Load").GetComponent<Button>();
        image = GameObject.Find("Image").GetComponent<Image>();
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
        button_save.onClick.AddListener(SaveByJson);
        button_load.onClick.AddListener(LoadByJson);
    }

    public Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.image = image.sprite.name;
        save.Num = GameManager.instance.num;
        return save;
    }
    private void SaveByJson()
    {
        Save save = CreateSaveGameObject();
        string Json = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/JsonData.text");
        sw.Write(Json);
        sw.Close();
        Debug.Log("-=-=-=-=保存成功-=-=-=-=");
    }
    private void LoadByJson()
    {
        if (File.Exists(Application.dataPath + "/JsonData.text"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/JsonData.text");
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save=JsonUtility.FromJson<Save>(JsonString);
            text.text = save.Num.ToString();
            image.sprite = Resources.Load(save.image) as Sprite;
            Debug.Log("-=-=-=-=读取成功-=-=-=-=");
        }
        else
        {
            Debug.Log("读取失败");
        }
    }
}
