using maui_planets.Views;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace maui_planets
{
    public partial class App : Application
    {
        const int WindowHeight = 1000;
        const int WindowWidth = 540;

        public App()
        {
            InitializeComponent(); 
            
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
                var mauiWindow = handler.VirtualView;
                var nativeWindow = handler.PlatformView;

                nativeWindow.Activate();

                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
#endif
            });

            MainPage = new StartPage();
        }
    }
}
