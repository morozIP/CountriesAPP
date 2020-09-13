using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CountriesAPP
{
 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            //dataDB.ItemsSource = new ActionsDB().GetCountries();
        }
             
        //private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    ActionsDB actionsDB = new ActionsDB();
            
        //    if (MessageBox.Show("сохранить информацию в базу (MS SQL)?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //    {
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var n = dataG.Items[dataG.SelectedIndex];
        //            Binding binding = new Binding("n");
        //            binding.Source = this;
                    

        //            //actionsDB.AddCountry(n);
        //            dataDB.ItemsSource = new ActionsDB().GetCountries();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);                  
        //        }
                
        //    }


        //}

        
    }
}
