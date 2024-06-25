using Android.App;
using Android.Runtime;

namespace TechnicalServices
{
    // connect to local service on the
    // emulator's host for debugging,
    // access via http://10.0.2.2

    [Application(UsesCleartextTraffic = true)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
