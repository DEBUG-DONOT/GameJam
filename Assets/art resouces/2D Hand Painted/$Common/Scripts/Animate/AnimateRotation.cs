using UnityEngine;
using Random = UnityEngine.Random;

namespace NotSlot.HandPainted2D
{
  [AddComponentMenu("2D Hand Painted/Animate/Rotation")]
  public sealed class AnimateRotation : MonoBehaviour
  {
    #region Inspector

    [SerializeField]
    private float speed = 3;

    #endregion


    #region MonoBehaviour

    public void Start ()
    {
      Transform myTransform = transform;
      Vector3 euler = myTransform.eulerAngles;
      euler.z = Random.Range(0, 360);
      myTransform.eulerAngles = euler;
    }

    private void Update ()
    {
      Transform myTransform = transform;
      Vector3 euler = myTransform.eulerAngles;
      euler.z += Time.deltaTime * speed;
      myTransform.eulerAngles = euler;
    }

    #endregion
  }
}