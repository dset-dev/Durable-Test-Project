
namespace EntitlementFormApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ParentTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.EntitlementIdFieldDescriptor = new System.Windows.Forms.Label();
            this.EntitlementIdField = new System.Windows.Forms.TextBox();
            this.EntitlementIdSubmitButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.ButtonResponse1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.ButtonResponse2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.ButtonResponse3 = new System.Windows.Forms.Label();
            this.LoggingOutput = new System.Windows.Forms.RichTextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.ParentTableLayout.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // ParentTableLayout
            // 
            this.ParentTableLayout.AutoSize = true;
            this.ParentTableLayout.ColumnCount = 2;
            this.ParentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 406F));
            this.ParentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ParentTableLayout.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.ParentTableLayout.Controls.Add(this.LoggingOutput, 1, 0);
            this.ParentTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParentTableLayout.Location = new System.Drawing.Point(0, 0);
            this.ParentTableLayout.Name = "ParentTableLayout";
            this.ParentTableLayout.RowCount = 2;
            this.ParentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.96296F));
            this.ParentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.03704F));
            this.ParentTableLayout.Size = new System.Drawing.Size(1088, 450);
            this.ParentTableLayout.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 277);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.EntitlementIdFieldDescriptor);
            this.flowLayoutPanel2.Controls.Add(this.EntitlementIdField);
            this.flowLayoutPanel2.Controls.Add(this.EntitlementIdSubmitButton);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(391, 42);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // EntitlementIdFieldDescriptor
            // 
            this.EntitlementIdFieldDescriptor.AutoSize = true;
            this.EntitlementIdFieldDescriptor.Location = new System.Drawing.Point(3, 0);
            this.EntitlementIdFieldDescriptor.Name = "EntitlementIdFieldDescriptor";
            this.EntitlementIdFieldDescriptor.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.EntitlementIdFieldDescriptor.Size = new System.Drawing.Size(104, 26);
            this.EntitlementIdFieldDescriptor.TabIndex = 3;
            this.EntitlementIdFieldDescriptor.Text = "Entitlement ID";
            // 
            // EntitlementIdField
            // 
            this.EntitlementIdField.Location = new System.Drawing.Point(113, 3);
            this.EntitlementIdField.Name = "EntitlementIdField";
            this.EntitlementIdField.Size = new System.Drawing.Size(166, 27);
            this.EntitlementIdField.TabIndex = 4;
            // 
            // EntitlementIdSubmitButton
            // 
            this.EntitlementIdSubmitButton.Location = new System.Drawing.Point(285, 3);
            this.EntitlementIdSubmitButton.Name = "EntitlementIdSubmitButton";
            this.EntitlementIdSubmitButton.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.EntitlementIdSubmitButton.Size = new System.Drawing.Size(94, 29);
            this.EntitlementIdSubmitButton.TabIndex = 2;
            this.EntitlementIdSubmitButton.Text = "Submit";
            this.EntitlementIdSubmitButton.UseVisualStyleBackColor = true;
            this.EntitlementIdSubmitButton.Click += new System.EventHandler(this.EntitlementIdSubmitButton_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.button1);
            this.flowLayoutPanel3.Controls.Add(this.ButtonResponse1);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 51);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(379, 39);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ButtonResponse1
            // 
            this.ButtonResponse1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResponse1.AutoSize = true;
            this.ButtonResponse1.Location = new System.Drawing.Point(103, 0);
            this.ButtonResponse1.Name = "ButtonResponse1";
            this.ButtonResponse1.Padding = new System.Windows.Forms.Padding(110, 6, 0, 0);
            this.ButtonResponse1.Size = new System.Drawing.Size(265, 26);
            this.ButtonResponse1.TabIndex = 4;
            this.ButtonResponse1.Text = "Response for button 1";
            this.ButtonResponse1.Click += new System.EventHandler(this.ButtonResponse1_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.button2);
            this.flowLayoutPanel4.Controls.Add(this.ButtonResponse2);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 96);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(379, 39);
            this.flowLayoutPanel4.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ButtonResponse2
            // 
            this.ButtonResponse2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResponse2.AutoSize = true;
            this.ButtonResponse2.Location = new System.Drawing.Point(103, 0);
            this.ButtonResponse2.Name = "ButtonResponse2";
            this.ButtonResponse2.Padding = new System.Windows.Forms.Padding(110, 6, 0, 0);
            this.ButtonResponse2.Size = new System.Drawing.Size(265, 26);
            this.ButtonResponse2.TabIndex = 5;
            this.ButtonResponse2.Text = "Response for button 2";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.button3);
            this.flowLayoutPanel5.Controls.Add(this.ButtonResponse3);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 141);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(379, 39);
            this.flowLayoutPanel5.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 29);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ButtonResponse3
            // 
            this.ButtonResponse3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResponse3.AutoSize = true;
            this.ButtonResponse3.Location = new System.Drawing.Point(103, 0);
            this.ButtonResponse3.Name = "ButtonResponse3";
            this.ButtonResponse3.Padding = new System.Windows.Forms.Padding(110, 6, 0, 0);
            this.ButtonResponse3.Size = new System.Drawing.Size(265, 26);
            this.ButtonResponse3.TabIndex = 7;
            this.ButtonResponse3.Text = "Response for button 3";
            // 
            // LoggingOutput
            // 
            this.LoggingOutput.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.LoggingOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoggingOutput.Font = new System.Drawing.Font("Courier New", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoggingOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.LoggingOutput.Location = new System.Drawing.Point(409, 3);
            this.LoggingOutput.Name = "LoggingOutput";
            this.LoggingOutput.ReadOnly = true;
            this.LoggingOutput.Size = new System.Drawing.Size(676, 277);
            this.LoggingOutput.TabIndex = 3;
            this.LoggingOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 450);
            this.Controls.Add(this.ParentTableLayout);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ParentTableLayout.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ParentTableLayout;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button EntitlementIdSubmitButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label EntitlementIdFieldDescriptor;
        private System.Windows.Forms.TextBox EntitlementIdField;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label ButtonResponse1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label ButtonResponse2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label ButtonResponse3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.RichTextBox LoggingOutput;
    }
}

