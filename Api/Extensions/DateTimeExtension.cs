using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class DateTimeExtension
    {
        public static int GetYearFromUtcNow(this DateOnly date) {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            int age = now.Year - date.Year;


            if(date > now.AddYears(-age))
                age--;

            return age;
        }
    }
}