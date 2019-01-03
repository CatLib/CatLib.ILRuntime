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
            var bind_1 = Make("bind_1");
            if (bind_1 == null)
            {
                Util.Faild("bind_1");
                return;
            }

            var bind_2 = Make<IBind>();
            if (bind_2 == null)
            {
                Util.Faild("bind_2");
                return;
            }

            var bind_3 = Make<IBind2>();
            if (bind_3 == null)
            {
                Util.Faild("bind_3");
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
            Bind<IBind, Bind>();
            Bind<IBind2>(() => new Bind());
            // TODO: public static IBindData Bind<TService>(Func<object[], object> concrete)
            // TODO: public static IBindData Bind<TService>(Func<IContainer, object[], object> concrete)
        }
    }
}
