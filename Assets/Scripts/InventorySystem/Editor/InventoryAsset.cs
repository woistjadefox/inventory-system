using UnityEditor;

namespace Zhdk.Gamelab.InventorySystem {
    
    public class InventoryAsset
    {
        [MenuItem("Assets/Create/InventoryObject")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<InventoryObject>();
        }
    }

}
