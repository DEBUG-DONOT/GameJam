using UnityEngine;
using Random = UnityEngine.Random;

#if RENDERER_UNIVERSAL
#if UNITY_2021_2_OR_NEWER
using UnityEngine.Rendering.Universal;
#else
using UnityEngine.Experimental.Rendering.Universal;
#endif
#endif

namespace NotSlot.HandPainted2D
{
#if RENDERER_UNIVERSAL
  [RequireComponent(typeof(Light2D))]
#endif
  [AddComponentMenu("2D Hand Painted/Animate/Light Flicker")]
  public sealed class AnimateLightFlicker : MonoBehaviour
  {
    #region Inspector

    [SerializeField]
    [Range(0.1f, 10)]
    private float speed = 2;

    [SerializeField]
    [Range(0.01f, 1)]
    private float amount = 0.5f;

    #endregion


    #region Fields

    private float _base;

    private float _counter;

#if RENDERER_UNIVERSAL
    private Light2D _light;
#endif

    #endregion


    #region MonoBehaviour

    private void Awake ()
    {
#if RENDERER_UNIVERSAL
      _light = GetComponent<Light2D>();
#endif
    }

    public void Start ()
    {
#if RENDERER_UNIVERSAL
      _base = _light.intensity;
#endif
      _counter = Random.Range(0, 10);
    }

    private void Update ()
    {
      _counter += Time.deltaTime * speed;
#if RENDERER_UNIVERSAL
      _light.intensity = _base + Mathf.Sin(_counter) * amount;
#endif
    }

    #endregion
  }
}