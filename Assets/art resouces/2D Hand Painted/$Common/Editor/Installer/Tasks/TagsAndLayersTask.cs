using System;
using UnityEditor;
using UnityEditor.Presets;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class TagsAndLayersTask : Installer.Task
  {
    #region Class

    public TagsAndLayersTask (Action onComplete) : base(onComplete)
    {
    }

    #endregion


    #region InstallerTask

    public override void Perform ()
    {
      if ( Installer.BundleMeta.TagsLayersVersion >=
           Installer.Config.TAGS_LAYERS_VERSION )
      {
        Complete();
        return;
      }

      UnityEngine.Object assetTags =
        AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset");
      if ( assetTags == null )
      {
        Complete();
        return;
      }

      Preset assetSource =
        AssetDatabase.LoadAssetAtPath<Preset>(Installer.Config.TAGS_PRESET);
      if ( assetSource == null )
      {
        Complete();
        return;
      }

      assetSource.ApplyTo(assetTags);
      Installer.BundleMeta.TagsLayersVersion =
        Installer.Config.TAGS_LAYERS_VERSION;
      AssetDatabase.SaveAssets();

      Complete();
    }

    #endregion
  }
}