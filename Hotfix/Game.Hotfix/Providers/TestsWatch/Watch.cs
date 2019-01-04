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

using Game.Hotfix.API.TestsWatch;

namespace Game.Hotfix.TestsWatch
{
    public class Watch : IWatch
    {
        public bool isOk = false;
        public void WatchOk()
        {
            isOk = true;
        }
    }
}
