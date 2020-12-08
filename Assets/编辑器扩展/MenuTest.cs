using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MenuTest : MonoBehaviour
{

    //为菜单栏增加一个名为Do Something的菜单项
    [MenuItem("MyMenu/Do Something")]
    static void DoSomething()
    {
        Debug.Log("Doing Something...");
    }


    // 被激活的菜单项
    //在菜单栏中增加一个叫作“Log Selected Transform Name”的菜单项
    //我们使用下面的静态方法ValidateLogSelectedTransformName来激活该菜单项
      //因此，只有当我们选择了一个transform时，该菜单项才可用
      [MenuItem("MyMenu/Log Selected Transform Name")]
      static void LogSelectedTransformName()
    {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
    }

    //激活上面的静态方法所定义的菜单项
    //如果该方法返回的false，则菜单项不激活
    [MenuItem("MyMenu/Log Selected Transform Name", true)]
    static bool ValidateLogSelectedTransformName()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }


    //在菜单栏中增加一个叫作“Log Selected Transform Name”的菜单项
    //并且为它增加一个快捷键（在Windows操作系统是“Ctrl+g”，在OS X
    //操作系统上是“cmd+g”）
    [MenuItem("MyMenu/Do Something with a Shortcut Key %g")]
    static void DoSomethingWithAShortcutKey()
    {
        Debug.Log("Doing something with a Shortcut Key...");
    }
    //为Rigidbody的context菜单增加一个名为Double Mass的菜单项
    [MenuItem("CONTEXT/Rigidbody/Double Mass")]
    static void DoubleMass(MenuCommand command)
    {
        Rigidbody body = (Rigidbody)command.context;
        body.mass = body.mass * 2;
        Debug.Log("Doubled Rigidbody's Mass to " + body.mass + " from Context Menu.");
    }


    //增加一个菜单项用来创建自定义的GameObjects
    [MenuItem("GameObject/MyCategory/Custom Game Object", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("Custom Game Object");
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}

