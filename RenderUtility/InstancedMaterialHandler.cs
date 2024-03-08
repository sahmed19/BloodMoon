using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BloodMoon.RenderUtility
{
    [RequireComponent(typeof(Renderer))]
    public class InstancedMaterialHandler : MonoBehaviour
    {
        public enum RainbowColor
        {
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Indigo,
            Violet
        }
        static readonly Color[] COLORS =
        {
            new Color(0.8f, 0.3f, 0.3f),
            new Color(0.9f, 0.6f, 0.3f),
            new Color(0.9f, 0.9f, 0.4f),
            new Color(0.3f, 0.8f, 0.3f),
            new Color(0.3f, 0.3f, 0.8f),
            new Color(0.3f, 0.3f, 0.7f),
            new Color(0.7f, 0.3f, 0.8f)
        };
        static Color Rainbow2Color(RainbowColor color) => COLORS[(int)color];

        [TitleGroup("Color")]
        [LabelText("Use Manual?"), ToggleLeft, SerializeField] bool UseManualBaseColor = false;
        [HideLabel, ShowIf("UseManualBaseColor"), OnValueChanged("Editor_RefreshMaterial"), SerializeField] Color BaseColorManual = Color.white;
        [HideLabel, HideIf("UseManualBaseColor"), OnValueChanged("Editor_RefreshMaterial"), SerializeField] RainbowColor BaseColorSelect = RainbowColor.Red;

        Color BaseColor => UseManualBaseColor ? BaseColorManual : Rainbow2Color(BaseColorSelect);

        Renderer mRenderer;
        Material mInstancedMaterial;
        static readonly int ID_BASE_COLOR = Shader.PropertyToID("_BaseColor");

        public void SetColor(RainbowColor rainbowColor)
        {
            UseManualBaseColor = false;
            BaseColorSelect = rainbowColor;
            RefreshMaterial();
        }
        public void SetColor(Color color)
        {
            UseManualBaseColor = true;
            BaseColorManual = color;
            RefreshMaterial();
        }

        void Awake()
        {
            InitializeInstancedMaterial();
            RefreshMaterial();
        }

        void InitializeInstancedMaterial()
        {
            if (mRenderer != null && mInstancedMaterial != null) return;
            mRenderer = GetComponent<Renderer>();
            mInstancedMaterial = new Material(mRenderer.sharedMaterial);
            mInstancedMaterial.name = mInstancedMaterial.name + " (" + gameObject.name + " INSTANCE)";
            mRenderer.sharedMaterial = mInstancedMaterial;
        }

        void RefreshMaterial()
        {
            mInstancedMaterial.SetColor(ID_BASE_COLOR, BaseColor);
        }

#if UNITY_EDITOR
        void Editor_RefreshMaterial()
        {
            if (EditorApplication.isPlaying) RefreshMaterial();
        }
#endif
    }
}

