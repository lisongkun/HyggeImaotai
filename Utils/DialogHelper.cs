using System;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using NLog;

namespace HyggeIMaoTai.Utils
{
    /// <summary>
    /// 对话框辅助工具类
    /// </summary>
    public static class DialogHelper
    {
        #region Properties

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #endregion
        /// <summary>
        /// 显示加载对话框并执行异步操作
        /// </summary>
        /// <param name="asyncAction">要执行的异步操作</param>
        /// <param name="message">加载提示消息，默认为"正在加载..."</param>
        /// <param name="dialogIdentifier">DialogHost 标识符，默认为"RootDialog"</param>
        /// <returns></returns>
        public static async Task ShowLoadingDialogAsync(
            Func<Task> asyncAction,
            string message = "正在加载...",
            string dialogIdentifier = "RootDialog")
        {
            var loadingDialog = new LoadingDialog();
            loadingDialog.SetMessage(message);

            await DialogHost.Show(loadingDialog, dialogIdentifier, async (object s, DialogOpenedEventArgs args) =>
            {
                try
                {
                    await asyncAction();
                }
                catch (Exception ex)
                {
                    // 关闭对话框后再显示错误消息
                    Logger.Error(ex);
                    args.Session.Close();
                    MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw; // 重新抛出异常，让调用者处理
                }
                finally
                {
                    // 确保对话框被关闭
                    if (!args.Session.IsEnded)
                    {
                        args.Session.Close();
                    }
                }
            });
        }

        /// <summary>
        /// 显示加载对话框并执行异步操作（带返回值）
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="asyncAction">要执行的异步操作</param>
        /// <param name="message">加载提示消息，默认为"正在加载..."</param>
        /// <param name="dialogIdentifier">DialogHost 标识符，默认为"RootDialog"</param>
        /// <returns>异步操作的返回值</returns>
        public static async Task<T> ShowLoadingDialogAsync<T>(
            Func<Task<T>> asyncAction,
            string message = "正在加载...",
            string dialogIdentifier = "RootDialog")
        {
            T result = default(T);
            var loadingDialog = new LoadingDialog();
            loadingDialog.SetMessage(message);

            await DialogHost.Show(loadingDialog, dialogIdentifier, async (object s, DialogOpenedEventArgs args) =>
            {
                try
                {
                    result = await asyncAction();
                }
                catch (Exception ex)
                {
                    // 关闭对话框后再显示错误消息
                    args.Session.Close();
                    MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw; // 重新抛出异常，让调用者处理
                }
                finally
                {
                    // 确保对话框被关闭
                    if (!args.Session.IsEnded)
                    {
                        args.Session.Close();
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// 显示加载对话框并执行异步操作（带错误处理回调）
        /// </summary>
        /// <param name="asyncAction">要执行的异步操作</param>
        /// <param name="onError">错误处理回调，如果为 null 则使用默认错误处理</param>
        /// <param name="message">加载提示消息，默认为"正在加载..."</param>
        /// <param name="dialogIdentifier">DialogHost 标识符，默认为"RootDialog"</param>
        /// <returns></returns>
        public static async Task ShowLoadingDialogAsync(
            Func<Task> asyncAction,
            Action<Exception> onError,
            string message = "正在加载...",
            string dialogIdentifier = "RootDialog")
        {
            var loadingDialog = new LoadingDialog();
            loadingDialog.SetMessage(message);

            await DialogHost.Show(loadingDialog, dialogIdentifier, async (object s, DialogOpenedEventArgs args) =>
            {
                try
                {
                    await asyncAction();
                }
                catch (Exception ex)
                {
                    // 关闭对话框
                    args.Session.Close();
                    
                    // 执行错误处理回调
                    if (onError != null)
                    {
                        onError(ex);
                    }
                    else
                    {
                        // 默认错误处理
                        MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                finally
                {
                    // 确保对话框被关闭
                    if (!args.Session.IsEnded)
                    {
                        args.Session.Close();
                    }
                }
            });
        }
    }
}

