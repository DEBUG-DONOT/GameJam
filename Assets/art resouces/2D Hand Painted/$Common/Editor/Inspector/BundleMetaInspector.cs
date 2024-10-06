using UnityEditor;

namespace NotSlot.HandPainted2D.Editor
{
  [CustomEditor(typeof(BundleMeta))]
  internal sealed class BundleMetaInspector : UnityEditor.Editor
  {
    #region Editor

    public override void OnInspectorGUI ()
    {
#if HANDPAINTED2D_SANDBOX
      base.OnInspectorGUI();
#else
      EditorGUILayout.HelpBox(
        "DO NOT DELETE this asset!\nThis asset is required by the 2D Hand Painted packs.\n\nYou may move it to a different location freely.",
        MessageType.Warning);
#endif
    }

    #endregion
  }
}