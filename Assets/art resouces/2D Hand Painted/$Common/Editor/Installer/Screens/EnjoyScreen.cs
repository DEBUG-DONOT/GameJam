using UnityEngine;

namespace NotSlot.HandPainted2D.Editor
{
  internal sealed class EnjoyScreen : Installer.Screen
  {
    #region Properties

    public override string ScreenTitle => "Enjoy!";

    public override bool CanContinue => true;

    public override bool CanClose => true;

    #endregion


    #region InstallerScreen

    public override void Tick ()
    {
      if ( Installer.OptionPackageShapes )
      {
        Rect preRect = Installer.Config.CONTENT_RECT;
        preRect.width -= 120;
        preRect.x += 60;
        const string textPre =
          "<color='yellow'><b>PLEASE RE-START UNITY TO COMPLETE THE INSTALLATION!</b>\n" +
          "Sprite Shapes won’t appear until closing and opening Unity again.</color>";
        GUI.Label(preRect, textPre, Installer.Config.STYLE_RICH);
      }

      Rect rect = Installer.Config.CONTENT_RECT;
      rect.width /= 2;
      rect.x += rect.width / 2;
      rect.y += Installer.Config.PADDING + (Installer.OptionPackageShapes ? 48 : 0);
      rect.height = 64;

      if ( GUI.Button(rect, "Online Manual & Support",
                      Installer.Config.STYLE_ENJOY_BUTTON) )
        Application.OpenURL(Installer.DocsLink);

      rect.y += rect.height + 2 * Installer.Config.PADDING;
      rect.height = 160;
      float addition = rect.width / 2;
      rect.x -= addition / 2;
      rect.width += addition;

      const string text =
        "<b>We strive to provide you with the highest - quality assets possible;</b> " +
        "if you find something missing, unclear, or have a suggestion, please contact us – we are here to assist, help, and support!" +
        "\n\n<i>Offline manual is also available. Look for the READ ME file inside the 2D Hand Painted folder.</i>";
      GUI.Label(rect, text, Installer.Config.STYLE_RICH);
    }

    #endregion
  }
}