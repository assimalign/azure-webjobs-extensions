using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.Http.Utilities
{
    internal static class TypeConverter
    {
        public static dynamic ToType(this string value, Type type, bool conversionException = false)
        {
            try
            {
                switch (type.Name)
                {
                    case nameof(String):
                        return value.ToString();

                    case nameof(Decimal):
                        return Decimal.Parse(value.ToString());

                    case nameof(Double):
                        return Double.Parse(value.ToString());

                    case nameof(Single):
                        return Single.Parse(value.ToString());

                    case nameof(Int16):
                        return Int16.Parse(value.ToString());

                    case nameof(Int32):
                        return Int32.Parse(value.ToString());

                    case nameof(Int64):
                        return  Int64.Parse(value.ToString());

                    case nameof(Boolean):
                        return  bool.Parse(value.ToString());

                    case nameof(DateTime):
                        return DateTime.Parse(value.ToString());

                    case nameof(TimeSpan):
                        return TimeSpan.Parse(value.ToString());
                }

                return null;
            }
            catch (Exception exception)
            {
                if (!conversionException)
                    throw exception;

                return null;
            }
        }
    }
}
