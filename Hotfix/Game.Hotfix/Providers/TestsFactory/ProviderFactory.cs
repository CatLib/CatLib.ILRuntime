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
using Game.Hotfix.API.TestsFactory;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsFactory
{
    public class ProviderFactory : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderFactory");

            var factory = Factory<IFactory>();
            var instance = factory();

            if (instance == null)
            {
                Util.Faild("Factory_1");
                return;
            }

            if (instance.Name != "hello world")
            {
                Util.Faild("Factory_2");
                return;
            }

            Util.Success("Factory");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderFactory");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderFactory");
            Singleton<IFactory, Factory>();
        }
    }
}
