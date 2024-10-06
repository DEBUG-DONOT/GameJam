using UnityEngine;

namespace NotSlot.HandPainted2D
{
  public partial class CameraController
  {
    private sealed class State
    {
      #region Properties

      public float Yaw { get; set; }

      public float Pitch { get; set; }

      public float Roll { get; set; }

      public float X { get; set; }

      public float Y { get; set; }

      #endregion


      #region Methods

      public void SetFromTransform (Transform aTransform)
      {
        Vector3 euler = aTransform.eulerAngles;
        Pitch = euler.x;
        Yaw = euler.y;
        Roll = euler.z;

        Vector3 position = aTransform.position;
        X = position.x;
        Y = position.y;
      }

      public void Translate (Vector3 translation)
      {
        Vector3 rotatedTranslation =
          Quaternion.Euler(Pitch, Yaw, Roll) * translation;

        X += rotatedTranslation.x;
        Y += rotatedTranslation.y;
      }

      public void LerpTowards (State target,
                               float positionLerpPct,
                               float rotationLerpPct)
      {
        Yaw = Mathf.Lerp(Yaw, target.Yaw, rotationLerpPct);
        Pitch = Mathf.Lerp(Pitch, target.Pitch, rotationLerpPct);
        Roll = Mathf.Lerp(Roll, target.Roll, rotationLerpPct);

        X = Mathf.Lerp(X, target.X, positionLerpPct);
        Y = Mathf.Lerp(Y, target.Y, positionLerpPct);
      }

      public void LimitToBounds (float top,
                                 float left,
                                 float right,
                                 float bottom)
      {
        X = Mathf.Clamp(X, left, right);
        Y = Mathf.Clamp(Y, bottom, top);
      }

      public void UpdateTransform (Transform aTransform)
      {
        aTransform.eulerAngles = new Vector3(Pitch, Yaw, Roll);
        aTransform.position = new Vector3(X, Y, aTransform.position.z);
      }

      #endregion
    }
  }
}