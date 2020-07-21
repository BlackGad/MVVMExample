using MVVMExample.ViewModels;

namespace MVVMExample.Views
{
    public partial class Area1View
    {
        #region Constructors

        public Area1View()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public Area1ViewModel ViewModel
        {
            get { return DataContext as Area1ViewModel; }
        }

        #endregion
    }
}