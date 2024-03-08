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
        public enum ColorSourceType
        {
            Manual,
            Rainbow,
            Other
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
        [SerializeField] ColorSourceType ColorSource = ColorSourceType.Rainbow;

        bool IsManual => ColorSource == ColorSourceType.Manual;
        bool IsRainbow => ColorSource == ColorSourceType.Rainbow;
        bool IsOther => ColorSource == ColorSourceType.Other;

        [HideLabel, ShowIf("IsManual"), OnValueChanged("Editor_RefreshMaterial"), SerializeField] Color BaseColorManual = Color.white;
        [HideLabel, ShowIf("IsRainbow"), OnValueChanged("Editor_RefreshMaterial"), SerializeField] RainbowColor BaseColorSelect = RainbowColor.Red;
        [HideLabel, ShowIf("IsOther"), OnValueChanged("Editor_RefreshMaterial"), SerializeField] InstancedMaterialHandler OtherMaterialHandler = null;

        Color GetBaseColor()
        {
            switch (ColorSource)
            {
                default:
                case ColorSourceType.Manual:
                    return BaseColorManual;
                case ColorSourceType.Rainbow:
                    return Rainbow2Color(BaseColorSelect);
                case ColorSourceType.Other:
                    return OtherMaterialHandler != null ? OtherMaterialHandler.GetBaseColor() : Color.white;
            }
        }

        Renderer mRenderer;
        Material mInstancedMaterial;
        static readonly int ID_BASE_COLOR = Shader.PropertyToID("_BaseColor");

        public void SetColor(RainbowColor rainbowColor)
        {
            ColorSource = ColorSourceType.Rainbow;
            BaseColorSelect = rainbowColor;
            RefreshMaterial();
        }
        public void SetColor(Color color)
        {
            ColorSource = ColorSourceType.Manual;
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
            mInstancedMaterial.SetColor(ID_BASE_COLOR, GetBaseColor());
        }

#if UNITY_EDITOR
        void Editor_RefreshMaterial()
        {
            if (EditorApplication.isPlaying) RefreshMaterial();
        }
#endif
    }
}

