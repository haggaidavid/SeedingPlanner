using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration; // <<--- Remove this!
using SeedingPlanner.Genetic; 
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace SeedingPlanner
{
    public partial class SeedingPlanner : Form
    {
        Population _pop = null;
        Series _bestSeries;
        Series _avgSeries;
        private Thread _workerThread = null;
        private volatile bool _bNeedToStop = true;

        public SeedingPlanner()
        {
            InitializeComponent();
            
            // load default values
            var appSettings = ConfigurationManager.AppSettings;
            population.Value = Convert.ToDecimal(appSettings["PopulationSize"]);
            generations.Value = Convert.ToDecimal(appSettings["NumberOfGenerations"]);
            mutationChance.Value = Convert.ToDecimal(appSettings["MutationChance"]);
            crossoverChance.Value = Convert.ToDecimal(appSettings["CrossoverChance"]);
            elitePercentage.Value = Convert.ToDecimal(appSettings["ElitePercentage"]);

            trayCost.Value = Convert.ToDecimal(appSettings["TrayCost"]);
            sampleCost.Value = Convert.ToDecimal(appSettings["SampleCost"]);

            DisableUI();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All Files|*.*";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inputExcelFilename.Text = dlg.FileName;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string filename = inputExcelFilename.Text;

            if (string.IsNullOrWhiteSpace(filename))
            {
                return;
            }

            if (!File.Exists(filename))
            {
                return;
            }

            if (BagsInventory.LoadFromExcel(filename))
            {
                // loaded successfully, update numbers and UI
                EnableUI();
                SeedingPlan plan = new SeedingPlan();

                int[] order = new int[BagsInventory.Count];
                for (int i = 0; i < BagsInventory.Count; ++i)
                {
                    order[i] = i;
                }
                plan.Setup(order);
                int cost = plan.Cost(Convert.ToInt32(trayCost.Value), Convert.ToInt32(sampleCost.Value));
                int trays = plan.TrayCount;
                int plates = plan.PlateCount;
                plan.SaveToExcel(filename + ".new.xlsx");


                Chromosome root = new Chromosome(BagsInventory.Count);
                _pop = new Population((int)population.Value, root, new FitnessFunction(), new SelectionMethod());

                chart.Series.Clear();
                _avgSeries = chart.Series.Add("avg");
                _bestSeries = chart.Series.Add("best");

                _avgSeries.ChartType = SeriesChartType.FastLine;
                _bestSeries.ChartType = SeriesChartType.FastLine;
            }
            else
            {
                // no bags inventory - disable UI
            }
        }

        private void DisableUI()
        {
            btnAllSteps.Enabled = false;
            btnSave.Enabled = false;
            btnStep.Enabled = false;
            btnStep2.Enabled = false;

            population.Enabled = false;
            generations.Enabled = false;
            mutationChance.Enabled = false;
            crossoverChance.Enabled = false;
            elitePercentage.Enabled = false;

            trayCost.Enabled = false;
            sampleCost.Enabled = false;

        }

        private void EnableUI()
        {
            btnAllSteps.Enabled = true;
            btnSave.Enabled = true;
            btnStep.Enabled = true;
            btnStep2.Enabled = true;

            population.Enabled = true;
            generations.Enabled = true;
            mutationChance.Enabled = true;
            crossoverChance.Enabled = true;
            elitePercentage.Enabled = true;

            trayCost.Enabled = true;
            sampleCost.Enabled = true;

        }

        delegate void AddToChartDelegate(double v1, double v2);

        private void AddToChart(double avgFitness, double bestFitness)
        {
            if (InvokeRequired)
            {
                Invoke(new AddToChartDelegate(AddToChart), avgFitness, bestFitness);
            }
            else
            {
                // update chart
                _avgSeries.Points.Add(avgFitness);
                _bestSeries.Points.Add(bestFitness);
            }
        }


        private void PlayOneEpoch()
        {
            _pop.RunEpoch();
            Chromosome best = (Chromosome)_pop.BestChromosome;
            double maxFitness = _pop.MaxFitness;
            double avgFitness = _pop.AvgFitness;
            double sumFitness = _pop.SumFitness;

            AddToChart(avgFitness, maxFitness);
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            //Config.Application.Save();
            PlayOneEpoch();
        }

        private void RunAllSteps()
        {
            for (int i = 0; i < generations.Value && !_bNeedToStop; ++i)
            {
                PlayOneEpoch();
            }
            UpdatePlayButtons();
        }

        private void UpdatePlayButtons()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdatePlayButtons));
            }

            if (_bNeedToStop)
            {
                btnAllSteps.Text = ">>";
            }
            else
            {
                btnAllSteps.Text = "| |";
            }
        }

        private void btnAllSteps_Click(object sender, EventArgs e)
        {
            if (!_bNeedToStop)
            {
                _bNeedToStop = true;
            }
            else
            {
                _bNeedToStop = false;
                _workerThread = new Thread(new ThreadStart(RunAllSteps));
                _workerThread.Start();
            }
            UpdatePlayButtons();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Chromosome best = (Chromosome)_pop.BestChromosome;
            SeedingPlan plan = new SeedingPlan();
            plan.Setup(best.Values);
            plan.SaveToExcel(inputExcelFilename.Text + ".new.xlsx");
        }

    }
}
