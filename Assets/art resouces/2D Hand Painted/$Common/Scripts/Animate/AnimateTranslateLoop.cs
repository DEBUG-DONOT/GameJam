using UnityEngine;

namespace NotSlot.HandPainted2D
{
  [AddComponentMenu("2D Hand Painted/Animate/Translate Loop")]
  public sealed class AnimateTranslateLoop : MonoBehaviour
  {
    #region Inspector

    [Range(-5, 5)]
    [SerializeField]
    private float speed = -0.2f;

    [SerializeField]
    private float leftWorldX = default;

    [SerializeField]
    private float rightWorldX = default;

    [SerializeField]
    private bool pingPong = false;

    [SerializeField]
    private bool wave = false;

    [Header("Sprite Renderer")]
    [SerializeField]
    private bool flipWithDirection = false;

    #endregion


    #region Fields

    private float _baseY;

    private SpriteRenderer _sprite;

    #endregion


    #region MonoBehavior

    private void Start ()
    {
      _baseY = transform.position.y;

      if ( flipWithDirection )
        TryGetComponent(out _sprite);
    }

    private void Update ()
    {
      Transform myTransform = transform;
      Vector3 pos = myTransform.position;
      pos.x += speed * Time.deltaTime;

      if ( wave )
        pos.y = _baseY + Mathf.Sin(_baseY + Time.time) * speed / 2;

      bool movingRight = speed > 0;
      bool changeDirection = (movingRight && pos.x >= rightWorldX) ||
                             (!movingRight && pos.x <= leftWorldX);
      if ( changeDirection )
      {
        if ( pingPong )
        {
          speed *= -1;
          if ( flipWithDirection && _sprite != null )
            _sprite.flipX = !_sprite.flipX;

          return;
        }

        pos.x = movingRight ? leftWorldX : rightWorldX;
      }

      myTransform.position = pos;
    }

    private void OnDrawGizmosSelected ()
    {
      Gizmos.color = Color.yellow;

      Vector3 from = new Vector3(leftWorldX, transform.position.y - 1);
      Vector3 to = new Vector3(leftWorldX, transform.position.y + 1);
      Gizmos.DrawLine(from, to);

      from.x = rightWorldX;
      to.x = rightWorldX;
      Gizmos.DrawLine(from, to);
    }

    #endregion
  }
}