namespace WinFormsApp2
{
    partial class Form1
    {
        private void InitializeComponent()
        {
            queuePanel = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // queuePanel
            // 
            queuePanel.BorderStyle = BorderStyle.FixedSingle;
            queuePanel.Location = new Point(12, 21);
            queuePanel.Name = "queuePanel";
            queuePanel.Size = new Size(400, 123);
            queuePanel.TabIndex = 0;
            queuePanel.FlowDirection = FlowDirection.LeftToRight;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(429, 319);
            Controls.Add(queuePanel);
            Name = "Form1";
            Text = "Очередь продуктов";
            ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel queuePanel;
    }
}
