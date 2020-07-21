using System;
using System.Windows;
using System.Windows.Controls;

namespace MVVMExample.Components
{
    public class RelayTemplateSelector : DataTemplateSelector
    {
        private readonly Func<object, DependencyObject, DataTemplate> _selectTemplateFunc;

        #region Constructors

        public RelayTemplateSelector(Func<object, DependencyObject, DataTemplate> selectTemplateFunc)
        {
            _selectTemplateFunc = selectTemplateFunc ?? throw new ArgumentNullException(nameof(selectTemplateFunc));
        }

        #endregion

        #region Override members

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return _selectTemplateFunc(item, container);
        }

        #endregion
    }
}