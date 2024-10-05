namespace NotSlot.HandPainted2D.Editor
{
  internal partial class Installer
  {
    public abstract class Screen
    {
      #region Properties

      public string ContinueButtonTitle => OverrideContinueTitle ?? "Continue";

      public abstract string ScreenTitle { get; }

      public abstract bool CanContinue { get; }

      public abstract bool CanClose { get; }

      public virtual bool Repaint => false;

      public virtual string OverrideContinueTitle => null;

      #endregion


      #region Methods

      public abstract void Tick ();

      public virtual void WillSerialize ()
      {
      }

      public virtual void DidDeserialize ()
      {
      }

      #endregion
    }
  }
}