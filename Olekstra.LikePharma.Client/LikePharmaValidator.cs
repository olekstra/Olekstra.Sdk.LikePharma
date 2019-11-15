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
        private readonly Policy policy;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="policy"><see cref="Policy"/>, которую необходимо использовать в процессе валидации.</param>
        public LikePharmaValidator(Policy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            this.policy = policy;
        }

        /// <summary>
        /// Выполяняет валидацию объекта, с учётом вложенных коллекций Order (для определённых типов), используя <see cref="Policy">политику валидации</see>, заданную в конструкторе.
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

            var psp = new PolicyServiceProvider(policy);

            var vc = new ValidationContext(instance, psp, null);

            results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(instance, vc, results, true);

            IEnumerable<object>? children = instance switch
            {
                GetDiscountRequest gdr => gdr.Orders,
                GetDiscountResponse gdr => gdr.Orders,
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

        private class PolicyServiceProvider : IServiceProvider
        {
            private readonly Policy policy;

            public PolicyServiceProvider(Policy policy)
            {
                this.policy = policy;
            }

            public object? GetService(Type serviceType)
            {
                if (serviceType == typeof(Policy))
                {
                    return policy;
                }

                return null;
            }
        }
    }
}
