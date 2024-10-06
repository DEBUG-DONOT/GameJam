using UnityEngine;
using Random = UnityEngine.Random;

namespace NotSlot.HandPainted2D
{
  [AddComponentMenu("2D Hand Painted/Animate/Swing")]
  public sealed class AnimateSwing : MonoBehaviour
  {
    #region Inspector

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float amount = 1;

    #endregion


    #region Fields

    private float _base;

    private float _counter;

    #endregion


    #region MonoBehaviour

    private void Start ()
    {
      _base = transform.eulerAngles.z;
      _counter = Random.Range(0, 10);
    }

    private void Update ()
    {
      _counter += Time.deltaTime * speed;

      Vector3 euler = transform.eulerAngles;
      euler.z = _base + Mathf.Sin(_counter) * amount;
      transform.eulerAngles = euler;
    }

    #endregion
  }
}