namespace Olekstra.LikePharma.Client.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Olekstra.LikePharma.Client.Attributes;

    /// <summary>
    /// Вспомогательная структура ответа на запрос <see cref="GetProgramsRequest"/> с двумя написания вариантами: program и programs.
    /// </summary>
    [XmlRoot("get_programs_response")]
    public class GetProgramsResponseHelper : ResponseBase
    {
        /// <summary>
        /// Список активных программ (единственое число).
        /// </summary>
        [EmptyCollectionWithoutEmptyElements]
        [JsonPropertyName("program")]
        [XmlArray("program")]
        [XmlArrayItem("program")]
        public List<GetProgramsResponse.Program> ProgramsSingular { get; set; } = new List<GetProgramsResponse.Program>();

        /// <summary>
        /// Список активных программ (множественое число).
        /// </summary>
        [EmptyCollectionWithoutEmptyElements]
        [JsonPropertyName("programs")]
        [XmlArray("programs")]
        [XmlArrayItem("program")]
        public List<GetProgramsResponse.Program> ProgramsPlural { get; set; } = new List<GetProgramsResponse.Program>();

        /// <summary>
        /// Копирует в себя значения полей переданного объекта.
        /// </summary>
        /// <param name="source">Исходный объект, поля которого надо скопировать.</param>
        /// <exception cref="ArgumentNullException">Если в параметре 'source' передано значение <b>null</b>.</exception>
        public void CopyFrom(GetProgramsResponse source)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            base.CopyFrom(source);
            ProgramsSingular = source.Programs;
            ProgramsPlural = source.Programs;
        }

        /// <summary>
        /// Копирует в предоставленный объект поля из данного объекта.
        /// </summary>
        /// <returns>Заполненный объект.</returns>
        public GetProgramsResponse CreateResponse()
        {
            var retVal = new GetProgramsResponse();
            CopyTo(retVal);
            retVal.Programs = this.ProgramsPlural.Count > 0 ? this.ProgramsPlural : this.ProgramsSingular;
            return retVal;
        }
    }
}
