/*
 * This file is part of the CatLib package.
 *
 * (c) CatLib <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: http://catlib.io/
 */

using CatLib;
using Game.API.Debugger;
using Game.HelloWorld;
using UnityEngine;

namespace Game.Hotfix
{
    public interface ITestHotfix
    {
        void Log(string message);
    }

    /// <summary>
    /// 程序入口
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="application">Application</param>
        public static void Main(IApplication application)
        {
            Debug.Log("hello hotfix");

            var binder = App.GetBind<IDebugger>();
            binder.Unbind();

            Debug.Log(App.Handler);
            Debug.Log(App.DebugLevel);

            App.OnNewApplication += (app) =>
            {
                Debug.Log("OnNewApplication:" + app);
            };

            App.Singleton<IDebugger, DebuggeUnity>();
            App.Make<IDebugger>().Log("hello in this hotfix");

            App.Singleton<DebuggerUnity2>();
            App.Singleton<ITestHotfix, TestHotFix>().Needs<IHotFixDebugger>()
                .Given<DebuggerUnity2>();
            
            App.Make<ITestHotfix>().Log("From HotFix");
        }

        public class DebuggeUnity : IDebugger
        {
            public DebuggeUnity(IHelloWorld helloworld)
            {
                helloworld.Say();
            }

            public void Log(string message)
            {
                Debug.Log("hotfix : " + message);
            }
        }

        public class DebuggerUnity2 : IHotFixDebugger
        {
            public DebuggerUnity2(IHelloWorld helloworld)
            {
                helloworld.Say();
            }

            public void Log(string message)
            {
                Debug.Log("hotfix2222222222 : " + message);
            }
        }

        public class TestHotFix : ITestHotfix
        {
            public TestHotFix(IHotFixDebugger debugger)
            {
                debugger.Log("TestHotFix:Debugger!!!!!");
            }
            public void Log(string message)
            {
                Debug.Log("This is Test Hot Fix : " + message);
            }
        }

        public interface IHotFixDebugger
        {
            void Log(string message);
        }

        public static IBindData Alias<TAlias>(this IBindData bindData)
        {
            return bindData.Alias(App.Type2Service(typeof(TAlias)));
        }
    }
}
