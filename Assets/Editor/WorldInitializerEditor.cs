using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(WorldInitializator))]
public class WorldInitializerEditor : Editor
{
        private int x = 45;
        private int y = 45;
        private SerializedProperty _dirtSprites;
        private SerializedProperty _oreCreator;
        private SerializedProperty _blockFinder;
        private SerializedProperty _blockClicker;
        private SerializedProperty _backpack;
        private SerializedProperty _backpackSlotsFinder;
        private SerializedProperty _noise;
        
        private void OnEnable()
        {
                _dirtSprites = serializedObject.FindProperty("_dirtSprites");
                _oreCreator = serializedObject.FindProperty("_oreCreator");
                _blockFinder = serializedObject.FindProperty("_blockFinder");
                _blockClicker = serializedObject.FindProperty("_blockClicker");
                _backpack = serializedObject.FindProperty("_backpack");
                _backpackSlotsFinder = serializedObject.FindProperty("_backpackSlotsFinder");
                _noise = serializedObject.FindProperty("_noise");
        }

        public override void OnInspectorGUI()
        {
                WorldInitializator world = (WorldInitializator)target;
                EditorGUILayout.BeginHorizontal();
                {
                        x = EditorGUILayout.IntField("Width", x);
                        y = EditorGUILayout.IntField("Height", y);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(_dirtSprites);
                EditorGUILayout.PropertyField(_oreCreator);
                EditorGUILayout.PropertyField(_blockFinder);
                EditorGUILayout.PropertyField(_blockClicker);
                EditorGUILayout.PropertyField(_backpack);
                EditorGUILayout.PropertyField(_backpackSlotsFinder);
                EditorGUILayout.PropertyField(_noise);
                if (GUILayout.Button("Initialize") && Application.isPlaying)
                {
                        if (x == 0 || y == 0)
                                return;
                       // world.InitializeWorld(x, y);
                }
                serializedObject.ApplyModifiedProperties();
        }
}