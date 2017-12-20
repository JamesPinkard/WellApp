using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Linq;
using WellApp.Data;
using WellApp.Repo.Text;

namespace WellApp.UI
{
    /// <summary>
    /// Interaction logic for ProgressBarWindow.xaml
    /// </summary>
    public partial class ProgressBarWindow : Window
    {
        public ProgressBarWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            MessageBox.Show("Data has been uploaded");
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            /*for (int i = 0; i < 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(100);
            }*/
            LoadMainTable(@"C:\Users\jpinkard\Desktop\GWDBDownload\WellMain.txt", sender);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

        private static void LoadMainTable(string path, object sender)
        {
            TextLoader textLoader = new TextLoader(path);

            // Reads text into two report data containers
            AquiferTextReport aqReport = new AquiferTextReport();
            WellTextReport wellReport = new WellTextReport();
            textLoader.LoadText(aqReport, wellReport);

            using (var context = new GroundwaterContext())
            {
                // loads aquifer data into database
                // var currentAquifers = context.Aquifers.ToList();
                var aquifers = aqReport.GetUnique();                
                var aqCount = aquifers.Count();
                int actualProgress;
                for (int i = 0; i < aqCount; i++)
                {
                    var aq = aquifers[i];
                    //var retrievedAq = context.Aquifers.Find(aq.AquiferID);          
                    
                    if (context.Aquifers.Any(e => e.AquiferID == aq.AquiferID))
                    {
                        context.Entry(aq).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        context.Aquifers.Add(aq);
                    }
                    actualProgress = i * 100 / (aqCount * 2);
                    (sender as BackgroundWorker).ReportProgress(actualProgress);
                }

                // loads well information into database
                var currentWells = context.Wells.ToList();
                context.Wells.RemoveRange(currentWells);
                context.SaveChanges();
                var wells = wellReport.GetAll();
                var wellCount = wells.Count();
                for (int i = 0; i < wellCount; i++)
                {
                    var w = wells[i];
                    context.Wells.Add(w);
                    actualProgress = 50 + (i * 100 / (wellCount * 2));
                    (sender as BackgroundWorker).ReportProgress(actualProgress);
                }              
                context.SaveChanges();  
                (sender as BackgroundWorker).ReportProgress(100);
            }
        }
    }
}
