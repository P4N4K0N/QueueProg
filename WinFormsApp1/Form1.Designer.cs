namespace WinFormsApp1
{
    partial class Form1
    {
        private void InitializeComponent()
        {
            btnCamera = new Button();
            btnPusher = new Button();
            switchState = new CheckBox();
            SuspendLayout();
            // 
            // btnCamera
            // 
            btnCamera.Location = new Point(44, 47);
            btnCamera.Name = "btnCamera";
            btnCamera.Size = new Size(88, 47);
            btnCamera.TabIndex = 0;
            btnCamera.Text = "Камера";
            btnCamera.Click += BtnCamera_Click;
            // 
            // btnPusher
            // 
            btnPusher.Location = new Point(44, 112);
            btnPusher.Name = "btnPusher";
            btnPusher.Size = new Size(88, 47);
            btnPusher.TabIndex = 1;
            btnPusher.Text = "Толкатель";
            btnPusher.Click += BtnPusher_Click;
            // 
            // switchState
            // 
            switchState.Location = new Point(44, 188);
            switchState.Name = "switchState";
            switchState.Size = new Size(88, 22);
            switchState.TabIndex = 2;
            switchState.Text = "Брак";
            switchState.CheckedChanged += switchState_TextChange;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(262, 281);
            Controls.Add(btnCamera);
            Controls.Add(btnPusher);
            Controls.Add(switchState);
            Name = "Form1";
            Text = "Control App";
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.Button btnPusher;
        private System.Windows.Forms.CheckBox switchState;
    }
}
