using System.ComponentModel;
using System.Globalization;

namespace System
{
    public class Vector3fConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Vector3f))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Vector3f)
            {
                Vector3f vector3f = (Vector3f)value;
                return "<" + vector3f.X + "," + vector3f.Y + "," + vector3f.Z + ">";
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
                    if (array.Length != 3)
                    {
                        throw new ArgumentException();
                    }

                    Vector3f vector3f = default(Vector3f);
                    for (int i = 0; i < Vector3f.GetSize(); i++)
                    {
                        vector3f[i] = Convert.ToSingle(array[i]);
                    }

                    return vector3f;
                }
                catch
                {
                    throw new ArgumentException("Can not convert " + (string)value + " to type Vector3f.");
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}