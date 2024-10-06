using System;
using UnityEditor;
using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class PlayerSettingsTask : Installer.Task
  {
    #region Class

    public PlayerSettingsTask (Action onComplete) : base(onComplete)
    {
    }

    #endregion


    #region InstallerTask

    public override void Perform ()
    {
      PlayerSettings.colorSpace = ColorSpace.Linear;
      Complete();
    }

    #endregion
  }
}