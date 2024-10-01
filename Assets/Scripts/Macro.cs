/*
 * 这个文件负责存储所有动画中的参数的名字，还有其他的宏
 * animator.SetFloat("move",speed);
 * animator.Set<Variable type>("variable name",variable value)
 * 类中的string存储与动画参数同名的变量，将变量的类型写在注释中
*/

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