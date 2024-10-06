using System;

namespace NotSlot.HandPainted2D.Editor
{
  internal partial class Installer
  {
    internal abstract class Task
    {
      #region Fields

      private readonly Action _completionHandler;

      #endregion


      #region Class

      protected Task (Action onComplete)
      {
        _completionHandler = onComplete;
      }

      #endregion


      #region Methods

      public abstract void Perform ();

      protected void Complete ()
      {
        _completionHandler();
      }

      #endregion
    }
  }
}