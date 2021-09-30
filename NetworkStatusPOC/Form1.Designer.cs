
namespace NetworkStatusPOC
{
    partial class NetworkStatusForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkNetworkButton = new System.Windows.Forms.Button();
            this.NetworkStatusDisplayLabel = new System.Windows.Forms.Label();
            this.VerboseNetworkStatusLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.92308F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.NetworkStatusDisplayLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.VerboseNetworkStatusLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(994, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.checkNetworkButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(232, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(759, 219);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // checkNetworkButton
            // 
            this.checkNetworkButton.Location = new System.Drawing.Point(3, 3);
            this.checkNetworkButton.Name = "checkNetworkButton";
            this.checkNetworkButton.Size = new System.Drawing.Size(233, 29);
            this.checkNetworkButton.TabIndex = 0;
            this.checkNetworkButton.Text = "Check Network Connection";
            this.checkNetworkButton.UseVisualStyleBackColor = true;
            this.checkNetworkButton.Click += new System.EventHandler(this.checkNetworkButton_Click);
            // 
            // NetworkStatusDisplayLabel
            // 
            this.NetworkStatusDisplayLabel.AutoSize = true;
            this.NetworkStatusDisplayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NetworkStatusDisplayLabel.Location = new System.Drawing.Point(3, 225);
            this.NetworkStatusDisplayLabel.Name = "NetworkStatusDisplayLabel";
            this.NetworkStatusDisplayLabel.Size = new System.Drawing.Size(223, 225);
            this.NetworkStatusDisplayLabel.TabIndex = 1;
            this.NetworkStatusDisplayLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // VerboseNetworkStatusLabel
            // 
            this.VerboseNetworkStatusLabel.AutoSize = true;
            this.VerboseNetworkStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VerboseNetworkStatusLabel.Location = new System.Drawing.Point(3, 0);
            this.VerboseNetworkStatusLabel.Name = "VerboseNetworkStatusLabel";
            this.VerboseNetworkStatusLabel.Size = new System.Drawing.Size(223, 225);
            this.VerboseNetworkStatusLabel.TabIndex = 2;
            this.VerboseNetworkStatusLabel.Text = "Verbose output here";
            // 
            // NetworkStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NetworkStatusForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.NetworkStatusForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button checkNetworkButton;
        private System.Windows.Forms.Label NetworkStatusDisplayLabel;
        private System.Windows.Forms.Label VerboseNetworkStatusLabel;
    }
}

