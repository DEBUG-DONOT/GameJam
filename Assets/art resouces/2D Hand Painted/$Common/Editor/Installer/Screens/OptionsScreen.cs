using UnityEditor;
using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class OptionsScreen : Installer.Screen
  {
    #region Properties

    public override string ScreenTitle => "Config";

    public override bool CanContinue => true;

    public override bool CanClose => false;

    #endregion


    #region InstallerScreen

    public override void Tick ()
    {
      GUI.BeginGroup(Installer.Config.CONTENT_RECT);

      //
      // Render Pipeline
      //

      string pipelineName = null;
      switch ( Installer.DetectedPipeline )
      {
        case Installer.Renderer.BuiltIn:
          pipelineName = "Built-in";
          break;
        case Installer.Renderer.Universal2D:
          pipelineName = "Universal (2D)";
          break;
      }

      GUILayout.Label("Render Pipeline", EditorStyles.boldLabel);
      GUILayout.Label(
        $"Detected render pipeline: <b>{pipelineName} Render Pipeline</b>, assets will be configured accordingly.",
        Installer.Config.STYLE_RICH_SMALL);

      //
      // Layers
      //

      EditorGUILayout.Space();
      Installer.OptionLayers = EditorGUILayout.ToggleLeft(
        "Tags & Layers preset", Installer.OptionLayers, EditorStyles.boldLabel);
      GUILayout.Label(
        "Our demo scenes use the following sorting layers: Background, Default, and Foreground.");
      if ( !Installer.OptionLayers )
        GUILayout.Label(
          "Without using the Tags & Layers preset the demo scenes will not display correctly.",
          Installer.Config.STYLE_WARN);

      //
      // Dependencies
      //

      EditorGUILayout.Space();
      GUILayout.Label("Install Dependencies", EditorStyles.boldLabel);
      Installer.OptionPackageShapes = EditorGUILayout.ToggleLeft(
        "2D Sprite Shapes", Installer.OptionPackageShapes);
      if ( !Installer.OptionPackageShapes )
        GUILayout.Label(
          "Sprite Shapes package is required to use the shapes provided by this asset.\nDemo scenes will not display correctly.",
          Installer.Config.STYLE_WARN);

      GUI.EndGroup();
    }

    #endregion
  }
}