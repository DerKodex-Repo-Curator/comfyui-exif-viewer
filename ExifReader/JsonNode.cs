using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace ExifReader
{
    [TypeConverter(typeof(JsonNodeConverter))]
    public class JsonNode
    {
        public Dictionary<string, object> Properties { get; } = new();

        public JsonNode(JObject obj)
        {
            foreach (var prop in obj.Properties())
            {
                var val = prop.Value;

                object value = val.Type switch
                {
                    JTokenType.Object => new JsonNode((JObject)val),
                    JTokenType.Array => val.ToString(), // Can be improved
                    JTokenType.Integer => val.Value<long>(),
                    JTokenType.Float => val.Value<double>(),
                    JTokenType.Boolean => val.Value<bool>(),
                    JTokenType.String => val.Value<string>(),
                    _ => val.ToString()
                };

                Properties[prop.Name] = value;
            }
        }
    }

    public class JsonNodeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var descriptors = new List<PropertyDescriptor>();

            if (value is JsonNode node)
            {
                foreach (var kvp in node.Properties)
                {
                    descriptors.Add(new JsonNodePropertyDescriptor(kvp.Key, kvp.Value));
                }
            }
            else if (value is JsonRootNode root)
            {
                foreach (var kvp in root.RootObjects)
                {
                    descriptors.Add(new JsonNodePropertyDescriptor(kvp.Key, kvp.Value));
                }
            }

            return new PropertyDescriptorCollection(descriptors.ToArray());
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is JsonNode)
            {
                var castValue = value as JsonNode;
                if (castValue.Properties.ContainsKey("class_type"))
                {
                    return castValue.Properties["class_type"];
                }
                return $"[Object] ({GetType().Name})";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class JsonNodePropertyDescriptor : PropertyDescriptor
    {
        private readonly object _value;

        public JsonNodePropertyDescriptor(string name, object value)
            : base(name, null)
        {
            _value = value;
        }

        public override Type PropertyType => _value?.GetType() ?? typeof(object);

        public override object GetValue(object component) => _value;

        public override void SetValue(object component, object value) { }

        public override bool IsReadOnly => true;

        public override Type ComponentType => typeof(object);

        public override bool CanResetValue(object component) => false;

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) => false;
    }

    [TypeConverter(typeof(JsonNodeConverter))]
    public class JsonRootNode
    {
        public Dictionary<string, object> RootObjects { get; } = new();

        public JsonRootNode(JObject root)
        {
            foreach (var prop in root.Properties())
            {
                if (prop.Value is JObject obj)
                {
                    var test = new JsonNode(obj);
                    if (test.Properties.ContainsKey("class_type"))
                    {
                        var propName = $"{test.Properties["class_type"]}_{prop.Name}";
                        RootObjects[propName] = new JsonNode(obj);
                    }
                    else
                    {
                        RootObjects[prop.Name] = new JsonNode(obj);
                    }
                }
                else
                {
                    RootObjects[prop.Name] = prop.Value.ToString();
                }
            }
        }
    }
}
