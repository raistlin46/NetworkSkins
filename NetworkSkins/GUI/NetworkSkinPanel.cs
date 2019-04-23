﻿using System;
using ColossalFramework.UI;
using NetworkSkins.GUI.Abstraction;
using NetworkSkins.GUI.Catenaries;
using NetworkSkins.GUI.Colors;
using NetworkSkins.GUI.Lights;
using NetworkSkins.GUI.Pillars;
using NetworkSkins.GUI.Surfaces;
using NetworkSkins.GUI.Trees;
using NetworkSkins.Tool;
using UnityEngine;

namespace NetworkSkins.GUI
{
    public class NetworkSkinPanel : PanelBase
    {
        private ToolBar toolBar;
        private TreePanel treesPanel;
        private StreetLightPanel lightsPanel;
        private TerrainSurfacePanel terrainSurfacePanel;
        private PillarPanel pillarPanel;
        private CatenaryPanel catenaryPanel;
        private ColorPanel colorPanel;
        private SettingsPanel settingsPanel;
        private UIPanel space;
        private PanelBase currentPanel;
        public override void OnDestroy() {
            toolBar.EventDragEnd -= OnToolBarDragEnd;
            UnregisterEvents();
            base.OnDestroy();
        }

        public override void Start() {
            base.Start();
            Build(PanelType.None, new Layout(new Vector2(0.0f, 234.0f), true, LayoutDirection.Horizontal, LayoutStart.TopLeft, 0));
            color = GUIColor;
            relativePosition = Persistence.GetToolbarPosition();
            autoFitChildrenVertically = true;
            CreateToolBar();
            RegisterEvents();
        }

        public override void Update() {
            base.Update();
            float midScreen = Screen.width / 2.0f;
            space.zOrder = 1;
            if (autoLayoutStart == LayoutStart.TopLeft) {
                toolBar.zOrder = 0;
                if (currentPanel != null) currentPanel.zOrder = 2;
                if (relativePosition.x > midScreen) {
                    autoLayoutStart = LayoutStart.TopRight;
                }
            }
            if (autoLayoutStart == LayoutStart.TopRight) {
                toolBar.zOrder = 2;
                if (currentPanel != null) currentPanel.zOrder = 0;
                if (relativePosition.x + width < midScreen) {
                    autoLayoutStart = LayoutStart.TopLeft;
                }
            }
        }

