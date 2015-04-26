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
using System.Configuration;


namespace SeedingPlanner
{
    public partial class SeedingPlanner : Form
    {

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

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}
