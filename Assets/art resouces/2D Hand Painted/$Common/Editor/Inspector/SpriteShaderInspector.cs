using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace NotSlot.HandPainted2D.Editor
{
  public sealed class SpriteShaderInspector : ShaderGUI
  {
    #region ShaderGUI

    public override void OnGUI (MaterialEditor materialEditor,
                                MaterialProperty[] properties)
    {
      // Fog
      MaterialProperty enableFog = FindProperty("_Fog", properties);
      enableFog.floatValue = EditorGUILayout.ToggleLeft(
        new GUIContent("Fog Coloring",
                       "Color sprites based on Z depth position."),
        enableFog.floatValue > 0, EditorStyles.boldLabel)
        ? 1
        : 0;

      EditorGUILayout.Space();
      EditorGUI.indentLevel++;
      EditorGUI.BeginDisabledGroup(enableFog.floatValue < 1);

      EditorGUILayout.LabelField("Background", EditorStyles.miniBoldLabel);

      MaterialProperty backgroundColor =
        FindProperty("_BackgroundColor", properties);
      backgroundColor.colorValue =
        EditorGUILayout.ColorField("Color", backgroundColor.colorValue);

      GUIContent[] rangeSubLabels =
      {
        new GUIContent("From"), new GUIContent("To")
      };
      GUIContent rangeLabel = new GUIContent("Z Range");
      float vector2Height =
        EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector2, rangeLabel);
      Rect rect = EditorGUILayout.GetControlRect(true, vector2Height);
      MaterialProperty backgroundRange =
        FindProperty("_BackgroundRange", properties);
      float[] values =
      {
        backgroundRange.vectorValue.x, backgroundRange.vectorValue.y
      };
      EditorGUI.MultiFloatField(rect, rangeLabel, rangeSubLabels, values);
      backgroundRange.vectorValue = new Vector4(values[0], values[1], 0, 0);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Foreground", EditorStyles.miniBoldLabel);

      MaterialProperty foregroundColor =
        FindProperty("_ForegroundColor", properties);
      foregroundColor.colorValue =
        EditorGUILayout.ColorField("Color", foregroundColor.colorValue);

      rect = EditorGUILayout.GetControlRect(true, vector2Height);
      MaterialProperty foregroundRange =
        FindProperty("_ForegroundRange", properties);
      values = new[]
      {
        foregroundRange.vectorValue.x, foregroundRange.vectorValue.y
      };
      EditorGUI.MultiFloatField(rect, rangeLabel, rangeSubLabels, values);
      foregroundRange.vectorValue = new Vector4(values[0], values[1], 0, 0);

      EditorGUI.EndDisabledGroup();
      EditorGUI.indentLevel--;

      EditorGUILayout.Space();
      EditorGUILayout.Space();

      // Flame
      MaterialProperty flameMap = FindProperty("_FlameMap", properties, false);
      if ( flameMap != null )
      {
        EditorGUILayout.LabelField("Flame", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        MaterialProperty flameColor = FindProperty("_FlameColor", properties);
        flameColor.floatValue = EditorGUILayout.ToggleLeft(
          new GUIContent("Blue", "Blue flame instead of orange."),
          flameColor.floatValue > 0)
          ? 1
          : 0;

        MaterialProperty flameBottom = FindProperty("_FlameBottom", properties);
        flameBottom.floatValue = EditorGUILayout.ToggleLeft(
          new GUIContent("Fade Bottom"), flameBottom.floatValue > 0)
          ? 1
          : 0;

        materialEditor.TextureProperty(flameMap, "Noise Map");
        EditorGUI.indentLevel--;
      }

      // Glitter
      MaterialProperty glitterMap =
        FindProperty("_GlitterMap", properties, false);
      if ( glitterMap != null )
        materialEditor.TextureProperty(glitterMap, glitterMap.displayName);

      // Noise Glow
      MaterialProperty glowBlend =
        FindProperty("_GlowBlend", properties, false);
      if ( glowBlend != null )
      {
        MaterialProperty glowScale = FindProperty("_GlowScale", properties);
        MaterialProperty glowSpeed = FindProperty("_GlowSpeed", properties);

        EditorGUILayout.LabelField("Glow", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        materialEditor.FloatProperty(glowBlend, "Blend");
        materialEditor.FloatProperty(glowScale, "Scale");
        materialEditor.FloatProperty(glowSpeed, "Speed");
        EditorGUI.indentLevel--;
      }

      // Stream
      MaterialProperty streamSpeed = FindProperty("_StreamSpeed", properties,
                                                  false);
      if ( streamSpeed != null )
      {
        EditorGUILayout.LabelField("Stream", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        materialEditor.FloatProperty(streamSpeed, "Speed");
        EditorGUI.indentLevel--;
      }

      // Waves
      MaterialProperty wavesSize =
        FindProperty("_WavesSize", properties, false);
      if ( wavesSize != null )
      {
        MaterialProperty wavesSpeed = FindProperty("_WavesSpeed", properties);
        MaterialProperty wavesScale =
          FindProperty("_WavesScale", properties, false);

        EditorGUILayout.LabelField("Waves", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        materialEditor.FloatProperty(wavesSize, "Size");
        materialEditor.FloatProperty(wavesSpeed, "Speed");
        materialEditor.FloatProperty(wavesScale, "Scale");
        EditorGUI.indentLevel--;
      }

      EditorGUILayout.Space();
      EditorGUILayout.Space();

      materialEditor.SetDefaultGUIWidths();

      if ( SupportedRenderingFeatures.active.editableMaterialRenderQueue )
        materialEditor.RenderQueueField();
      materialEditor.EnableInstancingField();
      materialEditor.DoubleSidedGIField();
    }

    #endregion
  }
}