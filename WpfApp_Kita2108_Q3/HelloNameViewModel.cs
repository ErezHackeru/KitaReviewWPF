using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp_Kita2108_Q3
{
    public class HelloNameViewModel : IDataErrorInfo
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {                
                name = value;
                MyDelegateGo.RaiseCanExecuteChanged();
                MyDelegateCancel.RaiseCanExecuteChanged();
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return GetErrorForProperty(propertyName);
            }
        }

        private string GetErrorForProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "Name":
                    if (Name == null)
                    {
                        return string.Empty;
                    }
                    else if (Name.Length > 10)
                    {
                        return "Over 10 characters";
                    }
                    else
                        return string.Empty;
                default:
                    return string.Empty;
            }
        }
        public string Error => throw new NotImplementedException();

        public DelegateCommand MyDelegateGo { get; set; }
        public DelegateCommand MyDelegateCancel { get; set; }

        public HelloNameViewModel()
        {
            MyDelegateGo = new DelegateCommand(ExecuteGoCommand, CanExecuteGoMethod);
            MyDelegateCancel = new DelegateCommand(ExecuteCancelCommand, CanExecuteCancelMethod);
        }

        // =========================== delegate Go
        private bool CanExecuteGoMethod()
        {
            if (Name != null)
            {
                return (Name.Length >= 3);
            }
            return false;
        }

        private void ExecuteGoCommand()
        {
            MessageBox.Show($"Hello {Name}");
        }

        // =========================== delegate Cancel
        private bool CanExecuteCancelMethod()
        {
            if (Name != null)
            {
                return (Name.Length >= 1);
            }
            return false;
        }

        private void ExecuteCancelCommand()
        {
            Name = string.Empty;
        }
    }
}
