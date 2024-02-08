using ImGuiNET;
using UnityEngine;

namespace BloodMoon.Debug
{
    public abstract class ADearImGuiHandlerBase
    {
        public ADearImGuiHandlerBase()
        {
            
        }
        
        public void Enable()
        {
            ImGuiUn.Layout += DrawLayout;
        }
        public void Disable()
        {
            ImGuiUn.Layout -= DrawLayout;
        }
        protected abstract void DrawLayout();
    }

    public class DearImGuiHandlerDemo : ADearImGuiHandlerBase
    {
        protected override void DrawLayout()
        {
            ImGui.ShowDemoWindow();
        }
    }
}
