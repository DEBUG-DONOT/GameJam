using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class IntroScreen : Installer.Screen
  {
    #region Fields

    private bool _canContinue;

    private bool _canClose;

    #endregion


    #region Properties

    public override string ScreenTitle => "Intro";

    public override bool CanContinue => _canContinue;

    public override bool CanClose => _canClose;

    #endregion


    #region InstallerScreen

    public override void Tick ()
    {
      string text =
        $"<b>Thank you for downloading the {Installer.Title}!</b>\n\n" +
        "The following screens will guide you through configuring and installing the assets to best suit your needs.";

      // We constantly check for new pipeline, allowing the user to start the
      // installation as soon as the pipeline is configured.
      if ( !_canContinue )
        DetectPipeline();

      switch ( Installer.DetectedPipeline )
      {
        case Installer.Renderer.UniversalForward:
          _canContinue = false;
          _canClose = true;
          text +=
            "\n\n<color=orange>Please configure the Universal Render Pipeline’s renderer to <b>2D Renderer</b>.</color>";
          break;
        case Installer.Renderer.BuiltIn:
          _canContinue = false;
          _canClose = true;
          text +=
            "\n\n<color=red>Built-in (standard) Renderer is not supported. Please install and configure the Universal Render Pipeline.</color>";
          break;
        case Installer.Renderer.Hd:
          _canContinue = false;
          _canClose = true;
          text +=
            "\n\n<color=red>High-definition Render Pipeline is not supported. Please install and configure the Universal Render Pipeline.</color>";
          break;
        case Installer.Renderer.Unrecognized:
          _canContinue = false;
          _canClose = true;
          text +=
            "\n\n<color=red>This Render Pipeline is not supported. Please install and configure the Universal Render Pipeline.</color>";
          break;
        case Installer.Renderer.Universal2D:
          if ( Installer.DetectedPipelineAssetCount > 1 )
            text +=
              "\n\n<color=yellow>Make sure to delete unused Render Pipeline Assets to prevent corrupted rendering.</color>";
          goto default;
        default:
          _canContinue = true;
          _canClose = false;
          break;
      }

      if ( _canContinue == false )
        text +=
          "\n\nAfter configuring the render pipeline, you can open this Installer window through the menu `2D Hand Painted / Install`.";
      else
        text += "\n\n<i>It may take a few minutes...</i>";

      // https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html
      Rect textRect = Installer.Config.CONTENT_RECT;
      textRect.height -= 85;
      GUI.Label(textRect, text, Installer.Config.STYLE_RICH);

      if ( _canContinue )
        return;

      const string buttonLink =
#if UNITY_2021_2_OR_NEWER
        "https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.0/manual/Setup.html";
#elif UNITY_2021_1_OR_NEWER
        "https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@11.0/manual/Setup.html";
#else // UNITY_2020_1_OR_NEWER
        "https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@10.4/manual/Setup.html";
#endif

      Rect buttonRect = textRect;
      buttonRect.height = 60;
      buttonRect.x = buttonRect.width / 4;
      buttonRect.width /= 2;
      buttonRect.y = textRect.y + textRect.height + 15;

      if ( GUI.Button(buttonRect, "Pipeline Setup Guide",
                      Installer.Config.STYLE_ENJOY_BUTTON) )
        Application.OpenURL(buttonLink);
    }

    #endregion


    #region Methods

    private static void DetectPipeline ()
    {
      Installer.DetectedPipelineAssetCount = 0;
      RenderPipelineAsset pipelineAsset = GraphicsSettings.renderPipelineAsset;
      if ( pipelineAsset == null )
      {
        Installer.DetectedPipeline = Installer.Renderer.BuiltIn;
        return;
      }

      Type pipelineAssetType = pipelineAsset.GetType();
      string typeName = pipelineAssetType.Name;
      if ( typeName.Contains("HDRenderPipelineAsset") )
      {
        Installer.DetectedPipeline = Installer.Renderer.Hd;
        return;
      }

      if ( !typeName.Contains("UniversalRenderPipelineAsset") )
      {
        Installer.DetectedPipeline = Installer.Renderer.Unrecognized;
        return;
      }

      PropertyInfo prop = pipelineAssetType.GetProperty("scriptableRenderer");
      if ( prop == null )
      {
        Installer.DetectedPipeline = Installer.Renderer.Unrecognized;
        return;
      }

      Installer.DetectedPipelineAssetCount = AssetDatabase
                                             .FindAssets(
                                               "t:UniversalRenderPipelineAsset")
                                             .Length;
      string rendererName = prop.GetValue(pipelineAsset).GetType().Name;
      Installer.DetectedPipeline = rendererName.Contains("Renderer2D")
        ? Installer.Renderer.Universal2D
        : Installer.Renderer.UniversalForward;
    }

    #endregion
  }
}