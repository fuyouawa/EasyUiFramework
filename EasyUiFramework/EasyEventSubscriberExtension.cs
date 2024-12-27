using System.Windows;
using EasyFramework;

namespace EasyUiFramework
{
    /// <summary>
    /// 
    /// </summary>
    public static class EasyEventSubscriberExtension
    {
        /// <summary>
        /// <para>注册target中所有事件处理器（标记了EasyEventHandler特性的成员函数）</para>
        /// <para>确保会在Ui线程触发</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="includeBaseClass">包含基类的事件处理器</param>
        /// <param name="triggerExtension">事件处理器的触发行为扩展</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IUnRegister RegisterEasyEventSubscriberInUiThread<T>(this T target,
            bool includeBaseClass = false,
            EasyEventTriggerExtensionDelegate? triggerExtension = null) where T : FrameworkElement, IEasyEventSubscriber
        {
            return target.RegisterEasyEventSubscriber(includeBaseClass, invoker =>
            {
                void Call() => target.Dispatcher.BeginInvoke(invoker);
                if (triggerExtension != null)
                {
                    triggerExtension(Call);
                }
                else
                {
                    Call();
                }
            });
        }

        /// <summary>
        /// <para>注册事件处理器</para>
        /// <para>确保会在Ui线程触发</para>
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="handler"></param>
        /// <param name="triggerExtension">事件处理器的触发行为扩展</param>
        public static IUnRegister RegisterEasyEventHandlerInUiThread<T, TEvent>(this T target,
            EasyEventHandlerDelegate<TEvent> handler,
            EasyEventTriggerExtensionDelegate? triggerExtension = null) where T : FrameworkElement, IEasyEventSubscriber
        {
            return target.RegisterEasyEventHandler(handler, invoker =>
            {
                void Call() => target.Dispatcher.Invoke(invoker);
                if (triggerExtension != null)
                {
                    triggerExtension(Call);
                }
                else
                {
                    Call();
                }
            });
        }
    }
}