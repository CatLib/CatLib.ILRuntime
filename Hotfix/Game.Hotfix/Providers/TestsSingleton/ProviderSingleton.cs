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
using Game.Hotfix.API.TestsSingleton;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsSingleton
{
    public class ProviderSingleton : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderSingleton");
            if (Make("singleton_1") == null)
            {
                Util.Faild("singleton_1");
                return;
            }

            if (Make<ISingleton1>() == null)
            {
                Util.Faild("singleton_2");
                return;
            }

            if (Make<ISingleton2>() == null)
            {
                Util.Faild("singleton_3");
                return;
            }

            if (Make<ISingleton3>() == null)
            {
                Util.Faild("singleton_4");
                return;
            }

            if (Make<ISingleton4>() == null)
            {
                Util.Faild("singleton_5");
                return;
            }

            Util.Success("Singleton");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderSingleton");
            return base.CoroutineInit();
        }


        public override void Register()
        {
            Singleton<Singleton>().Alias("singleton_1");
            Singleton<ISingleton1, Singleton>();
            Singleton<ISingleton2>(() => new Singleton());
            Singleton<ISingleton3>((p) => new Singleton());
            Singleton<ISingleton4>((c, p) => new Singleton());
        }
    }
}
