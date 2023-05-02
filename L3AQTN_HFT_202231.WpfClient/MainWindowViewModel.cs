using L3AQTN_HFT_202231.WpfClient;
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

namespace L3AQTN_HFT_202231.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Bus> Buses { get; set; }

        private Bus selectedBus;

        public Bus SelectedBus
        {
            get { return selectedBus; }
            set
            {
                if (value != null)
                {
                    selectedBus = new Bus()
                    {
                        Model = value.Model,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteBusCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateBusCommand { get; set; }

        public ICommand DeleteBusCommand { get; set; }

        public ICommand UpdateBusCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
               // Buses = new RestCollection<Bus>("http://localhost:53910/", "bus");
                Buses = new RestCollection<Bus>("http://localhost:10615/", "bus","hub");

                
                CreateBusCommand = new RelayCommand(() =>
                {
                    Buses.Add(new Bus()
                    {
                        Model = SelectedBus.Model
                    });
                });

                UpdateBusCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Buses.Update(SelectedBus);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteBusCommand = new RelayCommand(() =>
                {
                    Buses.Delete(SelectedBus.Id);
                },
                () =>
                {
                    return SelectedBus != null;
                });
                SelectedBus = new Bus();
            }

        }
    }
}
