using Litehtml.Services;
using NUnit.Framework;

namespace Litehtml
{
    public class api_LocationTest
    {
        [Test]
        public void Test()
        {
            var ctx = new context();
            ctx.load_master_stylesheet(Resources.master_css);
        }
    }
}
