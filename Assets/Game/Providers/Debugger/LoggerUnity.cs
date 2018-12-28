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

using Game.API.Debugger;
using Game.HelloWorld;
using UnityEngine;

namespace Game.Debugger
{
    public class LoggerUnity : IDebugger
    {
        public LoggerUnity(IHelloWorld helloworld)
        {
            helloworld.Say();
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}
