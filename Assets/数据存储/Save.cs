using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Save 
{
    public int Num;
    public string image;


    public List<int> enemyPositionX=new List<int>() {1,2,3,4,5,6 };
    public List<int> enemyPositionY = new List<int>() { 2,6,7,23,54,2};
}
