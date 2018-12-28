/*
 * This file is part of the CatLib package.
 *
 * (c) CatLib <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: https://catlib.io/
 */

using CatLib;
using UnityEngine;

namespace Game.HelloWorld
{
    public interface IHelloWorld
    {
        void Say();
    }

    public class HelloWorld : IHelloWorld
    {
        public void Say()
        {
            Debug.Log("Hello world , in main");
        }
    }
    public class ProviderHelloWorld : ServiceProvider
    {
        public override void Register()
        {
            App.Singleton<IHelloWorld, HelloWorld>();
        }
    }
}
