﻿using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Components
{
    public class Backgroundable : MonoBehaviour
    {
        private static Dictionary<string, string> Backgrounds => new Dictionary<string, string>()
        {
            { "round-rect-panel", "RoundRect10" },
            { "panel-top", "RoundRect10" },
            { "panel-fade-gradient", "RoundRect10Thin" },
            { "panel-top-gradient", "RoundRect10" },
        };

        private static Dictionary<string, string> ObjectNames => new Dictionary<string, string>()
        {
            { "round-rect-panel", "KeyboardWrapper" },
            { "panel-top", "BG" },
            { "panel-fade-gradient", "Background" },
            { "panel-top-gradient", "BG" },
        };
        private static Dictionary<string, string> ObjectParentNames => new Dictionary<string, string>()
        {
            { "round-rect-panel", "Wrapper" },
            { "panel-top", "PracticeButton" },
            { "panel-fade-gradient", "LevelListTableCell" },
            { "panel-top-gradient", "ActionButton" },
        };

        public Image background;

        private static readonly Dictionary<string, ImageView> _backgroundCache = new Dictionary<string, ImageView>();

        public void ApplyBackground(string name)
        {
            if (background != null)
                throw new Exception("Cannot add multiple backgrounds");

            if (!Backgrounds.TryGetValue(name, out string backgroundName))
                throw new Exception($"Background type '{name}' not found");

            try
            {
                if (!_backgroundCache.TryGetValue(name, out ImageView bgTemplate) || bgTemplate == null)
                {
                    if (!(bgTemplate is null) && bgTemplate == null)
                        _backgroundCache.Remove(name);

                    bgTemplate = Resources.FindObjectsOfTypeAll<ImageView>().First(x => x.gameObject?.name == ObjectNames[name] && x.sprite?.name == backgroundName && x.transform.parent?.name == ObjectParentNames[name]);
                    _backgroundCache.Add(name, bgTemplate);
                }
                background = gameObject.AddComponent(bgTemplate);
                background.enabled = true;
            }
            catch
            {
                Logger.log.Error($"Error loading background: '{name}'");
            }
        }
    }
}
