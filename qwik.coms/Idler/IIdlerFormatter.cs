using System;

namespace qwik.coms.idler
{
    public interface IIdlerFormatter
    {
        string Format(TimeSpan idleDuration);
    }
}