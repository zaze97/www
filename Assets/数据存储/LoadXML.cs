using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
public class LoadXML : MonoBehaviour
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
        button_save.onClick.AddListener(SaveByXML);
        button_load.onClick.AddListener(LoadByXML);
    }
    public Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.image = image.sprite.name;
        save.Num = GameManager.instance.num;
        return save;
    }
    private void SaveByXML()
    {
        Save save = CreateSaveGameObject();
        XmlDocument xmlDocument = new XmlDocument();

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        #region 保存数据
        XmlElement NumElement = xmlDocument.CreateElement("Num");
        NumElement.InnerText = save.Num.ToString();
        root.AppendChild(NumElement);

        XmlElement ImageElement = xmlDocument.CreateElement("image");
        ImageElement.InnerText = save.image.ToString();
        root.AppendChild(ImageElement);


        //数组存储
        XmlElement bat, batPositionX, batPositionY;
        for (int i = 0; i < save.enemyPositionX.Count; i++)
        {
            bat = xmlDocument.CreateElement("Bat");
            batPositionX = xmlDocument.CreateElement("BatPositionX");
            batPositionY = xmlDocument.CreateElement("BatPositionY");

            batPositionX.InnerText = save.enemyPositionX[i].ToString();
            batPositionY.InnerText = save.enemyPositionY[i].ToString();

            bat.AppendChild(batPositionX);
            bat.AppendChild(batPositionY);
            root.AppendChild(bat);
        }
        #endregion

        xmlDocument.AppendChild(root);
        Debug.Log(Application.dataPath + "/DataXML.text");
        xmlDocument.Save(Application.dataPath + "/DataXML.text");
        if (File.Exists(Application.dataPath + "/DataXML.text"))
        {
            Debug.Log("保存成功");
        }
    }
    private void LoadByXML()
    {
        if (File.Exists(Application.dataPath + "/DataXML.text")){
            Save save = new Save();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/DataXML.text");

            #region 读取数据
            XmlNodeList Num = xmlDocument.GetElementsByTagName("Num");
            int num = int.Parse(Num[0].InnerText);
            save.Num = num;
            text.text = save.Num.ToString();

            XmlNodeList image = xmlDocument.GetElementsByTagName("image");
            string Image = image[0].InnerText;
            save.image = Image;


            XmlNodeList bats = xmlDocument.GetElementsByTagName("Bat");
            if (bats.Count != 0)
            {
                for(int i = 0; i < bats.Count; i++)
                {
                    XmlNodeList batPosX = xmlDocument.GetElementsByTagName("BatPositionX");
                    int batPositionX = int.Parse(batPosX[i].InnerText);
                    save.enemyPositionX.Add(batPositionX);

                    XmlNodeList batPosY = xmlDocument.GetElementsByTagName("BatPositionY");
                    int batPositionY = int.Parse(batPosY[i].InnerText);
                    save.enemyPositionY.Add(batPositionY);
                }
            }
            #endregion


            Debug.Log("读取成功");
        }
        else
        {
            Debug.Log("文件不存在");
        }
    }


}

