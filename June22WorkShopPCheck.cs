using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using June22WorkShopPCheck;

[assembly: AssemblyVersion("1.0.0.1")]

namespace VMS.TPS
{
  public class Script
  {
    public Script()
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Execute(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
    {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            var planSetup = context.PlanSetup;
            Perform(planSetup);

    }

    public static void Perform(PlanSetup planSetup)
        {

            // Exceptions
            #region
            if (!planSetup.IsDoseValid)
            {
                MessageBox.Show("Plano has no valid dose");
                return;
            }
            if (planSetup.RTPrescription == null)
            {
                MessageBox.Show("RT Prescription not valid");
                return;
            }
            #endregion

            // Origin Check Test 🚀
            #region
            var originwasChangedTitle = "User Origin Check";
            var originwasChangedResult = "OK";
            var originwasChangedComment = "";
            var image = planSetup.StructureSet.Image;
            if (!image.HasUserOrigin)
            {
                originwasChangedResult = "X";
                originwasChangedComment += "Dicom origin = User Origin";

            }
            //var fullMessageOriginCheck = originwasChangedTitle + "\t Resultado: " + originwasChangedResult
            //    + "\n" + originwasChangedComment;
            //MessageBox.Show(fullMessageOriginCheck, "First Check");
            #endregion

            // Instance the first Check View 

            var checkScreenOrigin = new CheckScreen(originwasChangedTitle, originwasChangedResult, originwasChangedComment);

            // Setup Field Check Test 🚀

            #region
            var hasSetupFieldTitle = "Setup Field Check";
            var hasSetupFieldResult = "OK";
            var hasSetupFieldComment = "";
            // Let's use some LINQ here! 
            var setupBeams = planSetup.Beams.Where(beam => beam.IsSetupField);
            var anySetupBeams = setupBeams.Any();
            if (!anySetupBeams)
            {
                hasSetupFieldResult = "X";
                hasSetupFieldComment += " There are no Setup Fields!";
            }
            else
            {
                foreach (var setupBeam in setupBeams)
                {
                    hasSetupFieldComment += "Setup Beam Id : " + setupBeam.Id + "\n";

                }
            }

    //        var fullMessageSetupCheck = hasSetupFieldTitle + "\t Resultado: " + hasSetupFieldResult
    //+ "\n" + hasSetupFieldComment;
            //MessageBox.Show(fullMessageSetupCheck, "Second Check");
            #endregion

            // Instance the second Check View 


            var checkScreenSetup = new CheckScreen(hasSetupFieldTitle, hasSetupFieldResult, hasSetupFieldComment);


            // MU Factor Check Test 🚀
            #region
            var MUFactorTestTitle = "MU Check";
            var MUFactorTestResult = "OK";
            var MUFactorTestComment = "";
            // Math ....
            var validBeams = planSetup.Beams.Where(beam => !beam.IsSetupField);
            var totalMU = validBeams.Select(beam => beam.Meterset.Value).Sum();
            var dosePerFraction = planSetup.DosePerFraction.Dose;
            var factor = totalMU / dosePerFraction;

            if (factor > 5)
            {
                MUFactorTestResult = "X";

            }
            MUFactorTestComment += "IMRT Factor : " + factor.ToString("F1");
            MUFactorTestComment += "\n MU Total : " + totalMU.ToString("F1");

            //var fullMessageMUCheck = MUFactorTestTitle + "\t Resultado: " + MUFactorTestResult
            //    + "\n" + MUFactorTestComment;
            //MessageBox.Show(fullMessageMUCheck, "Third Check");

            #endregion

            // Instance the third CheckView 

            var checkScreenMU = new CheckScreen(MUFactorTestTitle, MUFactorTestResult, MUFactorTestComment);
            // Primary Reference Point Test 🚀

            #region
            var primaryRefTestTitle = "Primary Ref Point Check";
            var primaryRefTestResult = "OK";
            var primaryRefTestComment = "";

            var point = planSetup.PrimaryReferencePoint;
            var prescription = planSetup.RTPrescription;
            var maxDosePrescribed = prescription.
                Targets.Max(target => target.DosePerFraction.Dose * target.NumberOfFractions);
            // we already have dosePerFraction 
            var planDose = planSetup.TotalDose.Dose;

            if (planDose != maxDosePrescribed)
            {
                primaryRefTestResult = "X";
                primaryRefTestComment += "Target: " +
                    planSetup.TargetVolumeID + "was not planned for the largest prescription :"
                    + maxDosePrescribed + "cGy \n";

            }
            // Math ....
            var getdose = point.TotalDoseLimit.Dose;
            var perday = Math.Round(Math.Ceiling(10 * getdose / planSetup.NumberOfFractions.Value) / 10, 1);
            var daily = point.DailyDoseLimit.Dose >= perday;
            var total = perday * planSetup.NumberOfFractions;
            var totalcheck = point.TotalDoseLimit.Dose >= total;
            var session = point.SessionDoseLimit.Dose == point.DailyDoseLimit.Dose;
            primaryRefTestComment += "Ref Point : " + point.Id + "\n";
            primaryRefTestComment += "Daily Dose " + point.DailyDoseLimit.Dose.ToString() + "cGy Session " + point.SessionDoseLimit.Dose.ToString() + "cGy Total " + point.TotalDoseLimit.Dose.ToString() + " cGy \n";
            primaryRefTestComment += "Daily and session required : " + perday.ToString() + " cGy Total required : " + total.Value.ToString("F1") + " cGy \n";
            if (!(totalcheck && daily && session))
            {
                primaryRefTestResult = "X";
            }
    //        var fullMessageprimaryRef = primaryRefTestTitle + "\t Resultado: " + primaryRefTestResult
    //+ "\n" + primaryRefTestComment;
            //MessageBox.Show(fullMessageprimaryRef, "Forth Check");

            #endregion

            // Instance the forth Check View 

            var checkScreenPrimary = new CheckScreen(primaryRefTestTitle, primaryRefTestResult, primaryRefTestComment);


            // D95 Checks 🚀

            // Window 

            var window = new MainWindow(planSetup);

            window.AddCheck(checkScreenOrigin);
            window.AddCheck(checkScreenSetup);
            window.AddCheck(checkScreenPrimary);
            window.AddCheck(checkScreenMU);

            window.ShowDialog();
        }
    }
}
