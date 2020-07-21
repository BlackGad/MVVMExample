using System.Windows;

namespace MVVMExample.Components
{
    public interface ICustomDataTemplate
    {
        #region Properties

        string Description { get; }
        double? DesignHeight { get; }
        double? DesignWidth { get; }

        #endregion

        #region Members

        FrameworkElement CreateView();

        #endregion
    }
}