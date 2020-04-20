namespace Olekstra.LikePharma.Client.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Вспомогательная структура ответа на запрос <see cref="GetProgramsRequest"/> с двумя написания вариантами: program и programs.
    /// </summary>
    [XmlRoot("get_programs_response")]
    public class GetProgramsResponseHelper : ResponseBase
    {
        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public GetProgramsResponseHelper()
        {
            // Nothing
        }

        /// <summary>
        /// Конструктор, копирующий значения из переданного объекта.
        /// </summary>
        /// <param name="source">Объект, значения которого необходимо скопировать.</param>
        public GetProgramsResponseHelper(GetProgramsResponse source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            CopyFrom(source);
            ProgramsSingular = source.Programs;
            ProgramsPlural = source.Programs;
        }

        /// <summary>
        /// Список активных программ (единственое число).
        /// </summary>
        [JsonPropertyName("program")]
        [XmlIgnore]
        public List<GetProgramsResponse.Program>? ProgramsSingular { get; set; }

        /// <summary>
        /// Список активных программ (множественое число).
        /// </summary>
        [JsonPropertyName("programs")]
        [XmlIgnore]
        public List<GetProgramsResponse.Program>? ProgramsPlural { get; set; }

#pragma warning disable CA1819 // Очень нужно для XML-сериализации
#pragma warning disable SA1011 // Эй, это ж nullable-массив :)
        /// <summary>
        /// Список активных программ (единственое число), для сериализации.
        /// </summary>
        [JsonIgnore]
        [XmlArray("program")]
        [XmlArrayItem("program")]
        public GetProgramsResponse.Program[]? ProgramsSingularArray
        {
            get { return ProgramsSingular?.ToArray(); }
            set { ProgramsSingular = value?.ToList(); }
        }

        /// <summary>
        /// Список активных программ (множественое число), для сериализации.
        /// </summary>
        [JsonIgnore]
        [XmlArray("programs")]
        [XmlArrayItem("program")]
        public GetProgramsResponse.Program[]? ProgramsPluralArray
        {
            get { return ProgramsPlural?.ToArray(); }
            set { ProgramsPlural = value?.ToList(); }
        }
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

        /// <summary>
        /// Копирует в предоставленный объект поля из данного объекта.
        /// </summary>
        /// <returns>Заполненный объект.</returns>
        public GetProgramsResponse CreateResponse()
        {
            var retVal = new GetProgramsResponse();
            CopyTo(retVal);
            retVal.Programs = ProgramsPlural ?? ProgramsSingular ?? new List<GetProgramsResponse.Program>();
            return retVal;
        }
    }
}
