using UnityEditor;

namespace Zhdk.Gamelab.InventorySystem {
    
    public class InventoryAssetsCreator
    {
        [MenuItem("Assets/Create/Inventory System/Inventory Object")]
        public static void CreateInventoryObjectAsset()
        {
            ScriptableObjectUtility.CreateAsset<InventoryObject>();
        }

        [MenuItem("Assets/Create/Inventory System/Inventory")]
        public static void CreateInventoryAsset()
        {
            ScriptableObjectUtility.CreateAsset<Inventory>();
        }
    }

}
