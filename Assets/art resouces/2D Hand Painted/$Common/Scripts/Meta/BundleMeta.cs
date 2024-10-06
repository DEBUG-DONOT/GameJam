#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NotSlot.HandPainted2D
{
  public class BundleMeta : ScriptableObject, ISerializationCallbackReceiver
  {
    #region Constants

    private const string ASSET_PATH = "Assets/Editor/2DHandPainted.asset";

    #endregion


    #region Inspector

    [SerializeField]
    private string[] names = new string[0];

    [SerializeField]
    private short[] versions = new short[0];

    [SerializeField]
    private short tagsLayersVersion = 0;

    #endregion


    #region Fields

#if UNITY_EDITOR
    private static BundleMeta _instance = null;
#endif

    private Dictionary<string, short> _packs = new Dictionary<string, short>();

    #endregion


    #region Properties

    public short TagsLayersVersion
    {
      get => tagsLayersVersion;
      set => tagsLayersVersion = value;
    }

    #endregion


    #region ISerializationCallbackReceiver

    public void OnBeforeSerialize ()
    {
      names = _packs?.Keys.ToArray();
      versions = _packs?.Values.ToArray();
    }

    public void OnAfterDeserialize ()
    {
      _packs = Enumerable.Range(0, names.Length)
                         .ToDictionary(i => names[i], i => versions[i]);
    }

    #endregion


    #region Methods

    public bool HasAnotherPack (short installerVersion)
    {
      foreach ( short version in versions )
        if ( version >= installerVersion )
          return true;

      return false;
    }

    public short GetVersion (string pack)
    {
      return !_packs.ContainsKey(pack) ? (short) 0 : _packs[pack];
    }

    public void SetVersion (string pack, short version)
    {
      _packs[pack] = version;
    }

    public static BundleMeta GetAsset ()
    {
#if UNITY_EDITOR
      if ( _instance != null )
        return _instance;

      string[] guids = AssetDatabase.FindAssets("t:BundleMeta");
      if ( guids.Length > 0 )
      {
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        _instance = AssetDatabase.LoadAssetAtPath<BundleMeta>(path);
        return _instance;
      }

      // Create meta
      if ( !AssetDatabase.IsValidFolder("Assets/Editor") )
        AssetDatabase.CreateFolder("Assets", "Editor");

      _instance = CreateInstance<BundleMeta>();
      AssetDatabase.CreateAsset(_instance, ASSET_PATH);
      return _instance;
#else
      return null;
#endif
    }

    #endregion
  }
}