﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Olekstra.LikePharma.Client {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Olekstra.LikePharma.Client.ValidationMessages", typeof(ValidationMessages).Assembly);
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
        ///   Looks up a localized string similar to Can validate only string values..
        /// </summary>
        public static string CanValidateOnlyStringValue {
            get {
                return ResourceManager.GetString("CanValidateOnlyStringValue", resourceCulture);
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
        ///   Looks up a localized string similar to Invalid value to validate: GetDiscountRequest or GetDiscountResponse expected..
        /// </summary>
        public static string CardOrPhoneNumberAttribute_InvalidValue {
            get {
                return ResourceManager.GetString("CardOrPhoneNumberAttribute_InvalidValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} не должно содержать пустые (null) элементы..
        /// </summary>
        public static string CollectionCanNotHaveNullElements {
            get {
                return ResourceManager.GetString("CollectionCanNotHaveNullElements", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} не может быть пустым, должно содержать элементы..
        /// </summary>
        public static string CollectionMustHaveElements {
            get {
                return ResourceManager.GetString("CollectionMustHaveElements", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} должно быть не менее 1 и не более 9999..
        /// </summary>
        public static string CountMustBePositive {
            get {
                return ResourceManager.GetString("CountMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер карты указан некорректно: ожидается 13 цифр (без разделителей)..
        /// </summary>
        public static string Digit13CardNumberValidator_Failure {
            get {
                return ResourceManager.GetString("Digit13CardNumberValidator_Failure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер карты указан некорректно: ожидается 13 или 19 цифр (без разделителей)..
        /// </summary>
        public static string Digit13Or19CardNumberValidator_Failure {
            get {
                return ResourceManager.GetString("Digit13Or19CardNumberValidator_Failure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер карты указан некорректно: ожидается 19 цифр (без разделителей)..
        /// </summary>
        public static string Digit19CardNumberValidator_Failure {
            get {
                return ResourceManager.GetString("Digit19CardNumberValidator_Failure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер карты указан некорректно: ожидается 19 цифр (без разделителей), первые цифры 610294..
        /// </summary>
        public static string Digit19Starting610294CardNumberValidator_Failure {
            get {
                return ResourceManager.GetString("Digit19Starting610294CardNumberValidator_Failure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер карты указан некорректно: ожидается 19 цифр (без разделителей), первые цифры 6103..
        /// </summary>
        public static string Digit19Starting6103CardNumberValidator_Failure {
            get {
                return ResourceManager.GetString("Digit19Starting6103CardNumberValidator_Failure", resourceCulture);
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
        ///   Looks up a localized string similar to Invalid value to validate: GetDiscountResponse.Order expected..
        /// </summary>
        public static string ErrorCodeMatchDiscountAttribute_InvalidValue {
            get {
                return ResourceManager.GetString("ErrorCodeMatchDiscountAttribute_InvalidValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для строк с error_code!=0 нельзя указывать ненулевой &apos;discount&apos;..
        /// </summary>
        public static string FailedOrderNotNeedDiscount {
            get {
                return ResourceManager.GetString("FailedOrderNotNeedDiscount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для строк с error_code!=0 нельзя заполнять поле &apos;transaction&apos;..
        /// </summary>
        public static string FailedOrderNotNeedTransaction {
            get {
                return ResourceManager.GetString("FailedOrderNotNeedTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер телефона указан некорректно: ожидается +7 и следом 10 цифр (без разделителей)..
        /// </summary>
        public static string FullRussianPhoneNumberValidator_Failure {
            get {
                return ResourceManager.GetString("FullRussianPhoneNumberValidator_Failure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неподдерживаемое значение правила CardAndPhoneUsage: {0}..
        /// </summary>
        public static string InvalidCardAndPhoneUsageValue {
            get {
                return ResourceManager.GetString("InvalidCardAndPhoneUsageValue", resourceCulture);
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
        ///   Looks up a localized string similar to Должны быть указаны и номер карты, и номер телефона..
        /// </summary>
        public static string NeedCardAndPhone {
            get {
                return ResourceManager.GetString("NeedCardAndPhone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Должен быть указан номер карты или номер телефона..
        /// </summary>
        public static string NeedCardOrPhone {
            get {
                return ResourceManager.GetString("NeedCardOrPhone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Должен быть указан либо номер карты, либо номер телефона (но не оба сразу)..
        /// </summary>
        public static string NeedEitherCardOrPhone {
            get {
                return ResourceManager.GetString("NeedEitherCardOrPhone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Недопустимое значение {0}: разрешены только цифры, латинские буквы и дефис..
        /// </summary>
        public static string PharmacyIdInvalid {
            get {
                return ResourceManager.GetString("PharmacyIdInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} должно быть указано..
        /// </summary>
        public static string PharmacyIdMustBeSet {
            get {
                return ResourceManager.GetString("PharmacyIdMustBeSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} заполнять не разрешено..
        /// </summary>
        public static string PharmacyIdMustNotBeSet {
            get {
                return ResourceManager.GetString("PharmacyIdMustNotBeSet", resourceCulture);
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
        ///   Looks up a localized string similar to Недопустимое значение {0}: разрешены только цифры, латинские буквы и дефис..
        /// </summary>
        public static string PosIdInvalid {
            get {
                return ResourceManager.GetString("PosIdInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Значение поля {0} не может быть менее нуля..
        /// </summary>
        public static string PriceMustBePositive {
            get {
                return ResourceManager.GetString("PriceMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0} is not a collection..
        /// </summary>
        public static string PropertyIsNotACollection {
            get {
                return ResourceManager.GetString("PropertyIsNotACollection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректный запрос. Выявленные при валидации ошибки содержатся в Data[&quot;ValidationErrors&quot;]..
        /// </summary>
        public static string RequestValidationFailed {
            get {
                return ResourceManager.GetString("RequestValidationFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректный ответ сервера. Выявленные при валидации ошибки содержатся в Data[&quot;ValidationErrors&quot;]..
        /// </summary>
        public static string ResponseValidationFailed {
            get {
                return ResourceManager.GetString("ResponseValidationFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Номер телефона указан некорректно: ожидается 10 цифр (без разделителей), без указания префикса &quot;+7&quot; или &quot;8&quot;..
        /// </summary>
        public static string ShortRussianPhoneNumberValidator_Failure {
            get {
                return ResourceManager.GetString("ShortRussianPhoneNumberValidator_Failure", resourceCulture);
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
        ///   Looks up a localized string similar to Для строк с error_code=0 необходимо указать ненулевой &apos;discount&apos;..
        /// </summary>
        public static string SuccessfulOrderNeedDiscount {
            get {
                return ResourceManager.GetString("SuccessfulOrderNeedDiscount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для строк с error_code=0 необходимо заполнить поле &apos;transaction&apos;..
        /// </summary>
        public static string SuccessfulOrderNeedTransaction {
            get {
                return ResourceManager.GetString("SuccessfulOrderNeedTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проверяемый объект должен быть наследником ResponseBase.
        /// </summary>
        public static string ValidationContextObjectInstanceMustBeResponseBase {
            get {
                return ResourceManager.GetString("ValidationContextObjectInstanceMustBeResponseBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не найдена политика валидации..
        /// </summary>
        public static string ValidationPolicyNotFound {
            get {
                return ResourceManager.GetString("ValidationPolicyNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} должно быть пустым (заполнение запрещено) в текущем режиме работы..
        /// </summary>
        public static string ValueProhibited {
            get {
                return ResourceManager.GetString("ValueProhibited", resourceCulture);
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
