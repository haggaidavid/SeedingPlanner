namespace SeedingPlanner
{
    partial class SeedingPlanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.inputExcelFilename = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.elitePercentage = new System.Windows.Forms.NumericUpDown();
            this.crossoverChance = new System.Windows.Forms.NumericUpDown();
            this.mutationChance = new System.Windows.Forms.NumericUpDown();
            this.generations = new System.Windows.Forms.NumericUpDown();
            this.population = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textAvgPopCost = new System.Windows.Forms.TextBox();
            this.textCostOfOriginal = new System.Windows.Forms.TextBox();
            this.textCostOfBestFit = new System.Windows.Forms.TextBox();
            this.sampleCost = new System.Windows.Forms.NumericUpDown();
            this.trayCost = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStep2 = new System.Windows.Forms.Button();
            this.btnAllSteps = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elitePercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crossoverChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutationChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.population)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trayCost)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Existing excel file";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(349, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(35, 20);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // inputExcelFilename
            // 
            this.inputExcelFilename.Location = new System.Drawing.Point(99, 12);
            this.inputExcelFilename.Name = "inputExcelFilename";
            this.inputExcelFilename.Size = new System.Drawing.Size(245, 20);
            this.inputExcelFilename.TabIndex = 2;
            this.inputExcelFilename.Text = "D:\\Users\\Haggai\\Documents\\GitHub\\HaZera\\HaZera\\bin\\Debug\\input.xlsx";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.elitePercentage);
            this.groupBox1.Controls.Add(this.crossoverChance);
            this.groupBox1.Controls.Add(this.mutationChance);
            this.groupBox1.Controls.Add(this.generations);
            this.groupBox1.Controls.Add(this.population);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 154);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Genetic Algorithm Attributes ]";
            // 
            // elitePercentage
            // 
            this.elitePercentage.Location = new System.Drawing.Point(115, 124);
            this.elitePercentage.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.elitePercentage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.elitePercentage.Name = "elitePercentage";
            this.elitePercentage.Size = new System.Drawing.Size(72, 20);
            this.elitePercentage.TabIndex = 10;
            this.elitePercentage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // crossoverChance
            // 
            this.crossoverChance.DecimalPlaces = 2;
            this.crossoverChance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.crossoverChance.Location = new System.Drawing.Point(115, 98);
            this.crossoverChance.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            this.crossoverChance.Name = "crossoverChance";
            this.crossoverChance.Size = new System.Drawing.Size(72, 20);
            this.crossoverChance.TabIndex = 9;
            // 
            // mutationChance
            // 
            this.mutationChance.DecimalPlaces = 3;
            this.mutationChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.mutationChance.Location = new System.Drawing.Point(115, 72);
            this.mutationChance.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.mutationChance.Name = "mutationChance";
            this.mutationChance.Size = new System.Drawing.Size(72, 20);
            this.mutationChance.TabIndex = 8;
            this.mutationChance.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // generations
            // 
            this.generations.Location = new System.Drawing.Point(115, 46);
            this.generations.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.generations.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.generations.Name = "generations";
            this.generations.Size = new System.Drawing.Size(72, 20);
            this.generations.TabIndex = 7;
            this.generations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // population
            // 
            this.population.Location = new System.Drawing.Point(115, 20);
            this.population.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.population.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.population.Name = "population";
            this.population.Size = new System.Drawing.Size(72, 20);
            this.population.TabIndex = 6;
            this.population.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "elite selection (%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "crossover chance";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "mutation chance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "generations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "population";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textAvgPopCost);
            this.groupBox2.Controls.Add(this.textCostOfOriginal);
            this.groupBox2.Controls.Add(this.textCostOfBestFit);
            this.groupBox2.Controls.Add(this.sampleCost);
            this.groupBox2.Controls.Add(this.trayCost);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(224, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 154);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[ Seeding Planner Attributes ]";
            // 
            // textAvgPopCost
            // 
            this.textAvgPopCost.Location = new System.Drawing.Point(125, 124);
            this.textAvgPopCost.Name = "textAvgPopCost";
            this.textAvgPopCost.ReadOnly = true;
            this.textAvgPopCost.Size = new System.Drawing.Size(65, 20);
            this.textAvgPopCost.TabIndex = 14;
            // 
            // textCostOfOriginal
            // 
            this.textCostOfOriginal.Location = new System.Drawing.Point(125, 72);
            this.textCostOfOriginal.Name = "textCostOfOriginal";
            this.textCostOfOriginal.ReadOnly = true;
            this.textCostOfOriginal.Size = new System.Drawing.Size(65, 20);
            this.textCostOfOriginal.TabIndex = 13;
            // 
            // textCostOfBestFit
            // 
            this.textCostOfBestFit.Location = new System.Drawing.Point(125, 98);
            this.textCostOfBestFit.Name = "textCostOfBestFit";
            this.textCostOfBestFit.ReadOnly = true;
            this.textCostOfBestFit.Size = new System.Drawing.Size(65, 20);
            this.textCostOfBestFit.TabIndex = 12;
            // 
            // sampleCost
            // 
            this.sampleCost.Location = new System.Drawing.Point(125, 46);
            this.sampleCost.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.sampleCost.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sampleCost.Name = "sampleCost";
            this.sampleCost.Size = new System.Drawing.Size(65, 20);
            this.sampleCost.TabIndex = 11;
            this.sampleCost.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // trayCost
            // 
            this.trayCost.Location = new System.Drawing.Point(125, 20);
            this.trayCost.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.trayCost.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.trayCost.Name = "trayCost";
            this.trayCost.Size = new System.Drawing.Size(65, 20);
            this.trayCost.TabIndex = 10;
            this.trayCost.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "sample cost";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "cost of original";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "cost of best fit";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "avg. cost in population";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "tray cost";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 198);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(416, 20);
            this.progressBar.TabIndex = 5;
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(11, 223);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 23);
            this.btnStep.TabIndex = 6;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            // 
            // btnStep2
            // 
            this.btnStep2.Location = new System.Drawing.Point(93, 223);
            this.btnStep2.Name = "btnStep2";
            this.btnStep2.Size = new System.Drawing.Size(75, 23);
            this.btnStep2.TabIndex = 7;
            this.btnStep2.Text = "5 Steps";
            this.btnStep2.UseVisualStyleBackColor = true;
            // 
            // btnAllSteps
            // 
            this.btnAllSteps.Location = new System.Drawing.Point(174, 223);
            this.btnAllSteps.Name = "btnAllSteps";
            this.btnAllSteps.Size = new System.Drawing.Size(75, 23);
            this.btnAllSteps.TabIndex = 8;
            this.btnAllSteps.Text = "All the way";
            this.btnAllSteps.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(354, 223);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save best fit to file";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(385, 11);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(46, 20);
            this.btnLoad.TabIndex = 10;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // SeedingPlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 540);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAllSteps);
            this.Controls.Add(this.btnStep2);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.inputExcelFilename);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Name = "SeedingPlanner";
            this.Text = "Seeding Planner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elitePercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crossoverChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutationChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.population)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trayCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox inputExcelFilename;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown elitePercentage;
        private System.Windows.Forms.NumericUpDown crossoverChance;
        private System.Windows.Forms.NumericUpDown mutationChance;
        private System.Windows.Forms.NumericUpDown generations;
        private System.Windows.Forms.NumericUpDown population;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textAvgPopCost;
        private System.Windows.Forms.TextBox textCostOfOriginal;
        private System.Windows.Forms.TextBox textCostOfBestFit;
        private System.Windows.Forms.NumericUpDown sampleCost;
        private System.Windows.Forms.NumericUpDown trayCost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnStep2;
        private System.Windows.Forms.Button btnAllSteps;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
    }
}

