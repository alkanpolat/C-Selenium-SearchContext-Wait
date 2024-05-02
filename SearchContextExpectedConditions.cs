using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace TestProject
{
    public class SearchContextExpectedConditions
    {
        private SearchContextExpectedConditions()
        {
        }

        public static Func<ISearchContext, IWebElement> ElementExists(By locator)
        {
            return (ISearchContext searchContext) => searchContext.FindElement(locator);
        }

        public static Func<ISearchContext, IWebElement> ElementIsVisible(By locator)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    return ElementIfVisible(searchContext.FindElement(locator));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(By locator)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> readOnlyCollection = searchContext.FindElements(locator);
                    if (readOnlyCollection.Any((IWebElement element) => !element.Displayed))
                    {
                        return null;
                    }

                    return readOnlyCollection.Any() ? readOnlyCollection : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(ReadOnlyCollection<IWebElement> elements)
        {
            return delegate
            {
                try
                {
                    if (elements.Any((IWebElement element) => !element.Displayed))
                    {
                        return null;
                    }

                    return elements.Any() ? elements : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, ReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocatedBy(By locator)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> readOnlyCollection = searchContext.FindElements(locator);
                    return readOnlyCollection.Any() ? readOnlyCollection : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, bool> TextToBePresentInElement(IWebElement element, string text)
        {
            return delegate
            {
                try
                {
                    return element.Text.Contains(text);
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            };
        }

        public static Func<ISearchContext, bool> TextToBePresentInElementLocated(By locator, string text)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    return searchContext.FindElement(locator).Text.Contains(text);
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            };
        }

        public static Func<ISearchContext, bool> TextToBePresentInElementValue(IWebElement element, string text)
        {
            return delegate
            {
                try
                {
                    return element.GetAttribute("value")?.Contains(text) ?? false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            };
        }

        public static Func<ISearchContext, bool> TextToBePresentInElementValue(By locator, string text)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    return searchContext.FindElement(locator).GetAttribute("value")?.Contains(text) ?? false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            };
        }

        public static Func<ISearchContext, bool> InvisibilityOfElementLocated(By locator)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    return !searchContext.FindElement(locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        public static Func<ISearchContext, bool> InvisibilityOfElementWithText(By locator, string text)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    string text2 = searchContext.FindElement(locator).Text;
                    if (string.IsNullOrEmpty(text2))
                    {
                        return true;
                    }

                    return !text2.Equals(text);
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        public static Func<ISearchContext, IWebElement> ElementToBeClickable(By locator)
        {
            return delegate (ISearchContext searchContext)
            {
                IWebElement webElement = ElementIfVisible(searchContext.FindElement(locator));
                try
                {
                    if (webElement != null && webElement.Enabled)
                    {
                        return webElement;
                    }

                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, IWebElement> ElementToBeClickable(IWebElement element)
        {
            return delegate
            {
                try
                {
                    if (element != null && element.Displayed && element.Enabled)
                    {
                        return element;
                    }

                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, bool> StalenessOf(IWebElement element)
        {
            return delegate
            {
                try
                {
                    return element == null || !element.Enabled;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            };
        }

        public static Func<ISearchContext, bool> ElementToBeSelected(IWebElement element)
        {
            return ElementSelectionStateToBe(element, selected: true);
        }

        public static Func<ISearchContext, bool> ElementToBeSelected(IWebElement element, bool selected)
        {
            return (ISearchContext searchContext) => element.Selected == selected;
        }

        public static Func<ISearchContext, bool> ElementSelectionStateToBe(IWebElement element, bool selected)
        {
            return (ISearchContext searchContext) => element.Selected == selected;
        }

        public static Func<ISearchContext, bool> ElementToBeSelected(By locator)
        {
            return ElementSelectionStateToBe(locator, selected: true);
        }

        public static Func<ISearchContext, bool> ElementSelectionStateToBe(By locator, bool selected)
        {
            return delegate (ISearchContext searchContext)
            {
                try
                {
                    return searchContext.FindElement(locator).Selected == selected;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            if (!element.Displayed)
            {
                return null;
            }

            return element;
        }
    }
}
