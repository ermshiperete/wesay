using System.ComponentModel;
using NUnit.Framework;

namespace WeSay.Foundation.Tests
{
    [TestFixture]
    public class MultiTextTests
    {
        private bool _gotHandlerNotice;

        [SetUp]
        public void Setup() {}

        [Test]
        public void Notification()
        {
            _gotHandlerNotice = false;
            MultiText text = new MultiText();
            text.PropertyChanged += propertyChangedHandler;
            text.SetAlternative("zox", "");
            Assert.IsTrue(_gotHandlerNotice);
        }

        private void propertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            _gotHandlerNotice = true;
        }

        [Test]
        public void MergedGuyHasCorrectParentsOnForms()
        {
            MultiText x = new MultiText();
            x["a"] = "alpha";
            MultiText y = new MultiText();
            y["b"] = "beta";
            x.MergeIn(y);
            Assert.AreSame(y, y.Find("b").Parent);
            Assert.AreSame(x, x.Find("b").Parent);
        }
    }
}