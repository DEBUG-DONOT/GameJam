using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed partial class Installer : EditorWindow,
                                            ISerializationCallbackReceiver
  {
    #region Constants

    private const string DEFAULT_TITLE = "2D Hand Painted Packs";
    
    private const string TITLE_KEY = "installer.title";

    private static readonly Screen[] SCREENS =
    {
      new IntroScreen(),
      new OptionsScreen(),
      new InstallScreen(),
      new EnjoyScreen()
    };

    #endregion


    #region Fields

    private int _activeScreen = 0;

    private static bool _checkPendingInstall;

    private static readonly List<PackMeta> PENDING_PACKS = new List<PackMeta>();

    #endregion


    #region Properties

    public static string Title
    {
      get => SessionState.GetString(TITLE_KEY, DEFAULT_TITLE);
      private set => SessionState.SetString(TITLE_KEY, value);
    }

    public static BundleMeta BundleMeta { get; private set; }

    public static Renderer DetectedPipeline { get; set; }

    public static int DetectedPipelineAssetCount { get; set; }

    public static bool OptionLayers { get; set; } = true;

    public static bool OptionPackageShapes { get; set; } = true;

    public static string DocsLink { get; set; }

    public static IReadOnlyList<PackMeta> PendingPacks => PENDING_PACKS;

    private static int ScreensCount => SCREENS.Length;

    #endregion


    #region EditorWindow

    private void OnEnable ()
    {
      // Window config
      wantsMouseMove = true;
      titleContent = new GUIContent("NotSlot Installer");

      // Force window size
      minSize = Config.WINDOW_SIZE;
      maxSize = Config.WINDOW_SIZE;

      BundleMeta = BundleMeta.GetAsset();
      string[] guids =
        AssetDatabase.FindAssets("t:PackMeta", new[] { Config.ROOT_FOLDER });
      foreach ( string guid in guids )
      {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        PackMeta meta = AssetDatabase.LoadAssetAtPath<PackMeta>(path);
        if ( BundleMeta.GetVersion(meta.Name) < meta.InstallerVersion )
          PENDING_PACKS.Add(meta);
      }

      // Set title based on pending packs count
      Title = PENDING_PACKS.Count switch
      {
        Config.PACKS_COUNT_EXCLUDING_SAMPLE => "2D Hand Painted Bundle",
        1 => $"2D Hand Painted: {PENDING_PACKS[0].Name} Pack",
        _ => DEFAULT_TITLE
      };
    }

    private void OnGUI ()
    {
      // We need to make sure on each paint that the configuration is properly
      // initialized, since the editor may reload.
      Config.ValidateInit();

      DrawHeader();
      SCREENS[_activeScreen].Tick();
      DrawFooter();

      if ( Event.current.type == EventType.MouseMove ||
           SCREENS[_activeScreen].Repaint )
        Repaint();
    }

    private void OnDestroy ()
    {
      if ( SCREENS[_activeScreen].CanClose )
        return;

      // Force reopening the installer until everything is installed.
      EditorApplication.update += ShowInstaller;
    }

    #endregion


    #region ISerializationCallbackReceiver

    public void OnBeforeSerialize ()
    {
      SCREENS[_activeScreen].WillSerialize();
    }

    public void OnAfterDeserialize ()
    {
      SCREENS[_activeScreen].DidDeserialize();
    }

    #endregion


    #region Methods

    public static void ShowInstaller ()
    {
      EditorApplication.update -= ShowInstaller;
      GetWindow<Installer>(true).Show();
    }

    [InitializeOnLoadMethod]
    private static void Init ()
    {
      // Check if we need to install new packs
#if !HANDPAINTED2D_SANDBOX
      EditorApplication.update += CheckPendingInstallation;
#endif
    }

    private static void CheckPendingInstallation ()
    {
      if ( !_checkPendingInstall )
      {
        _checkPendingInstall = true;
        return;
      }

      EditorApplication.update -= CheckPendingInstallation;

      bool needInstaller = false;
      string[] guids =
        AssetDatabase.FindAssets("t:PackMeta", new[] { Config.ROOT_FOLDER });
      if ( guids.Length == 0 )
        return;

      BundleMeta bundleMeta = BundleMeta.GetAsset();
      if ( bundleMeta.HasAnotherPack(Config.INSTALLER_VERSION) )
      {
        SkipInstallation(bundleMeta, guids);
        return;
      }

      foreach ( string guid in guids )
      {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        PackMeta meta = AssetDatabase.LoadAssetAtPath<PackMeta>(path);
        if ( bundleMeta.GetVersion(meta.Name) >= meta.InstallerVersion )
          continue;

        needInstaller = true;
        break;
      }

      if ( needInstaller )
        EditorApplication.update += ShowInstaller;
      else
        InstallerUtil.DeleteInstallerMenu();
    }

    private void DrawHeader ()
    {
      Rect rect = new Rect(Config.PADDING, 0,
                           Config.WINDOW_SIZE.x - 2 * Config.PADDING, 46);
      GUI.Label(rect, Title, Config.STYLE_HEADER);
      rect.y += 44;

      float segmentWidth =
        (Config.WINDOW_SIZE.x - 4 * (ScreensCount - 1) - 2 * Config.PADDING) /
        ScreensCount;
      for ( int i = 0; i < ScreensCount; i++ )
      {
        // Title
        Rect label = new Rect(rect.x, rect.y, segmentWidth, 28);
        GUI.Label(label, SCREENS[i].ScreenTitle,
                  i == _activeScreen
                    ? Config.STYLE_STEP_ACTIVE
                    : Config.STYLE_STEP_NORMAL);

        // Line
        Color color;
        if ( i > _activeScreen )
          color = Config.COLOR_STEP_PENDING;
        else if ( i == _activeScreen )
          color = Config.COLOR_STEP_CURRENT;
        else
          color = Config.COLOR_STEP_COMPLETE;

        Rect line = new Rect(rect.x, rect.y + 28, segmentWidth, 2);
        EditorGUI.DrawRect(line, color);

        rect.x += segmentWidth + 4;
      }
    }

    private void DrawFooter ()
    {
      Rect rect = new Rect(Config.PADDING,
                           Config.WINDOW_SIZE.y - 2 * Config.PADDING - 29,
                           Config.WINDOW_SIZE.x - 2 * Config.PADDING, 1);
      EditorGUI.DrawRect(rect, Config.COLOR_DIVIDER);

      rect.y += Config.PADDING + 1;
      rect.height = 28;

      Rect logo = rect;
      logo.width = 33;
      Texture image = logo.Contains(Event.current.mousePosition)
        ? Config.TEXTURE_LOGO_HOVER
        : Config.TEXTURE_LOGO_NORMAL;
      EditorGUIUtility.AddCursorRect(logo, MouseCursor.Link);
      if ( GUI.Button(logo, image, GUIStyle.none) )
        Application.OpenURL("https://notslot.com");

      if ( _activeScreen >= ScreensCount - 1 )
        return;

      bool canContinue = SCREENS[_activeScreen].CanContinue;
      EditorGUI.BeginDisabledGroup(!canContinue);

      Rect button = rect;
      button.width = 100;
      button.x += rect.width - 100;
      if ( GUI.Button(button, SCREENS[_activeScreen].ContinueButtonTitle) )
        _activeScreen++;

      EditorGUI.EndDisabledGroup();
    }

    private static void SkipInstallation (BundleMeta bundleMeta,
                                          IEnumerable<string> guids)
    {
      foreach ( string guid in guids )
      {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        PackMeta pack = AssetDatabase.LoadAssetAtPath<PackMeta>(path);
        bundleMeta.SetVersion(pack.Name, Config.INSTALLER_VERSION);
        EditorUtility.SetDirty(bundleMeta);

#if !HANDPAINTED2D_SANDBOX
        AssetDatabase.DeleteAsset(path);
#endif
        InstallerUtil.DeleteInstallerMenu();
      }

      AssetDatabase.SaveAssets();
    }

    #endregion
  }
}