using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkShopCBFM22
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;

            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var patientsFileName = fileDialog.FileName;

            var read = File.ReadAllLines(patientsFileName);

            var patients = read.Select(line => new Patient(line.Split(',')[0],
                line.Split(',')[1], line.Split(',')[2]));


            var prioritys = patients.Where(pat => pat.Priority != "Urgent").ToList();
            prioritys.ForEach(prio => prio.PrintPatient());
            Console.ReadKey();
            
        }

    }
    public class Patient
    {
        public string Name;
        public string Id;
        public string Priority;

        public Patient(string name, string id, string priority)
        {
            Name = name;
            Id = id;
            Priority = priority;
        }
        public Patient()
        {

        }

        public string PrintPatient()
        {
            Console.WriteLine("Patient : " + Name + " " + Id +  " " + Priority);
            return Name;
        }
    }
}
