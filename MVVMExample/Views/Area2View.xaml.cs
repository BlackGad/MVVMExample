using MVVMExample.Models;
using MVVMExample.ViewModels;

namespace MVVMExample.Views
{
    public partial class Area2View
    {
        #region Constructors

        public Area2View(BusinessLogicService businessLogicService)
        {
            InitializeComponent();
            CalculatedValue = businessLogicService.CalculateValue();
        }

        #endregion

        #region Properties

        public int CalculatedValue { get; }

        public Area2ViewModel ViewModel
        {
            get { return DataContext as Area2ViewModel; }
        }

        #endregion
    }
}