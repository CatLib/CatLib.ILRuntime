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
using Game.Hotfix.API.TestsBind;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsBind
{
    public class ProviderBind : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderBind");
            if (Make("bind_1") == null)
            {
                Util.Faild("bind_1");
                return;
            }

            if (Make<IBind1>() == null)
            {
                Util.Faild("bind_2");
                return;
            }

            if (Make<IBind2>() == null)
            {
                Util.Faild("bind_3");
                return;
            }

            if (Make<IBind3>() == null)
            {
                Util.Faild("bind_4");
                return;
            }

            if (Make<IBind4>() == null)
            {
                Util.Faild("bind_5");
                return;
            }

            Util.Success("Bind");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderBind");
            return base.CoroutineInit();
        }


        public override void Register()
        {
            Bind<Bind>().Alias("bind_1");
            Bind<IBind1, Bind>();
            Bind<IBind2>(() => new Bind());
            Bind<IBind3>((p) => new Bind());
            Bind<IBind4>((c, p) => new Bind());
        }
    }
}
