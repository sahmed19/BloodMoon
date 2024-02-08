using System;
using UnityEngine;

namespace BloodMoon.TechArtUtility
{
    [RequireComponent(typeof(Renderer))]
    public class InstancedMaterialHandler : MonoBehaviour
    {
        public Color BaseColor = Color.white;
        
        Renderer mRenderer;
        Material mInstancedMaterial;
        static readonly int ID_BASE_COLOR = Shader.PropertyToID("_BaseColor");

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
    }
}

