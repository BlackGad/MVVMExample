using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MVVMExample.Components
{
    public class CustomFrameworkElementFactory : FrameworkElementFactory
    {
        private readonly ICustomDataTemplate _customDataTemplate;

        #region Constructors

        public CustomFrameworkElementFactory(ICustomDataTemplate customDataTemplate)
            : base(typeof(Control))
        {
            _customDataTemplate = customDataTemplate ?? throw new ArgumentNullException(nameof(customDataTemplate));

            OverrideDefaultFactory();
        }

        #endregion

        #region Members

        internal void OverrideDefaultFactory()
        {
            var field = typeof(FrameworkElementFactory).GetField("_knownTypeFactory", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new NotSupportedException();
            field.SetValue(this, new Func<object>(CreateViewInstance));
        }

        private object CreateViewInstance()
        {
            var container = new ContentPresenter
            {
                Content = _customDataTemplate.CreateView()
            };

            return container;
        }

        #endregion
    }
}