using UnityEngine;
using Random = UnityEngine.Random;

namespace NotSlot.HandPainted2D
{
  [RequireComponent(typeof(Animator))]
  [AddComponentMenu("2D Hand Painted/Animate/Random Frame")]
  public sealed class AnimateRandomFrame : MonoBehaviour
  {
    #region MonoBehaviour

    private void Start ()
    {
      GetComponent<Animator>().Update(Random.value);
    }

    #endregion
  }
}