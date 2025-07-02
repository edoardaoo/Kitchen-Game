#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace KitchenGame.Inventory
{
    [CustomEditor(typeof(SlotItem_SO), true)]
    public class SlotItem_SOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set Unique Random ID"))
            {
                SlotItem_SO item = (SlotItem_SO)target;

                HashSet<int> existingIDs = GetAllItemIDs();
                int newID;

                do
                {
                    newID = Random.Range(1, 100_000_000);
                } while (existingIDs.Contains(newID));

                item.SetIDEditorOnly(newID);

                EditorUtility.SetDirty(item);
                AssetDatabase.SaveAssets();
                Debug.Log($"Novo ID definido para {item.ItemName}: {newID}");
            }
        }

        private HashSet<int> GetAllItemIDs()
        {
            string[] guids = AssetDatabase.FindAssets("t:SlotItem_SO");
            HashSet<int> ids = new();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SlotItem_SO item = AssetDatabase.LoadAssetAtPath<SlotItem_SO>(path);
                if (item != null)
                    ids.Add(item.ItemID);
            }

            return ids;
        }

        // ---------- MENU: Set IDs for All ----------
        [MenuItem("Tools/Slot Items/Set IDs for All")]
        public static void SetIDsForAllItems()
        {
            string[] guids = AssetDatabase.FindAssets("t:SlotItem_SO");
            HashSet<int> existingIDs = new();
            int updatedCount = 0;

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SlotItem_SO item = AssetDatabase.LoadAssetAtPath<SlotItem_SO>(path);

                if (item == null)
                    continue;

                if (item.ItemID != 0)
                {
                    existingIDs.Add(item.ItemID);
                    continue;
                }

                int newID;
                do
                {
                    newID = Random.Range(1, 100_000_000);
                } while (existingIDs.Contains(newID));

                item.SetIDEditorOnly(newID);
                existingIDs.Add(newID);

                EditorUtility.SetDirty(item);
                updatedCount++;
            }

            AssetDatabase.SaveAssets();
            Debug.Log($"[SlotItem_SOEditor] IDs atualizados: {updatedCount} item(s).");
        }

        // ---------- MENU: Check Missing Names ----------
        [MenuItem("Tools/Slot Items/Check Missing Names")]
        public static void CheckMissingNames()
        {
            string[] guids = AssetDatabase.FindAssets("t:SlotItem_SO");
            List<string> missingNameAssets = new();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SlotItem_SO item = AssetDatabase.LoadAssetAtPath<SlotItem_SO>(path);

                if (item == null)
                    continue;

                if (string.IsNullOrWhiteSpace(item.ItemName))
                    missingNameAssets.Add(path);
            }

            if (missingNameAssets.Count == 0)
            {
                Debug.Log("[SlotItem_SOEditor] Nenhum SlotItem_SO com nome vazio.");
            }
            else
            {
                Debug.LogWarning($"[SlotItem_SOEditor] Encontrados {missingNameAssets.Count} SlotItem_SO(s) com nome vazio ou nulo:");
                foreach (var path in missingNameAssets)
                    Debug.LogWarning($"→ {path}");
            }
        }
    }
#endif
}