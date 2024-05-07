using System.ComponentModel;
using System.Globalization;

namespace System
{
    public class Vector2fConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Vector2f))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Vector2f)
            {
                Vector2f vector2f = (Vector2f)value;
                return "<" + vector2f.X + "," + vector2f.Y + ">";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string obj = (string)value;
                    obj.Trim();
                    string[] array = obj.Replace("<", "").Replace(">", "").Split(new char[1] { ',' });
                    if (array.Length != 2)
                    {
                        throw new ArgumentException();
                    }

                    Vector2f vector2f = default(Vector2f);
                    for (int i = 0; i < Vector2f.GetSize(); i++)
                    {
                        vector2f[i] = Convert.ToSingle(array[i]);
                    }

                    return vector2f;
                }
                catch
                {
                    throw new ArgumentException("Can not convert " + (string)value + " to type Vector2f.");
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}