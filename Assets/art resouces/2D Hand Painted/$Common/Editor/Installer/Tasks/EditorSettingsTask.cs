using System;
using UnityEditor;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class EditorSettingsTask : Installer.Task
  {
    #region Class

    public EditorSettingsTask (Action onComplete) : base(onComplete)
    {
    }

    #endregion


    #region InstallerTask

    public override void Perform ()
    {
      EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;
      Complete();
    }

    #endregion
  }
}