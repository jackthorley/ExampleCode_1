using System;

namespace App
{
    //This was lifted from some work I did for a project to help with testing times and make them more consistant
    internal static class SystemTime
    {
        private static readonly Func<DateTime> _machineNowProvider = () => DateTime.Now;
        private static readonly Func<DateTime> _machineUtcNowProvider = () => DateTime.UtcNow;

        private static Func<DateTime> _currentUtcNowProvider = _machineUtcNowProvider;
        private static Func<DateTime> _currentNowProvider = _machineNowProvider;

        public static Func<DateTime> UtcNowProvider
        {
            get { return _currentUtcNowProvider; }
            set { _currentUtcNowProvider = value; }
        }

        public static Func<DateTime> NowProvider
        {
            get { return _currentNowProvider; }
            set { _currentNowProvider = value; }
        }

        public static DateTime Now { get { return NowProvider(); } }

        public static DateTime UtcNow { get { return UtcNowProvider(); } }

        public static void Reset()
        {
            _currentUtcNowProvider = _machineUtcNowProvider;
            _currentNowProvider = _machineNowProvider;
        }
    }
}
