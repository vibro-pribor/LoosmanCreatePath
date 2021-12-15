using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VP.Xml.Serialization
{
    /// <summary>
    /// Аналог стандартного Dictionary, с возможность xml-сериализации
    /// </summary>
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
		/// <summary>
		/// Инициализирует новый пустой экземпляр класса SerializableDictionary&lt;TKey, TValue&gt;, 
		/// имеющий начальную емкость по умолчанию и использующий функцию сравнения по умолчанию,
		/// проверяющую равенство для данного типа ключа
		/// </summary>
		public SerializableDictionary()
        {
 
        }
 
		/// <summary>
		/// Инициализирует новый экземпляр класса SerializableDictionary&lt;TKey, TValue&gt;,
		/// который содержит элементы, скопированные из заданной коллекции  
		/// System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;,
		/// и использует функцию сравнения по умолчанию, проверяющую равенство для данного типа ключа
		/// </summary>
		/// <param name="dictionary">Объект System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;,
		/// элементы которого копируются в новый объект SerializableDictionary&lt;TKey, TValue&gt;</param>
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
 
        }
        
		/// <summary>
		/// Инициализирует новый пустой экземпляр класса SerializableDictionary&lt;TKey, TValue&gt; 
		/// с начальной емкостью по умолчанию, использующий указанную функцию сравнения 
		/// IEqualityComparer&lt;TKey&gt;
		/// </summary>
		/// <param name="comparer">Реализация IEqualityComparer&lt;TKey&gt;, 
		/// которую следует использовать при сравнении ключей, или null, 
		/// если для данного типа ключа должна использоваться реализация 
		/// EqualityComparer&lt;TKey&gt; по умолчанию</param>
        public SerializableDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
 
        }
        
		/// <summary>
		/// Инициализирует новый пустой экземпляр класса SerializableDictionary&lt;TKey, TValue&gt;,
		/// имеющий заданную начальную емкость и использующий функцию сравнения по умолчанию,
		/// проверяющую равенство для данного типа ключа
		/// </summary>
		/// <param name="capacity">Начальное количество элементов, которое может содержать
		/// коллекция SerializableDictionary&lt;TKey, TValue&gt;</param>
        public SerializableDictionary(int capacity)
            : base(capacity)
        {
 
        }
        
		/// <summary>
		/// Инициализирует новый экземпляр класса SerializableDictionary&lt;TKey, TValue&gt;,
		/// который содержит элементы, скопированные из заданной коллекции IDictionary&lt;TKey, TValue&gt;,
		/// и использует указанный интерфейс IEqualityComparer&lt;TKey&gt;.
		/// </summary>
		/// <param name="dictionary">Объект IDictionary&lt;TKey, TValue&gt;,
		/// элементы которого копируются в новый объект SerializableDictionary&lt;TKey, TValue&gt;</param>
		/// <param name="comparer">Реализация IEqualityComparer&lt;TKey&gt;, 
		/// которую следует использовать при сравнении ключей, или null, 
		/// если для данного типа ключа должна использоваться реализация 
		/// EqualityComparer&lt;TKey&gt; по умолчанию</param>
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
 
        }
        
		/// <summary>
		/// Инициализирует новый пустой экземпляр класса SerializableDictionary&lt;TKey, TValue&gt;
		/// c заданной начальной емкостью, использующий указанную функцию сравнения IEqualityComparer&lt;TKey&gt;
		/// </summary>
		/// <param name="capacity">Начальное количество элементов, которое может содержать
		/// коллекция SerializableDictionary&lt;TKey, TValue&gt;</param>
		/// <param name="comparer">Реализация IEqualityComparer&lt;TKey&gt;, 
		/// которую следует использовать при сравнении ключей, или null, 
		/// если для данного типа ключа должна использоваться реализация 
		/// EqualityComparer&lt;TKey&gt; по умолчанию</param>
        public SerializableDictionary(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
 
        }

		/// <summary>
		/// Инициализирует новый экземпляр класса SerializableDictionary&lt;TKey, TValue&gt; с сериализованными данными
		/// </summary>
		/// <param name="info">Объект System.Runtime.Serialization.SerializationInfo,
		/// который содержит сведения, требуемые для сериализации коллекции 
		/// SerializableDictionary&lt;TKey, TValue&gt;</param>
		/// <param name="context">Структура System.Runtime.Serialization.StreamingContext,
		/// содержащая исходный объект и объект назначения для сериализованного потока,
		/// связанного с коллекцией SerializableDictionary&lt;TKey, TValue&gt;</param>
		protected SerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
 
        }
 
        #region IXmlSerializable Members

		/// <summary>
		/// Получение XmlSchema, описывающей представление XML объекта,
		/// полученного из метода WriteXml и включенного в метод ReadXml
		/// </summary>
		/// <remarks>Этот метод является зарезервированным, и его не следует использовать</remarks> 
		/// <returns>XmlSchema, описывающая представление XML объекта,
		/// полученного из метода WriteXml и включенного в метод ReadXml</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

		/// <summary>
		///  Создает объект из представления XML
		/// </summary>
		/// <param name="reader">Поток XmlReader, из которого выполняется десериализация объекта</param>
        public void ReadXml(XmlReader reader)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty) return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                var key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                var value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

		/// <summary>
		/// Преобразует объект в представление XML
		/// </summary>
		/// <param name="writer">Поток XmlWriter, в который выполняется сериализация объекта</param>
        public void WriteXml(XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}