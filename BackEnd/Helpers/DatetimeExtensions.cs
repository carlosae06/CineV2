using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Helpers
{
    public static class DatetimeExtensions
    {

        //propiedad fecha local
        public static DateTime LocalTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local);
        }

        public static DateTime FechaHoraLocal(this DateTime date)
        {

            var timeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.Id == "SA Western Standard Time");
            date = DateTime.UtcNow.Add(timeZone!.BaseUtcOffset);
            return date;
        }


        public static DateTime FechaHoraZona(this DateTime date, string Zona)
        {

            var timeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.Id == Zona);
            date = date.Add(timeZone!.BaseUtcOffset);
            return date;
        }

        public static long toDateLong(this DateTime pfecha)
        {
            string xCadena = pfecha.Year.ToString() + NumeroCadena(pfecha.Month) + NumeroCadena(pfecha.Day);

            return long.Parse(xCadena);

        }

        public static string toDateStringInvoice(this DateTime pfecha)
        {
            string xCadena = NumeroCadena(pfecha.Day) + "/" + NumeroCadena(pfecha.Month) + "/" + NumeroCadena(pfecha.Year);

            return xCadena;

        }

        public static string NumeroCadena(long num)
        {
            if (num < 10)
            {
                return "0" + num;
            }
            else
                return num.ToString();
        }


    }
}
