using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS;
using VMS.TPS.Common.Model.API;

namespace June22WorkShopPCheck
{
    class Executable
    {
        [STAThread]

        public static void Main()
        {
            try
            {
                using (Application app = Application.CreateApplication())
                {
                    var patient = app.OpenPatientById("script_test");
                    var plan = patient.Courses.First().PlanSetups.First(e => e.RTPrescription != null);
                    Script.Perform(plan);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
