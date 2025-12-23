using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Tymski {
    [Serializable]
    public class SceneReference : ISerializationCallbackReceiver {
#if UNITY_EDITOR
        [SerializeField] private Object sceneAsset;

        private bool IsValidSceneAsset {
            get {
                if (!sceneAsset) return false;
                return sceneAsset is SceneAsset;
            }
        }
#endif

        [SerializeField] private string scenePath = string.Empty;

        public string ScenePath {
            get {
#if UNITY_EDITOR
                return GetScenePathFromAsset();
#else
                return scenePath;
#endif
            }
            set {
                scenePath = value;
#if UNITY_EDITOR
                sceneAsset = GetSceneAssetFromPath();
#endif
            }
        }

        public string SceneName => System.IO.Path.GetFileNameWithoutExtension(scenePath);

        public static implicit operator string(SceneReference sceneReference) {
            return sceneReference.ScenePath;
        }

        public void LoadScene() {
            if (string.IsNullOrEmpty(scenePath)) {
                Debug.LogError("Scene path is empty!");
                return;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }

        public AsyncOperation LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode mode = UnityEngine.SceneManagement.LoadSceneMode.Single) {
            if (string.IsNullOrEmpty(scenePath)) {
                Debug.LogError("Scene path is empty!");
                return null;
            }
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneName, mode);
        }

        public void OnBeforeSerialize() {
#if UNITY_EDITOR
            HandleBeforeSerialize();
#endif
        }

        public void OnAfterDeserialize() {
#if UNITY_EDITOR
            EditorApplication.update += HandleAfterDeserialize;
#endif
        }

#if UNITY_EDITOR
        private SceneAsset GetSceneAssetFromPath() {
            return string.IsNullOrEmpty(scenePath) ? null : AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        }

        private string GetScenePathFromAsset() {
            return sceneAsset == null ? string.Empty : AssetDatabase.GetAssetPath(sceneAsset);
        }

        private void HandleBeforeSerialize() {
            if (IsValidSceneAsset == false && string.IsNullOrEmpty(scenePath) == false) {
                sceneAsset = GetSceneAssetFromPath();
                if (sceneAsset == null) scenePath = string.Empty;
                EditorSceneManager.MarkAllScenesDirty();
            } else {
                scenePath = GetScenePathFromAsset();
            }
        }

        private void HandleAfterDeserialize() {
            EditorApplication.update -= HandleAfterDeserialize;
            if (IsValidSceneAsset) return;
            if (string.IsNullOrEmpty(scenePath)) return;

            sceneAsset = GetSceneAssetFromPath();
            if (!sceneAsset) scenePath = string.Empty;
            if (!Application.isPlaying) EditorSceneManager.MarkAllScenesDirty();
        }
#endif
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer {
        private const string sceneAssetPropertyString = "sceneAsset";
        private const string scenePathPropertyString = "scenePath";

        private static readonly float lineHeight = EditorGUIUtility.singleLineHeight;
        private const float PAD_SIZE = 2f;
        private const float BUTTON_WIDTH = 60f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.serializedObject.isEditingMultipleObjects) {
                GUI.Label(position, "Scene multiediting not supported");
                return;
            }

            var sceneAssetProperty = GetSceneAssetProperty(property);
            var sceneControlID = GUIUtility.GetControlID(FocusType.Passive);

            // Основне поле для вибору сцени
            var fieldRect = position;
            fieldRect.height = lineHeight;

            EditorGUI.BeginChangeCheck();
            sceneAssetProperty.objectReferenceValue = EditorGUI.ObjectField(
                fieldRect, label, sceneAssetProperty.objectReferenceValue, typeof(SceneAsset), false);

            if (EditorGUI.EndChangeCheck()) {
                var buildScene = BuildUtils.GetBuildScene(sceneAssetProperty.objectReferenceValue);
                if (buildScene.scene == null)
                    GetScenePathProperty(property).stringValue = string.Empty;
            }

            // Показуємо статус та кнопки Build Settings
            if (sceneAssetProperty.objectReferenceValue != null) {
                var buildScene = BuildUtils.GetBuildScene(sceneAssetProperty.objectReferenceValue);
                DrawBuildSettingsInfo(position, buildScene);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var sceneAssetProperty = GetSceneAssetProperty(property);
            return sceneAssetProperty.objectReferenceValue != null ? lineHeight * 2 + PAD_SIZE : lineHeight;
        }

        private void DrawBuildSettingsInfo(Rect position, BuildUtils.BuildScene buildScene) {
            var infoRect = position;
            infoRect.y += lineHeight + PAD_SIZE;
            infoRect.height = lineHeight;
            infoRect.x += EditorGUIUtility.labelWidth;
            infoRect.width -= EditorGUIUtility.labelWidth;

            var statusRect = infoRect;
            statusRect.width -= BUTTON_WIDTH * 2 + PAD_SIZE * 2;

            var buttonRect1 = infoRect;
            buttonRect1.x = statusRect.xMax + PAD_SIZE;
            buttonRect1.width = BUTTON_WIDTH;

            var buttonRect2 = buttonRect1;
            buttonRect2.x = buttonRect1.xMax + PAD_SIZE;

            // Статус сцени
            var statusContent = new GUIContent();
            var statusStyle = EditorStyles.helpBox;

            if (buildScene.buildIndex == -1) {
                statusContent.text = "⚠ Not in Build Settings";
                statusContent.tooltip = "Scene not added in Build Settings і не буде включена в білд";

                if (GUI.Button(buttonRect1, new GUIContent("Add", "Add scene to Build Settings"), EditorStyles.miniButton))
                    BuildUtils.AddBuildScene(buildScene, true, true);

                if (GUI.Button(buttonRect2, "Settings", EditorStyles.miniButton))
                    BuildUtils.OpenBuildSettings();
            } else if (buildScene.scene.enabled) {
                statusContent.text = $"✓ Build Index: {buildScene.buildIndex}";
                statusContent.tooltip = "Сцена в Build Settings і ENABLED";

                if (GUI.Button(buttonRect1, "Disable", EditorStyles.miniButton))
                    BuildUtils.SetBuildSceneState(buildScene, false);

                if (GUI.Button(buttonRect2, "Delete", EditorStyles.miniButton))
                    BuildUtils.RemoveBuildScene(buildScene);
            } else {
                statusContent.text = $"○ Build Index: {buildScene.buildIndex} (вимкнено)";
                statusContent.tooltip = "Scene in Build Settings but Disabled";

                if (GUI.Button(buttonRect1, "Enable", EditorStyles.miniButton))
                    BuildUtils.SetBuildSceneState(buildScene, true);

                if (GUI.Button(buttonRect2, "Disable", EditorStyles.miniButton))
                    BuildUtils.RemoveBuildScene(buildScene);
            }

            GUI.Label(statusRect, statusContent, statusStyle);
        }

        private static SerializedProperty GetSceneAssetProperty(SerializedProperty property) {
            return property.FindPropertyRelative(sceneAssetPropertyString);
        }

        private static SerializedProperty GetScenePathProperty(SerializedProperty property) {
            return property.FindPropertyRelative(scenePathPropertyString);
        }

        private static class BuildUtils {
            public struct BuildScene {
                public int buildIndex;
                public GUID assetGUID;
                public string assetPath;
                public EditorBuildSettingsScene scene;
            }

            public static BuildScene GetBuildScene(Object sceneObject) {
                var entry = new BuildScene {
                    buildIndex = -1,
                    assetGUID = new GUID(string.Empty)
                };

                if (sceneObject as SceneAsset == null) return entry;

                entry.assetPath = AssetDatabase.GetAssetPath(sceneObject);
                entry.assetGUID = new GUID(AssetDatabase.AssetPathToGUID(entry.assetPath));

                var scenes = EditorBuildSettings.scenes;
                for (var index = 0; index < scenes.Length; ++index) {
                    if (!entry.assetGUID.Equals(scenes[index].guid)) continue;
                    entry.scene = scenes[index];
                    entry.buildIndex = index;
                    return entry;
                }

                return entry;
            }

            public static void SetBuildSceneState(BuildScene buildScene, bool enabled) {
                var scenesToModify = EditorBuildSettings.scenes;
                foreach (var curScene in scenesToModify.Where(curScene => curScene.guid.Equals(buildScene.assetGUID))) {
                    curScene.enabled = enabled;
                    break;
                }
                EditorBuildSettings.scenes = scenesToModify;
            }

            public static void AddBuildScene(BuildScene buildScene, bool force = false, bool enabled = true) {
                var newScene = new EditorBuildSettingsScene(buildScene.assetGUID, enabled);
                var tempScenes = EditorBuildSettings.scenes.ToList();
                tempScenes.Add(newScene);
                EditorBuildSettings.scenes = tempScenes.ToArray();
            }

            public static void RemoveBuildScene(BuildScene buildScene) {
                var tempScenes = EditorBuildSettings.scenes.ToList();
                tempScenes.RemoveAll(scene => scene.guid.Equals(buildScene.assetGUID));
                EditorBuildSettings.scenes = tempScenes.ToArray();
            }

            public static void OpenBuildSettings() {
                EditorWindow.GetWindow(typeof(BuildPlayerWindow));
            }
        }
    }
#endif
}
