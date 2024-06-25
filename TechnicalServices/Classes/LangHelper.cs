using System.Reflection;
using System.Resources;

namespace TechnicalServices.Classes
{
    public static class LangHelper
    {
        private static ResourceManager _rm;
        static LangHelper()
        {
            _rm = new ResourceManager("TechnicalServices.Resources.Languages.AppResources", Assembly.GetExecutingAssembly());
        }
        public static string? GetString(string name)
        {
            return _rm.GetString(name);
        }
    }
}
