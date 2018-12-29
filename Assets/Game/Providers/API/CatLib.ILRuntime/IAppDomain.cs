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

namespace CatLib.API.ILRuntime
{
    /// <summary>
    /// App Domain
    /// </summary>
    public interface IAppDomain
    {
        /// <summary>
        /// 调用热更新中的方法
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="method">调用方法</param>
        /// <param name="instance">类型实例</param>
        /// <param name="params">传递参数</param>
        /// <returns></returns>
        object Invoke(string type, string method, object instance, params object[] @params);

        /// <summary>
        /// 创建热更新工程中的实例
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="args">构造函数参数</param>
        /// <returns></returns>
        object CreateInstance(string type, object[] args = null);
    }
}
