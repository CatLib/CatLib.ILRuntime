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
using Game.Hotfix.API.TestsTag;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsTag
{
    public class ProviderTag : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderTag");

            var service = Tagged("abc");

            if (service.Length != 1)
            {
                Util.Faild("Tag_1");
                return;
            }

            Util.Success("Tag");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderTag");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderTag");
            Singleton<ITag, Tag>();
            Tag<ITag>("abc");
        }
    }
}
