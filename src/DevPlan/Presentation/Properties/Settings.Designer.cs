﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevPlan.Presentation.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoLogin {
            get {
                return ((bool)(this["AutoLogin"]));
            }
            set {
                this["AutoLogin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string UserID {
            get {
                return ((string)(this["UserID"]));
            }
            set {
                this["UserID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Password {
            get {
                return ((string)(this["Password"]));
            }
            set {
                this["Password"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>2</string>
  <string>0,0,1,9</string>
  <string>10,5,1,9</string>
  <string>0,0,1,9</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection TestCarCalendarStyle {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["TestCarCalendarStyle"]));
            }
            set {
                this["TestCarCalendarStyle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>3</string>
  <string>0,0,1,9</string>
  <string>10,5,1,9</string>
  <string>0,0,1,9</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection OuterCarCalendarStyle {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["OuterCarCalendarStyle"]));
            }
            set {
                this["OuterCarCalendarStyle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>3</string>
  <string>0,0,1,9</string>
  <string>10,5,1,9</string>
  <string>0,0,1,9</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CarShareCalendarStyle {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CarShareCalendarStyle"]));
            }
            set {
                this["CarShareCalendarStyle"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://gj1tds.subaru.co.jp/devplan/support/")]
        public string HelpFile {
            get {
                return ((string)(this["HelpFile"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>3</string>
  <string>0,0,1,9</string>
  <string>10,5,1,9</string>
  <string>0,40,1,9</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection TrackCalendarStyle {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["TrackCalendarStyle"]));
            }
            set {
                this["TrackCalendarStyle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool MyPendingCar {
            get {
                return ((bool)(this["MyPendingCar"]));
            }
            set {
                this["MyPendingCar"] = value;
            }
        }
    }
}