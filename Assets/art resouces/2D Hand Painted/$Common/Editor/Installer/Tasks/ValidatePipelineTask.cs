using System;
using UnityEditor;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class ValidatePipelineTask : Installer.Task
  {
    #region Class

    public ValidatePipelineTask (Action onComplete) : base(onComplete)
    {
    }

    #endregion


    #region InstallerTask

    public override void Perform ()
    {
      string[] guids = AssetDatabase.FindAssets("t:Renderer2DData");
      foreach ( string guid in guids )
      {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        UnityEngine.Object renderer = AssetDatabase.LoadMainAssetAtPath(path);
        SerializedObject serialized = new SerializedObject(renderer);
        SerializedProperty prop =
          serialized.FindProperty("m_TransparencySortMode");
        prop.intValue = 2; // Orthographic
        serialized.ApplyModifiedProperties();
      }

      AssetDatabase.SaveAssets();
      Complete();
    }

    #endregion
  }
}