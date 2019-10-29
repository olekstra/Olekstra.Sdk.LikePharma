﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Olekstra.LikePharma.Client.Validation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Olekstra.LikePharma.Client.Validation.ValidationMessages", typeof(ValidationMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} может содержать только цифры (19шт)..
        /// </summary>
        public static string CardNumberInvalid {
            get {
                return ResourceManager.GetString("CardNumberInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} должно быть от 0 до 9999..
        /// </summary>
        public static string ErrorCodeInvalid {
            get {
                return ResourceManager.GetString("ErrorCodeInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} должно содержать не менее {1} символов..
        /// </summary>
        public static string MinLengthFailed {
            get {
                return ResourceManager.GetString("MinLengthFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} должно начинаться с &apos;+7&apos; и следом 10 цифр..
        /// </summary>
        public static string PhoneNumberInvalid {
            get {
                return ResourceManager.GetString("PhoneNumberInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Недопустимое значение {0}: разрешены только цифры и латинские буквы..
        /// </summary>
        public static string PosIdInvalid {
            get {
                return ResourceManager.GetString("PosIdInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to При неуспешном (&quot;error&quot;) значении поля &quot;status&quot; поле &quot;ErrorCode&quot; должно иметь значение больше нуля..
        /// </summary>
        public static string StatusErrorMustHaveNonZeroErrorCode {
            get {
                return ResourceManager.GetString("StatusErrorMustHaveNonZeroErrorCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} не может содержать такое значение..
        /// </summary>
        public static string StatusInvalid {
            get {
                return ResourceManager.GetString("StatusInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to При успешном (&quot;success&quot;) значении поля &quot;status&quot; поле &quot;ErrorCode&quot; должно иметь значение 0..
        /// </summary>
        public static string StatusSuccessMustHaveErrorCodeZero {
            get {
                return ResourceManager.GetString("StatusSuccessMustHaveErrorCodeZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} должно быть указано..
        /// </summary>
        public static string ValueRequired {
            get {
                return ResourceManager.GetString("ValueRequired", resourceCulture);
            }
        }
    }
}
