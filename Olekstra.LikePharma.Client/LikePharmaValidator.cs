namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Валидатор для запросов и ответов.
    /// </summary>
    public class LikePharmaValidator
    {
        private readonly ProtocolSettings protocolSettings;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="protocolSettings"><see cref="ProtocolSettings"/>, которую необходимо использовать в процессе валидации.</param>
        public LikePharmaValidator(ProtocolSettings protocolSettings)
        {
            this.protocolSettings = protocolSettings ?? throw new ArgumentNullException(nameof(protocolSettings));
        }

        /// <summary>
        /// Выполняет валидацию объекта с учётом вложенных коллекций (Orders, Products, ...), используя <see cref="ProtocolSettings">параметры протокола</see>, заданные в конструкторе.
        /// </summary>
        /// <param name="instance">Экземпляр объекта для проверки.</param>
        /// <param name="results">Список, который необходимо заполнить ошибками (если они будут найдены).</param>
        /// <returns>Результат валидации: <b>true</b> в случае успешной валидации или <b>false</b> если были обнаружены ошибки.</returns>
        public bool TryValidateObject(object instance, out List<ValidationResult> results)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var psp = new ProtocolSettingsServiceProvider(protocolSettings);

            var vc = new ValidationContext(instance, psp, null);

            results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(instance, vc, results, true);

            IEnumerable<object>? children = instance switch
            {
                GetDiscountRequest gdr => gdr.Orders,
                GetDiscountResponse gdr => gdr.Orders,
                ConfirmPurchaseRequest cpr => cpr.Skus,
                GetProgramsResponse gpr => gpr.Programs,
                GetProductsResponse gpr => gpr.Products,
                UpdatePharmaciesRequest upr => upr.Pharmacies,
                _ => null,
            };

            if (children != null)
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        var childvc = new ValidationContext(child, psp, null);
                        isValid &= Validator.TryValidateObject(child, childvc, results, true);
                    }
                }
            }

            return isValid;
        }

        private class ProtocolSettingsServiceProvider : IServiceProvider
        {
            private readonly ProtocolSettings protocolSettings;

            public ProtocolSettingsServiceProvider(ProtocolSettings protocolSettings)
            {
                this.protocolSettings = protocolSettings;
            }

            public object? GetService(Type serviceType)
            {
                if (serviceType == typeof(ProtocolSettings))
                {
                    return protocolSettings;
                }

                return null;
            }
        }
    }
}
