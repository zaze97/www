using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorTest))]
[ExecuteInEditMode]

public class MyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //获取EditorTest类的实例
        EditorTest editorTest = (EditorTest)target;
        //绘制一个窗口
        editorTest.mRectValue = EditorGUILayout.RectField("窗口坐标",editorTest.mRectValue);
        //绘制一个贴图槽
        editorTest.texture=EditorGUILayout.ObjectField("增加一个贴图", editorTest.texture, typeof(Texture), true) as Texture;
    }
}
