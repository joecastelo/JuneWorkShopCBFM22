using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace June22WorkShopPCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string Id { get; set; }

        public string PlanSetupId { get; set; }

        public string PhotonModel { get; set; }
        public IEnumerable<string> CalculationOptions { get; set; }

        public string OptimizationModel { get; set; }



        public List<UserControl> ListChecks { get; set; }

        public MainWindow(PlanSetup plan)
        {
            DataContext = this;
            Id = plan.Course.Patient.Name;
            PlanSetupId = plan.Id;
            CalculationOptions =
                plan.PhotonCalculationOptions.Select(e => e.Key + " : " + e.Value);
            PhotonModel = plan.PhotonCalculationModel;
            OptimizationModel = plan.GetCalculationModel(CalculationType.PhotonVMATOptimization);
            ListChecks = new List<UserControl>();
            InitializeComponent();

        }

        public void AddCheck(UserControl checkScreen)
        {

            ListChecks.Add(checkScreen);
            CheckList.ItemsSource = new List<UserControl>();
            CheckList.ItemsSource = ListChecks;
        }
    }
}
