using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using L3AQTN_HFT_202231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace L3AQTN_HFT_202231.WpfClient.ViewModels
{
    public class OwnerWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Owner> Owners { get; set; }

        private Owner selectedOwner;

        public Owner SelectedOwner
        {
            get { return selectedOwner; }
            set
            {
                if (value != null)
                {
                    selectedOwner = new Owner()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteOwnerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateOwnerCommand { get; set; }

        public ICommand DeleteOwnerCommand { get; set; }

        public ICommand UpdateOwnerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public OwnerWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Owners = new RestCollection<Owner>("http://localhost:10615/", "owner", "hub");

                CreateOwnerCommand = new RelayCommand(() =>
                {
                    Owners.Add(new Owner()
                    {
                        Name = SelectedOwner.Name
                    });
                });

                UpdateOwnerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Owners.Update(SelectedOwner);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteOwnerCommand = new RelayCommand(() =>
                {
                    Owners.Delete(SelectedOwner.Id);
                },
                () =>
                {
                    return SelectedOwner != null;
                });
                SelectedOwner = new Owner();
            }

        }
    }
}