        private void CreateToolBar() {
            toolBar = AddUIComponent<ToolBar>();
            toolBar.Build(PanelType.None, new Layout(new Vector2(40.0f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 0, "GenericPanel"));
            space = AddUIComponent<UIPanel>();
            space.size = new Vector2(5.0f, toolBar.height);
        }

        private void CreateTreesPanel() {
            treesPanel = AddUIComponent<TreePanel>();
            treesPanel.Build(PanelType.Trees, new Layout(new Vector2(400.0f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 5, "GenericPanel"));
            currentPanel = treesPanel;
        }

        private void CreateLightsPanel() {
            lightsPanel = AddUIComponent<StreetLightPanel>();
            lightsPanel.Build(PanelType.Lights, new Layout(new Vector2(400.0f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 5, "GenericPanel"));
            currentPanel = lightsPanel;
        }

        private void CreateSurfacePanel() {
            terrainSurfacePanel = AddUIComponent<TerrainSurfacePanel>();
            terrainSurfacePanel.Build(PanelType.Surfaces, new Layout(new Vector2(388.0f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 5, "GenericPanel"));
            currentPanel = terrainSurfacePanel;
        }

        private void CreateCatenaryPanel() {
            catenaryPanel = AddUIComponent<CatenaryPanel>();
            catenaryPanel.Build(PanelType.Catenary, new Layout(new Vector2(400.0f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 5, "GenericPanel"));
            currentPanel = catenaryPanel;
        }

        private void CreatePillarsPanel() {
            pillarPanel = AddUIComponent<PillarPanel>();
            pillarPanel.Build(PanelType.Pillars, new Layout(new Vector2(400.0f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 5, "GenericPanel"));
            currentPanel = pillarPanel;
        }

        private void CreateColorsPanel() {
            colorPanel = AddUIComponent<ColorPanel>();
            colorPanel.Build(PanelType.Color, new Layout(new Vector2(255f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 0, "GenericPanel"));
            colorPanel.padding = new RectOffset(1, 0, 0, 0);
            colorPanel.autoFitChildrenHorizontally = true;
            currentPanel = colorPanel;
        }

        private void CreateSettingsPanel() {
            settingsPanel = AddUIComponent<SettingsPanel>();
            settingsPanel.Build(PanelType.Settings, new Layout(new Vector2(228.6f, 0.0f), true, LayoutDirection.Vertical, LayoutStart.TopLeft, 0, "GenericPanel"));
            settingsPanel.autoFitChildrenHorizontally = true;
            currentPanel = settingsPanel;
        }

        private void RegisterEvents() {
            toolBar.EventDragEnd += OnToolBarDragEnd;
            RegisterClickEvents();
            RegisterVisibilityEvents();
        }

        private void OnToolBarDragEnd() {
            Persistence.SetToolbarPosition(relativePosition);
        }

        private void UnregisterEvents() {
            UnregisterClickEvents();
            UnregisterVisibilityEvents();
        }

        private void RegisterClickEvents() {
            toolBar.ButtonBar.EventTreesClicked += OnTreesClicked;
            toolBar.ButtonBar.EventLightsClicked += OnLightsClicked;
            toolBar.ButtonBar.EventSurfacesClicked += OnSurfacesClicked;
            toolBar.ButtonBar.EventPillarsClicked += OnPillarsClicked;
            toolBar.ButtonBar.EventColorClicked += OnColorClicked;
            toolBar.ButtonBar.EventCatenaryClicked += OnCatenaryClicked;
            toolBar.ButtonBar.EventExtrasClicked += OnExtrasClicked;
            toolBar.ButtonBar.EventPipetteClicked += OnPipetteClicked;
        }

        private void RegisterVisibilityEvents() {
            toolBar.ButtonBar.EventTreesVisibilityChanged += OnTreesVisibilityChanged;
            toolBar.ButtonBar.EventLightsVisibilityChanged += OnLightsVisibilityChanged;
            toolBar.ButtonBar.EventSurfacesVisibilityChanged += OnSurfacesVisibilityChanged;
            toolBar.ButtonBar.EventPillarsVisibilityChanged += OnPillarsVisibilityChanged;
            toolBar.ButtonBar.EventColorVisibilityChanged += OnColorVisibilityChanged;
            toolBar.ButtonBar.EventCatenaryVisibilityChanged += OnCatenaryVisibilityChanged;
            toolBar.ButtonBar.EventSettingsVisibilityChanged += OnSettingsVisibilityChanged;
        }

        private void UnregisterClickEvents() {
            toolBar.ButtonBar.EventTreesClicked -= OnTreesClicked;
            toolBar.ButtonBar.EventLightsClicked -= OnLightsClicked;
            toolBar.ButtonBar.EventSurfacesClicked -= OnSurfacesClicked;
            toolBar.ButtonBar.EventPillarsClicked -= OnPillarsClicked;
            toolBar.ButtonBar.EventColorClicked -= OnColorClicked;
            toolBar.ButtonBar.EventCatenaryClicked -= OnCatenaryClicked;
            toolBar.ButtonBar.EventExtrasClicked -= OnExtrasClicked;
        }

        private void UnregisterVisibilityEvents() {
            toolBar.ButtonBar.EventTreesVisibilityChanged -= OnTreesVisibilityChanged;
            toolBar.ButtonBar.EventLightsVisibilityChanged -= OnLightsVisibilityChanged;
            toolBar.ButtonBar.EventSurfacesVisibilityChanged -= OnSurfacesVisibilityChanged;
            toolBar.ButtonBar.EventPillarsVisibilityChanged -= OnPillarsVisibilityChanged;
            toolBar.ButtonBar.EventColorVisibilityChanged -= OnColorVisibilityChanged;
            toolBar.ButtonBar.EventCatenaryVisibilityChanged -= OnCatenaryVisibilityChanged;
            toolBar.ButtonBar.EventSettingsVisibilityChanged -= OnSettingsVisibilityChanged;
        }

        private void OnSettingsVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && settingsPanel != null) {
                SetButtonUnfocused(button);
                Destroy(settingsPanel.gameObject);
            }
        }

        private void OnCatenaryVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && catenaryPanel != null) {
                SetButtonUnfocused(button);
                Destroy(catenaryPanel.gameObject);
            }
        }

        private void OnColorVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && colorPanel != null) {
                SetButtonUnfocused(button);
                Destroy(colorPanel.gameObject);
            }
        }

        private void OnPillarsVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && pillarPanel != null) {
                SetButtonUnfocused(button);
                Destroy(pillarPanel.gameObject);
            }
        }

