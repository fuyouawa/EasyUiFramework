using System.Windows;
using EasyFramework;

namespace EasyUiFramework
{
    public static class UnRegisterExtension
    {
        public static IUnRegister UnRegisterWhenUnloaded(
            this IUnRegister unRegister,
            FrameworkElement element)
        {
            element.Unloaded += (s, e) => { unRegister.UnRegister(); };
            return unRegister;
        }
    }
}
