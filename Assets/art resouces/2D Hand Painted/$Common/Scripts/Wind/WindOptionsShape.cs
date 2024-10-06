using UnityEngine;
using UnityEngine.U2D;

namespace NotSlot.HandPainted2D
{
  [RequireComponent(typeof(SpriteShapeRenderer))]
  [AddComponentMenu("2D Hand Painted/Wind Options (Sprite Shape)")]
  public sealed class WindOptionsShape : MonoBehaviour
  {
    #region Constants

    private static readonly int WIND_SPEED = Shader.PropertyToID("_WindSpeed");
    
    private static readonly int WIND_NOISE = Shader.PropertyToID("_WindNoise");

    private static readonly int WIND_FLIP = Shader.PropertyToID("_WindFlip");

    #endregion


    #region Inspector

    [SerializeField]
    [Range(0.1f, 10f)]
    private float speed = 1;

    [SerializeField]
    [Range(0, 1)]
    private float noise = 0.2f;

    [SerializeField]
    private bool flip = false;

    #endregion


    #region MonoBehaviour

    private void OnValidate ()
    {
      UpdateShader();
    }

    private void Start ()
    {
      UpdateShader();
    }

    #endregion


    #region Methods

    private void UpdateShader ()
    {
      SpriteShapeRenderer spriteRenderer = GetComponent<SpriteShapeRenderer>();
      MaterialPropertyBlock block = new MaterialPropertyBlock();
      spriteRenderer.GetPropertyBlock(block);
      block.SetFloat(WIND_SPEED, speed);
      block.SetFloat(WIND_NOISE, noise);
      block.SetFloat(WIND_FLIP, flip ? 1 : 0);
      spriteRenderer.SetPropertyBlock(block);
    }

    #endregion
  }
}