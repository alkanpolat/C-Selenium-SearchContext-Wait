using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Program
{
    public class SearchContextWait : DefaultWait<ISearchContext>
    {
        private static TimeSpan DefaultSleepTimeout => TimeSpan.FromMilliseconds(500.0);

        public SearchContextWait(ISearchContext searchContext, TimeSpan timeout)
            : this(new SystemClock(), searchContext, timeout, DefaultSleepTimeout)
        {
        }

        public SearchContextWait(IClock clock, ISearchContext searchContext, TimeSpan timeout, TimeSpan sleepInterval)
        : base(searchContext, clock)
        {
            base.Timeout = timeout;
            base.PollingInterval = sleepInterval;
            IgnoreExceptionTypes(typeof(NotFoundException));
        }
    }
}
