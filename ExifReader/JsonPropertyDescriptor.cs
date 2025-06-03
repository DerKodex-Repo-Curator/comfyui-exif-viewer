using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace ExifReader
{
    public class JsonPropertyDescriptor : PropertyDescriptor
    {
        private readonly object _value;

        public JsonPropertyDescriptor(string name, object value)
            : base(name, null)
        {
            _value = value;
        }

        public override Type PropertyType
        {
            get
            {
                if (_value is JsonPropertyWrapper)
                    return typeof(JsonPropertyWrapper);

                return _value?.GetType() ?? typeof(object);
            }
        }

        public override object GetValue(object component) => _value;

        public override bool IsReadOnly => true;
        public override Type ComponentType => typeof(object);
        public override bool CanResetValue(object component) => false;
        public override void ResetValue(object component) { }
        public override bool ShouldSerializeValue(object component) => false;

        public override void SetValue(object component, object value)
        {
            // Read-only — do nothing
        }
    }
}
