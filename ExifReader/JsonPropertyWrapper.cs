using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace ExifReader
{
    public class JsonPropertyWrapper : ICustomTypeDescriptor
    {
        private readonly Dictionary<string, object> _properties;

        public JsonPropertyWrapper(JObject jObject)
        {
            _properties = new Dictionary<string, object>();

            foreach (var property in jObject.Properties())
            {
                var value = property.Value;

                if (value.Type == JTokenType.Object)
                {
                    _properties[property.Name] = new JsonPropertyWrapper((JObject)value);
                }
                else if (value.Type == JTokenType.Array)
                {
                    _properties[property.Name] = value.ToString(); // or implement array support
                }
                else
                {
                    _properties[property.Name] = ((JValue)value).Value;
                }
            }
        }

        public override string ToString()
        {
            return "[Object]";

        }
        public PropertyDescriptorCollection GetProperties()
        {
            var descriptors = new List<PropertyDescriptor>();
            foreach (var kvp in _properties)
            {
                descriptors.Add(new JsonPropertyDescriptor(kvp.Key, kvp.Value));
            }
            return new PropertyDescriptorCollection(descriptors.ToArray());
        }

        // ICustomTypeDescriptor boilerplate
        public AttributeCollection GetAttributes() => AttributeCollection.Empty;
        public string GetClassName() => null;
        public string GetComponentName() => null;
        public TypeConverter GetConverter() => null;
        public EventDescriptor GetDefaultEvent() => null;
        public PropertyDescriptor GetDefaultProperty() => null;
        public object GetEditor(Type editorBaseType) => null;
        public EventDescriptorCollection GetEvents(Attribute[] attributes) => EventDescriptorCollection.Empty;
        public EventDescriptorCollection GetEvents() => EventDescriptorCollection.Empty;
        public object GetPropertyOwner(PropertyDescriptor pd) => this;
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => GetProperties();
    }
}
