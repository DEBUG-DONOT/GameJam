using UnityEditor;
using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal partial class Installer
  {
    // ReSharper disable InconsistentNaming
    public static class Config
    {
      #region Constants

      public const int PACKS_COUNT_EXCLUDING_SAMPLE = 6;

      public const short INSTALLER_VERSION = 1;

      public const short TAGS_LAYERS_VERSION = 1;

      public const string ROOT_FOLDER = "Assets/2D Hand Painted";

      public const string COMMON_FOLDER = ROOT_FOLDER + "/$Common";

      public const string TAGS_PRESET = ROOT_FOLDER + "/TagsLayers.preset";

      public const string INSTALLER_FOLDER =
        COMMON_FOLDER + "/Editor/Installer";

      public const string IMAGES_FOLDER = INSTALLER_FOLDER + "/Images";

      public const string INSTALL_MENU_SCRIPT =
        INSTALLER_FOLDER + "/InstallMenu.cs";

      public const float PADDING = 12;

      public static readonly Vector2 WINDOW_SIZE = new Vector2(580, 420);

      public static readonly Rect CONTENT_RECT =
        new Rect(PADDING, 88, WINDOW_SIZE.x - 2 * PADDING, WINDOW_SIZE.y - 153);

      #endregion


      #region Style

      public static GUIStyle STYLE_HEADER;

      public static GUIStyle STYLE_STEP_ACTIVE;

      public static GUIStyle STYLE_STEP_NORMAL;

      public static GUIStyle STYLE_RICH;

      public static GUIStyle STYLE_RICH_SMALL;

      public static GUIStyle STYLE_ENJOY_BUTTON;

      public static GUIStyle STYLE_WARN;

      public static Texture2D TEXTURE_LOGO_NORMAL;

      public static Texture2D TEXTURE_LOGO_HOVER;

      public static Texture2D TEXTURE_CHECK_FALSE;

      public static Texture2D TEXTURE_CHECK_TRUE;

      public static Texture2D TEXTURE_CHECK_PROG_1;

      public static Texture2D TEXTURE_CHECK_PROG_2;

      public static Texture2D TEXTURE_CHECK_PROG_3;

      public static Texture2D TEXTURE_CHECK_PROG_4;

      public static Color COLOR_STEP_PENDING = EditorGUIUtility.isProSkin
        ? new Color(0.14f, 0.14f, 0.14f)
        : new Color(0.86f, 0.86f, 0.86f);

      public static Color COLOR_STEP_CURRENT = EditorGUIUtility.isProSkin
        ? new Color(58 / 255f, 121 / 255f, 187 / 255f)
        : new Color(60 / 255f, 118 / 255f, 191 / 255f);

      public static Color COLOR_STEP_COMPLETE = EditorGUIUtility.isProSkin
        ? new Color(70 / 255f, 90 / 255f, 100 / 255f)
        : new Color(155 / 255f, 185 / 255f, 215 / 255f);

      public static Color COLOR_DIVIDER = EditorGUIUtility.isProSkin
        ? new Color(0.16f, 0.17f, 0.18f)
        : new Color(0.74f, 0.73f, 0.72f);

      #endregion


      #region Fields

      private static bool _didInit;

      #endregion


      #region Methods

      public static void ValidateInit ()
      {
        if ( _didInit )
          return;

        STYLE_HEADER = new GUIStyle
        {
          fontSize = 22,
          fontStyle = FontStyle.Bold,
          alignment = TextAnchor.MiddleCenter,
          normal = new GUIStyleState
          {
            textColor = EditorGUIUtility.isProSkin
              ? EditorStyles.largeLabel.normal.textColor
              : new Color(0.14f, 0.15f, 0.16f)
          }
        };

        STYLE_STEP_ACTIVE = new GUIStyle(EditorStyles.label)
        {
          alignment = TextAnchor.MiddleCenter,
          fontSize = 14,
          fontStyle = FontStyle.Bold
        };

        STYLE_STEP_NORMAL = new GUIStyle(STYLE_STEP_ACTIVE)
        {
          fontStyle = FontStyle.Normal,
          normal = new GUIStyleState
          {
            textColor = EditorGUIUtility.isProSkin
              ? new Color(0.54f, 0.55f, 0.56f)
              : new Color(0.46f, 0.45f, 0.44f)
          }
        };

        STYLE_RICH = new GUIStyle(EditorStyles.label)
        {
          richText = true,
          alignment = TextAnchor.UpperLeft,
          wordWrap = true,
          fontSize = 14,
        };

        STYLE_RICH_SMALL = new GUIStyle(STYLE_RICH)
        {
          fontSize = 12
        };

        STYLE_ENJOY_BUTTON = new GUIStyle("button")
        {
          // richText = true,
          wordWrap = true,
          fontSize = 14,
        };

        STYLE_WARN = new GUIStyle(EditorStyles.label)
        {
          fontStyle = FontStyle.Italic,
          normal = new GUIStyleState
          {
            textColor = Color.yellow
          }
        };

        TEXTURE_LOGO_NORMAL =
          AssetDatabase.LoadAssetAtPath<Texture2D>($"{IMAGES_FOLDER}/logo.png");

        TEXTURE_LOGO_HOVER =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/logo-hover.png");

        TEXTURE_CHECK_FALSE =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/check-false.png");

        TEXTURE_CHECK_TRUE =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/check-true.png");

        TEXTURE_CHECK_PROG_1 =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/check-prog-1.png");

        TEXTURE_CHECK_PROG_2 =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/check-prog-2.png");

        TEXTURE_CHECK_PROG_3 =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/check-prog-3.png");

        TEXTURE_CHECK_PROG_4 =
          AssetDatabase.LoadAssetAtPath<Texture2D>(
            $"{IMAGES_FOLDER}/check-prog-4.png");

        _didInit = true;
      }

      #endregion
    }
  }
}