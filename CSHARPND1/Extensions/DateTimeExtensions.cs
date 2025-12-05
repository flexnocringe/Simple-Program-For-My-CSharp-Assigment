using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Extensions
{
    //Praplėstas C# tipas (0,5 t.)
    static partial class DateTimeExtensions
    {
        public static bool IsPastDueDate(this DateTime date, DateTime dueDate) => date > dueDate;
        public static string FormatLithuanian(this DateTime date) => date.ToString("yyyy-MM-dd HH:mm 'h.'", CultureInfo.InvariantCulture);
    }
}
