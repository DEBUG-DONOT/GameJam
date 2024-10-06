using System;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class SpriteShapesTask : Installer.Task
  {
    #region Class

    public SpriteShapesTask (Action onComplete) : base(onComplete)
    {
    }

    #endregion


    #region InstallerTask

    public override void Perform ()
    {
      const string packAddress = "com.unity.2d.spriteshape";
      AddRequest request = Client.Add(packAddress);
      EditorApplication.update += WaitForInstallation;

      void WaitForInstallation ()
      {
        if ( request.Status == StatusCode.InProgress )
          return;

        EditorApplication.update -= WaitForInstallation;
        Complete();
      }
    }

    #endregion
  }
}