using UnityEngine;

namespace NotSlot.HandPainted2D
{
  public class PackMeta : ScriptableObject
  {
    #region Inspector

    [SerializeField]
    private string packName;

    [SerializeField]
    private short installerVersion;

    [SerializeField]
    private string manual;

    #endregion


    #region Properties

    public string Name => packName;

    public short InstallerVersion => installerVersion;

    public string Manual => manual;

    #endregion
  }
}