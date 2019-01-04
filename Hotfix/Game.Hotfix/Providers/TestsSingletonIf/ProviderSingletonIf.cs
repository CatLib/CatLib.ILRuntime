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

using System.Collections;
using CatLib;
using Game.Hotfix.API.TestsSingletonIf;
using static CatLib.App;

namespace Game.Hotfix.TestsSingletonIf
{
    public class ProviderSingletonIf : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderSingletonIf");
            if (Make<SingletonIf>() == null)
            {
                Util.Faild("SingletonIf_7");
                return;
            }

            if (Make<ISingletonIf1>() == null)
            {
                Util.Faild("SingletonIf_8");
                return;
            }

            if (Make<ISingletonIf2>() == null)
            {
                Util.Faild("SingletonIf_9");
                return;
            }

            if (Make<ISingletonIf3>() == null)
            {
                Util.Faild("SingletonIf_7");
                return;
            }

            if (Make<ISingletonIf4>() == null)
            {
                Util.Faild("SingletonIf_10");
                return;
            }

            if (Make("abc-singletonif") == null)
            {
                Util.Faild("SingletonIf_11");
                return;
            }

            Util.Success("SingletonIf");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderSingletonIf");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderSingletonIf");
            if (!SingletonIf<SingletonIf>(out IBindData bindData) || bindData == null)
            {
                Util.Faild("SingletonIf_1");
            }

            if (!SingletonIf<ISingletonIf1, SingletonIf>(out bindData) || bindData == null)
            {
                Util.Faild("SingletonIf_2");
            }

            if (!SingletonIf<ISingletonIf2>(() => new SingletonIf(), out bindData) || bindData == null)
            {
                Util.Faild("SingletonIf_3");
            }

            if (!SingletonIf<ISingletonIf3>((p) => new SingletonIf(), out bindData) || bindData == null)
            {
                Util.Faild("SingletonIf_4");
            }

            if (!SingletonIf<ISingletonIf4>((c, p) => new SingletonIf(), out bindData) || bindData == null)
            {
                Util.Faild("SingletonIf_5");
            }

            if (!SingletonIf("abc-singletonif", (c, p) => new SingletonIf(), out bindData) || bindData == null)
            {
                Util.Faild("SingletonIf_6");
            }
        }
    }
}
