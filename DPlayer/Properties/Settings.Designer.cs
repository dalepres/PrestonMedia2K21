﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DPlayer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int SnoozeTime {
            get {
                return ((int)(this["SnoozeTime"]));
            }
            set {
                this["SnoozeTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ButtonFace")]
        public string AlbumArtForeColor {
            get {
                return ((string)(this["AlbumArtForeColor"]));
            }
            set {
                this["AlbumArtForeColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12/11/2006 14:30:00")]
        public global::System.DateTime AlarmTime {
            get {
                return ((global::System.DateTime)(this["AlarmTime"]));
            }
            set {
                this["AlarmTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("E:\\My Music\\The Beatles\\Sgt. Pepper\'s Lonely Hearts Club Band\\11 Good Morning Goo" +
            "d Morning.mp3")]
        public string AlarmTrack {
            get {
                return ((string)(this["AlarmTrack"]));
            }
            set {
                this["AlarmTrack"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int AlarmVolumeStepUp {
            get {
                return ((int)(this["AlarmVolumeStepUp"]));
            }
            set {
                this["AlarmVolumeStepUp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("G:\\DebbiePics")]
        public string SlideShowPath {
            get {
                return ((string)(this["SlideShowPath"]));
            }
            set {
                this["SlideShowPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public string SlideShowIntervalSeconds {
            get {
                return ((string)(this["SlideShowIntervalSeconds"]));
            }
            set {
                this["SlideShowIntervalSeconds"] = value;
            }
        }
    }
}