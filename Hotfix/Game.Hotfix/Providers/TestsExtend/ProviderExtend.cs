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
using Game.Hotfix.API.TestsExtend;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsExtend
{
    public class ProviderExtend : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderExtend");

            var extend = Make<IExtend>();
            if (extend.Name != "Extend")
            {
                Util.Faild("Extend_1");
                return;
            }

            var extend2 = Make<IExtend2>();
            if (extend2.Name2 != "hello")
            {
                Util.Faild("Extend_2");
                return;
            }

            Util.Success("Extend"); 
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderExtend");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Singleton<IExtend, Extend>();
            Singleton<IExtend2, Extend>();

            Extend<IExtend, IExtend>((instance) =>
            {
                instance.Name = "Extend";
                return instance;
            });

            Extend<IExtend, IExtend>((instance, c) => instance);

            Extend<IExtend2>((instance) =>
            {
                instance.Name2 = "hello";
                return instance;
            });

            Extend<IExtend2>((instance, c) => instance);
        }
    }
}
