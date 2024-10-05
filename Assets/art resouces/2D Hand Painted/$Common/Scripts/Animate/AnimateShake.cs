using UnityEngine;

namespace NotSlot.HandPainted2D
{
  [AddComponentMenu("2D Hand Painted/Animate/Shake")]
  public sealed class AnimateShake : MonoBehaviour
  {
    #region Inspector

    [SerializeField]
    [Range(0.01f, 10)]
    private float speed = 0.5f;

    [SerializeField]
    [Range(0.01f, 10)]
    private float amount = 1;

    #endregion


    #region Fields

    private Vector3 _initialWorldPos;

    #endregion


    #region MonoBehaviour

    public void Start ()
    {
      _initialWorldPos = transform.position;
    }

    private void Update ()
    {
      float timer = Time.time * speed;
      float xNoise = Mathf.PerlinNoise(
        _initialWorldPos.x + timer, _initialWorldPos.y);
      float yNoise = Mathf.PerlinNoise(_initialWorldPos.x,
                                       _initialWorldPos.y + timer);

      Vector3 pos = _initialWorldPos;
      pos.x += Process(xNoise);
      pos.y += Process(yNoise);

      transform.position = pos;
    }

    #endregion


    #region Methods

    private float Process (float noise)
    {
      return amount * Mathf.Lerp(-1, 1, noise);
    }

    #endregion
  }
}