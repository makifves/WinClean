﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Scover.WinClean.Properties {
    
    
    [CompilerGenerated()]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.2.0.0")]
    public sealed partial class Settings : ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [SpecialSetting(SpecialSetting.WebServiceUrl)]
        [DefaultSettingValue("https://github.com/5cover/WinClean/wiki")]
        public string WikiUrl {
            get {
                return ((string)(this["WikiUrl"]));
            }
        }
        
        [UserScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("0")]
        public int LogLevel {
            get {
                return ((int)(this["LogLevel"]));
            }
            set {
                this["LogLevel"] = value;
            }
        }
        
        [UserScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("05:00:00")]
        public TimeSpan ScriptTimeout {
            get {
                return ((TimeSpan)(this["ScriptTimeout"]));
            }
            set {
                this["ScriptTimeout"] = value;
            }
        }
        
        [UserScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("False")]
        public bool DetailsDuringExecution {
            get {
                return ((bool)(this["DetailsDuringExecution"]));
            }
            set {
                this["DetailsDuringExecution"] = value;
            }
        }
        
        [UserScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("False")]
        public bool DetailsAfterExecution {
            get {
                return ((bool)(this["DetailsAfterExecution"]));
            }
            set {
                this["DetailsAfterExecution"] = value;
            }
        }
        
        [UserScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("True")]
        public bool ForbidScriptCodeEdit {
            get {
                return ((bool)(this["ForbidScriptCodeEdit"]));
            }
            set {
                this["ForbidScriptCodeEdit"] = value;
            }
        }
    }
}
