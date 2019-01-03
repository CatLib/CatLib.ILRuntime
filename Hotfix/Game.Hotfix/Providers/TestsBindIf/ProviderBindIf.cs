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
using Game.Hotfix.API.TestsBindIf;
using static CatLib.App;

namespace Game.Hotfix.TestsBindIf
{
    public class ProviderBindIf : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderBindIf");
            if (Make<BindIf>() == null)
            {
                Util.Faild("BindIf_7");
                return;
            }

            if (Make<IBindIf1>() == null)
            {
                Util.Faild("BindIf_8");
                return;
            }

            if (Make<IBindIf2>() == null)
            {
                Util.Faild("BindIf_9");
                return;
            }

            if (Make<IBindIf3>() == null)
            {
                Util.Faild("BindIf_7");
                return;
            }

            if (Make<IBindIf4>() == null)
            {
                Util.Faild("BindIf_10");
                return;
            }

            if (Make("abc") == null)
            {
                Util.Faild("BindIf_11");
                return;
            }

            Util.Success("BindIf");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderBindIf");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderBindIf");
            if (!BindIf<BindIf>(out IBindData bindData) || bindData == null)
            {
                Util.Faild("BindIf_1");
            }

            if (!BindIf<IBindIf1, BindIf>(out bindData) || bindData == null)
            {
                Util.Faild("BindIf_2");
            }

            if (!BindIf<IBindIf2>(() => new BindIf(), out bindData) || bindData == null)
            {
                Util.Faild("BindIf_3");
            }

            if (!BindIf<IBindIf3>((p) => new BindIf(), out bindData) || bindData == null)
            {
                Util.Faild("BindIf_4");
            }

            if (!BindIf<IBindIf4>((c,p) => new BindIf(), out bindData) || bindData == null)
            {
                Util.Faild("BindIf_5");
            }

            if (!BindIf("abc", (c, p) => new BindIf(), out bindData) || bindData == null)
            {
                Util.Faild("BindIf_6");
            }
        }
    }
}
