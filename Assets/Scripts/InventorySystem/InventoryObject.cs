using UnityEngine;

namespace Zhdk.Gamelab.InventorySystem
{

    public class InventoryObject : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField]
        private string title;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite sprite;


        public string GetTitle ()
        {
            return title;
        }

        public Sprite GetSprite () 
        {
            return sprite;
        }

    }

}
