/*
 * 这个文件负责存储所有动画中的参数的名字，还有其他的宏
 * animator.SetFloat("move",speed);
 * animator.Set<Variable type>("variable name",variable value)
 * 类中的string存储与动画参数同名的变量，将变量的类型写在注释中
*/
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEngine;
public class HexagonDirection
{
    //public static Vector3 Heax_up = new Vector3(0, 1, 0);
    static Vector3 Heax_left = new Vector3(-Mathf.Sqrt(3.0f)/2-0.2f, 0,0);
    static Vector3 Heax_leftUp=new Vector3(-0.5f, 1);
    static Vector3 Heax_leftDown = new Vector3(-0.5f, -1);
    static Vector3 Heax_Right = new Vector3(Mathf.Sqrt(3.0f)/2+0.2f, 0);
    static Vector3 Heax_RightDown = new Vector3(0.5f, -1);
    static Vector3 Heax_RightUp = new Vector3(0.5f, 1);

    public static int GetIndex(Vector3 position)
    {
        for (int i = 0; i < 6; i++)
        {
            if(position==Heax_Directions[i]) return i;
        }
        return -1;
    }
    public static List<Vector3> Heax_Directions = new List<Vector3> {Heax_left,Heax_Right,Heax_leftUp,Heax_RightUp,Heax_leftDown,Heax_RightDown};

}

public class CheckClick
{
    public static GameObject CheckClickOnSomething()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 ray=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit=Physics2D.Raycast(ray,Vector2.zero);
            if(hit.collider != null)
            {
                GameObject go=hit.collider.gameObject;
                //Debug.Log("click on"+go.name);
                return go;
            }
            else return null;
        }
        else return null;
    }
}

public class AxisMacro
{
    public static string HorizontalString = "Horizontal";
    public static string VerticalString = "Vertical";
    
}


public class PlayerAnimatorMacro
{
    //所有的宏都应该是static的
    public static string idle="idle";//bool
    //或者run，一个表示移动的动画宏
    public static string move="move";//float
    public static string jump = "jump"; //bool

}

public class CharacterAnimatorMacro
{
    public static string idle="idle";//bool
    public static string move="move";//float

}
//具体的enimy或者npc可以从上面的CharacterAnimatorMacro派生