using System;
using System.Text;

namespace qwik.coms.idler
{
    public class IdlerFormatter : IIdlerFormatter
    {
        public string Format(TimeSpan idleDuration)
        {
            if (Math.Abs(idleDuration.TotalMinutes) < 1) return "less than a minute";

            var time = string.Empty;
            time = AddTimePart(() => idleDuration.Days, "d", time);
            time = AddTimePart(() => idleDuration.Hours, "h", time);
            time = AddTimePart(() => idleDuration.Minutes, "m", time);

            return time;
        }

        private string AddTimePart(Func<int> timePart, string label, string time)
        {
            if (string.IsNullOrEmpty(time)) time = string.Empty;
            var builder = new StringBuilder(time);
            if (timePart() != 0)
            {
                if (!string.IsNullOrEmpty(time)) builder.Append(" ");
                builder.Append(timePart() + label);
            }
            return builder.ToString();
        }
    }
}