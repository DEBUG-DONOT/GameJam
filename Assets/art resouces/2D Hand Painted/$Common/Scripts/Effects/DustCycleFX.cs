using UnityEngine;

namespace NotSlot.HandPainted2D
{
  public sealed class DustCycleFX : MonoBehaviour
  {
    #region Constants

    private static readonly int OPACITY = Shader.PropertyToID("_Opacity");

    #endregion


    #region Fields

    private Renderer _renderer;

    private MaterialPropertyBlock _block;

    private float _opacity = 0;

    private float _timer = 0;

    #endregion


    #region MonoBehaviour

    private void Awake ()
    {
      _renderer = GetComponent<Renderer>();
      _block = new MaterialPropertyBlock();
      _renderer.GetPropertyBlock(_block);
    }

    private void Start ()
    {
      SetOpacity(0);
    }

    private void Update ()
    {
      _timer += Time.deltaTime;
      
      if ( _timer <= 1f )
      {
        // Fade in
        SetOpacity(_timer / 1f);
      }
      else if ( _timer >= 3.0f && _timer <= 4f )
      {
        // Fade out
        float amount = (_timer - 3.0f) / 1f;
        SetOpacity(1 - amount);
      }
      else if ( _timer >= 6 )
      {
        // Start Next cycle
        _timer = 0;
      }
    }

    #endregion


    #region Methods

    private void SetOpacity (float opacity)
    {
      _opacity = opacity;
      _block.SetFloat(OPACITY, _opacity);
      _renderer.SetPropertyBlock(_block);
    }

    #endregion
  }
}