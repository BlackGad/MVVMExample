using System.Windows;
using MVVMExample.Models;

namespace MVVMExample.ViewModels
{
    public class Area1ViewModel : DependencyObject
    {
        #region Constructors

        public Area1ViewModel(BusinessLogicService businessLogicService)
        {
            CalculatedValue = businessLogicService.CalculateValue();
        }

        #endregion

        #region Properties

        public int CalculatedValue { get; }

        #endregion
    }
}