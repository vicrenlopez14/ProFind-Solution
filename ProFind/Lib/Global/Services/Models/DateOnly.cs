namespace ProFind.Lib.Global.Services
{
    public partial class DateOnly
    {
        public DateOnly(int? year, int? month, int? day, DayOfWeek? dayOfWeek, int? dayOfYear, int? dayNumber)
        {
            Year = year;
            Month = month;
            Day = day;
            DayOfWeek = dayOfWeek;
            DayOfYear = dayOfYear;
            DayNumber = dayNumber;
        }
    }
}
