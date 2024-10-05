using System;
using UnityEngine;

namespace NotSlot.HandPainted2D
{
  [RequireComponent(typeof(SpriteRenderer))]
  [AddComponentMenu("2D Hand Painted/Wind Options (Sprite)")]
  public partial class WindOptions : MonoBehaviour
  {
    #region Constants

    private static readonly int WIND_SPEED = Shader.PropertyToID("_WindSpeed");

    private static readonly int WIND_SWAY = Shader.PropertyToID("_WindSway");

    private static readonly int WIND_NOISE = Shader.PropertyToID("_WindNoise");

    private static readonly int WIND_MAPPING =
      Shader.PropertyToID("_WindMapping");

    #endregion


    #region Inspector

    [SerializeField]
    [Range(0.1f, 10f)]
    private float speed = 1;

    [SerializeField]
    [Range(0, 1)]
    private float noise = 0;

    [SerializeField]
    [Range(0, 2)]
    private float sway = 0.2f;

    [SerializeField]
    private SwayOrigin swayOrigin = SwayOrigin.Bottom;

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
      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
      MaterialPropertyBlock block = new MaterialPropertyBlock();
      spriteRenderer.GetPropertyBlock(block);
      block.SetFloat(WIND_SPEED, speed);
      block.SetFloat(WIND_SWAY, sway);
      block.SetFloat(WIND_NOISE, noise);

      Vector4 origin;
      Bounds bounds = spriteRenderer.sprite.bounds;
      switch ( swayOrigin )
      {
        case SwayOrigin.Top:
          origin = new Vector4(bounds.max.y, bounds.min.y, -0.01f, 0.01f);
          break;
        case SwayOrigin.Bottom:
          origin = new Vector4(bounds.min.y, bounds.max.y, -0.01f, 0.01f);
          break;
        case SwayOrigin.Left:
          origin = new Vector4(-0.01f, 0.01f, bounds.min.x, bounds.max.x);
          break;
        case SwayOrigin.Right:
          origin = new Vector4(-0.01f, 0.01f, bounds.max.x, bounds.min.x);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      block.SetVector(WIND_MAPPING, origin);

      spriteRenderer.SetPropertyBlock(block);
    }

    #endregion
  }
}