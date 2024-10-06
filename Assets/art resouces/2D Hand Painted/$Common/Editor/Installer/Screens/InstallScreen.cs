using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class InstallScreen : Installer.Screen
  {
    #region Constants

    private const string SESSION_KEY = "installer.active-task";

    #endregion


    #region Fields

    private bool _canContinue;

    private bool _canClose;

    private bool _overrideContinueTitle = true;

    private int _progressTick;

    #endregion


    #region Fields

    private readonly List<Installer.Task> _tasks = new List<Installer.Task>();

    private int _activeTask = -1;

    private bool _pendingTask;

    private bool _didDeserialize;

    #endregion


    #region Properties

    public override string ScreenTitle => "Install";

    public override bool CanContinue => _canContinue;

    public override bool CanClose => _canClose;

    public override bool Repaint => true;

    public override string OverrideContinueTitle =>
      _overrideContinueTitle ? "Installing..." : null;

    #endregion


    #region InstallerScreen

    public override void WillSerialize ()
    {
      if ( _activeTask >= 0 )
        SessionState.SetInt(SESSION_KEY, _activeTask + 1);
    }

    public override void DidDeserialize ()
    {
      _didDeserialize = true;
    }

    public override void Tick ()
    {
      if ( _didDeserialize )
      {
        _activeTask = SessionState.GetInt(SESSION_KEY, -1);
        SessionState.EraseInt(SESSION_KEY);
        if ( _activeTask >= 0 )
          _pendingTask = true;

        _didDeserialize = false;
      }

      if ( _activeTask < 0 )
        InitTasks();

      Rect rect = Installer.Config.CONTENT_RECT;
      rect.height = 18;
      rect.x += Installer.Config.PADDING;
      rect.y += Installer.Config.PADDING;

      int taskCount = 0;

      void TaskGUI (string text)
      {
        Rect bullet = rect;
        bullet.width = 18;

        Texture2D bulletTexture = null;
        if ( taskCount == _activeTask )
          switch ( Mathf.FloorToInt(_progressTick++ % 20 / 5f) )
          {
            case 0:
              bulletTexture = Installer.Config.TEXTURE_CHECK_PROG_1;
              break;
            case 1:
              bulletTexture = Installer.Config.TEXTURE_CHECK_PROG_2;
              break;
            case 2:
              bulletTexture = Installer.Config.TEXTURE_CHECK_PROG_3;
              break;
            case 3:
              bulletTexture = Installer.Config.TEXTURE_CHECK_PROG_4;
              break;
          }
        else
          bulletTexture = taskCount < _activeTask
            ? Installer.Config.TEXTURE_CHECK_TRUE
            : Installer.Config.TEXTURE_CHECK_FALSE;

        GUI.DrawTexture(bullet, bulletTexture);
        taskCount++;

        Rect label = rect;
        label.width -= 24;
        label.x += 24;
        GUI.Label(label, text);

        rect.y += 28;
      }

      // Pipeline
      string pipelineName =
        Installer.DetectedPipeline == Installer.Renderer.BuiltIn
          ? "Built-in"
          : "Universal 2D";
      TaskGUI($"Validate pipeline: {pipelineName}");

      // Layers
      if ( Installer.OptionLayers )
        TaskGUI("Config sorting layers");

      // Packages
      if ( Installer.OptionPackageShapes )
        TaskGUI("Install 2D Sprite Shapes package");

      // Player
      TaskGUI("Config Player: Linear color space");

      // Editor
      TaskGUI("Config Editor: 2D workflow mode");

      if ( _pendingTask )
        _pendingTask = false;
      else
        return;

      // Start next task
      if ( _activeTask < _tasks.Count )
      {
        Installer.Task task = _tasks[_activeTask];
        task.Perform();
        return;
      }

      // No More tasks
      CompleteInstallation();
    }

    #endregion


    #region Methods

    private void InitTasks ()
    {
      // Pipeline
      _tasks.Add(new ValidatePipelineTask(CompleteTask));

      // Layers
      if ( Installer.OptionLayers )
        _tasks.Add(new TagsAndLayersTask(CompleteTask));

      // Packages
      if ( Installer.OptionPackageShapes )
        _tasks.Add(new SpriteShapesTask(CompleteTask));

      // Settings
      _tasks.Add(new PlayerSettingsTask(CompleteTask));
      _tasks.Add(new EditorSettingsTask(CompleteTask));

      CompleteTask();
    }

    private void CompleteTask ()
    {
      _activeTask++;
      _pendingTask = true;
    }

    private void CompleteInstallation ()
    {
      _canClose = true;
      _canContinue = true;
      _overrideContinueTitle = false;

      Installer.DocsLink = Installer.PendingPacks.Count == 1
        ? Installer.PendingPacks[0].Manual
        : "https://notslot.com/assets/2d-hand-painted/manual";

      foreach ( PackMeta pack in Installer.PendingPacks )
      {
        Installer.BundleMeta.SetVersion(pack.Name,
                                        Installer.Config.INSTALLER_VERSION);
        EditorUtility.SetDirty(Installer.BundleMeta);

        InstallerUtil.DeleteInstallerMenu();
#if !HANDPAINTED2D_SANDBOX
        string path = AssetDatabase.GetAssetPath(pack);
        AssetDatabase.DeleteAsset(path);
#endif
      }

      AssetDatabase.SaveAssets();
    }

    #endregion
  }
}