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
using L3AQTN_HFT_202231.WpfClient.Windows;

namespace L3AQTN_HFT_202231.WpfClient.ViewModels
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
                if (value!=null)
                {
                    selectedBus = new Bus() {
                    BrandId=value.BrandId,
                    Model=value.Model,
                    OwnerId=value.OwnerId,
                    Price=value.Price,
                    Id=value.Id,

                    };

                }
                OnPropertyChanged();
                
                (DeleteBusCommand as RelayCommand).NotifyCanExecuteChanged();
              

            }
        }


        public ICommand CreateBusCommand { get; set; }

        public ICommand DeleteBusCommand { get; set; }

        public ICommand UpdateBusCommand { get; set; }

        public ICommand OpenOwnerWindowCommand { get; set; }

        public ICommand OpenBrandWindowCommand { get; set; }

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
                Buses = new RestCollection<Bus>("http://localhost:10615/", "bus", "hub");

                OpenOwnerWindowCommand = new RelayCommand(() =>
                {
                    var ownerWindow = new OwnerWindow();
                    ownerWindow.ShowDialog();
                });

                OpenBrandWindowCommand = new RelayCommand(() =>
                {
                    var brandWindow = new BrandWindow();
                    brandWindow.ShowDialog();
                });

                CreateBusCommand = new RelayCommand(() =>
                {
                    Bus newBus = new Bus(SelectedBus);
                    newBus.BrandId = SelectedBus.BrandId;
                    newBus.Model = SelectedBus.Model;
                    newBus.Price = SelectedBus.Price;
                    newBus.OwnerId = SelectedBus.OwnerId;
                    Buses.Add(newBus);
                   
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