        private void OnSurfacesVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && terrainSurfacePanel != null) {
                SetButtonUnfocused(button);
                Destroy(terrainSurfacePanel.gameObject);
            }
        }

        private void OnLightsVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && lightsPanel != null) {
                SetButtonUnfocused(button);
                Destroy(lightsPanel.gameObject);
            }
        }

        private void OnTreesVisibilityChanged(UIButton button, UIButton[] buttons, bool visible) {
            if (!visible && treesPanel != null) {
                SetButtonUnfocused(button);
                Destroy(treesPanel.gameObject);
            }
        }

        private void OnExtrasClicked(UIButton button, UIButton[] buttons) {
            if (settingsPanel != null) {
                SetButtonUnfocused(button);
                Destroy(settingsPanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreateSettingsPanel();
            }
        }

        private void OnCatenaryClicked(UIButton button, UIButton[] buttons) {
            if (catenaryPanel != null) {
                SetButtonUnfocused(button);
                Destroy(catenaryPanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreateCatenaryPanel();
            }
        }

        private void OnColorClicked(UIButton button, UIButton[] buttons) {
            if (colorPanel != null) {
                SetButtonUnfocused(button);
                Destroy(colorPanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreateColorsPanel();
            }
        }

        private void OnSurfacesClicked(UIButton button, UIButton[] buttons) {
            if (terrainSurfacePanel != null) {
                SetButtonUnfocused(button);
                Destroy(terrainSurfacePanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreateSurfacePanel();
            }
        }

        private void OnPillarsClicked(UIButton button, UIButton[] buttons) {
            if (pillarPanel != null) {
                SetButtonUnfocused(button);
                Destroy(pillarPanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreatePillarsPanel();
            }
        }

        private void OnLightsClicked(UIButton button, UIButton[] buttons) {
            if (lightsPanel != null) {
                SetButtonUnfocused(button);
                Destroy(lightsPanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreateLightsPanel();
            }
        }

        private void OnTreesClicked(UIButton button, UIButton[] buttons) {
            if (treesPanel != null) {
                SetButtonUnfocused(button);
                Destroy(treesPanel.gameObject);
            } else {
                RefreshButtons(button, buttons);
                CloseAll();
                CreateTreesPanel();
            }
        }

        private void OnPipetteClicked(UIButton focusedButton, UIButton[] buttons) {
            ToolsModifierControl.SetTool<PipetteTool>();
        }

        private void CloseAll() {
            if (treesPanel != null) {
                Destroy(treesPanel.gameObject);
            }
            if (lightsPanel != null) {
                Destroy(lightsPanel.gameObject);
            }
            if (terrainSurfacePanel != null) {
                Destroy(terrainSurfacePanel.gameObject);
            }
            if (pillarPanel != null) {
                Destroy(pillarPanel.gameObject);
            }
            if (catenaryPanel != null) {
                Destroy(catenaryPanel.gameObject);
            }
            if (colorPanel != null) {
                Destroy(colorPanel.gameObject);
            }
            if (settingsPanel != null) {
                Destroy(settingsPanel.gameObject);
            }
        }

        private void RefreshButtons(UIButton focusedButton, UIButton[] buttons) {
            for (int i = 0; i < buttons.Length; i++) {
                SetButtonUnfocused(buttons[i]);
            }
            SetButtonFocused(focusedButton);
        }

        private void SetButtonFocused(UIButton button) {
            if (button != null) {
                button.normalBgSprite = button.focusedBgSprite = button.hoveredBgSprite = string.Concat(button.normalBgSprite.Replace("Focused", ""), "Focused");
            }
        }

        private void SetButtonUnfocused(UIButton button) {
            if (button != null) {
                button.normalBgSprite = button.focusedBgSprite = button.normalBgSprite.Replace("Focused", "");
                button.hoveredBgSprite = button.hoveredBgSprite.Replace("Focused", "Hovered");
            }
        }
    }
}