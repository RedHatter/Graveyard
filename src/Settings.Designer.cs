namespace HDT.Plugins.Graveyard {


    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {

        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

        public static Settings Default {
            get {
                return defaultInstance;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool NormalEnabled {
            get {
                return ((bool)(this["NormalEnabled"]));
            }
            set {
                this["NormalEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnemyEnabled {
            get {
                return ((bool)(this["EnemyEnabled"]));
            }
            set {
                this["EnemyEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ResurrectEnabled {
            get {
                return ((bool)(this["ResurrectEnabled"]));
            }
            set {
                this["ResurrectEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ResurrectKazakus {
            get {
                return ((bool)(this["ResurrectKazakus"]));
            }
            set {
                this["ResurrectKazakus"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AnyfinEnabled {
            get {
                return ((bool)(this["AnyfinEnabled"]));
            }
            set {
                this["AnyfinEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool NZothEnabled {
            get {
                return ((bool)(this["NZothEnabled"]));
            }
            set {
                this["NZothEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DiscardEnabled {
            get {
                return ((bool)(this["DiscardEnabled"]));
            }
            set {
                this["DiscardEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool HadronoxEnabled {
            get {
                return ((bool)(this["HadronoxEnabled"]));
            }
            set {
                this["HadronoxEnabled"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DKGuldanEnabled {
            get {
                return ((bool)(this["DKGuldanEnabled"]));
            }
            set {
                this["DKGuldanEnabled"] = value;
            }
        }
    }
}
