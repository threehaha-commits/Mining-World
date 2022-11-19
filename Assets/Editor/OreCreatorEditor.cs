using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OreCreator))]
public class OreCreatorEditor : Editor
{
        private SerializedProperty _oreIntensity;
        private SerializedProperty _oreDensity;
        private SerializedProperty _oreTypeFromNoise;
        
        private void OnEnable()
        {
                _oreDensity = serializedObject.FindProperty("_oreDensity");
                _oreTypeFromNoise = serializedObject.FindProperty("_oreTypeFromNoise");
                _oreIntensity = serializedObject.FindProperty("_oreIntensity");
        }

        public override void OnInspectorGUI()
        {
                serializedObject.Update();
                EditorGUILayout.LabelField("Интенсивность руды");
                EditorGUILayout.PropertyField(_oreIntensity);
                EditorGUILayout.LabelField("Чем меньше данный показатель, тем больше плотность руды");
                EditorGUILayout.PropertyField(_oreDensity);
                EditorGUILayout.LabelField("Значение шума при котором будет появлятся данная руда");
                EditorGUILayout.PropertyField(_oreTypeFromNoise);
                EditorGUILayout.LabelField("0. Stone\n1. Coal\n2. Lava\n3. BronIron\n4. Silver\n5. Gold\n6. Emerald\n7. Diamond", GUILayout.Height(140));
                serializedObject.ApplyModifiedProperties();;
        }
}