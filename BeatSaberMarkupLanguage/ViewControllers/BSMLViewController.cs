﻿using HMUI;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatSaberMarkupLanguage.ViewControllers
{
    public abstract class BSMLViewController : ViewController, INotifyPropertyChanged
    {
        public abstract string Content { get; }

        public Action<bool, bool, bool> didActivate;

        public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
                BSMLParser.instance.Parse(Content, gameObject, this);

            didActivate?.Invoke(firstActivation, addedToHierarchy, screenSystemEnabling);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Logger.log?.Error($"Error invoking PropertyChanged for property '{propertyName}' on View Controller {name}\n{ex}");
            }
        }
    }
}
