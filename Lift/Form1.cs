using System.ComponentModel;

namespace Lift
{
    public partial class Form1 : Form
    {
        BackgroundWorker bgWorkerUp = new BackgroundWorker();
        BackgroundWorker bgWorkerDown = new BackgroundWorker();
        int liftSpeed = 10;
        bool isMovingUp = true;
        bool isMovingDown = false;
        int liftTopY;

        // Lift going up
        private void BgWorkerUp_DoWork(object sender, DoWorkEventArgs e)
        {
            int liftY = btnLift.Location.Y;

            while (liftY > 10)
            {
                liftY -= liftSpeed; // Move lift up
                bgWorkerUp.ReportProgress(0, liftY); // Report liftY progress
                System.Threading.Thread.Sleep(50);
            }
            isMovingUp = false;
        }

        // Lift going down
        private void BgWorkeDown_DoWork(object sender, DoWorkEventArgs e)
        {
            int liftY = btnLift.Location.Y;

            while (liftY < this.ClientSize.Height - btnLift.Height - 10)
            {
                liftY += liftSpeed; // Move lift down (increment liftY)
                bgWorkerDown.ReportProgress(0, liftY); // Use bgWorkerDown
                System.Threading.Thread.Sleep(50);
            }
            isMovingDown = false;
        }

        // Update lift location when progress is reported
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)  // Ensure UserState is not null
            {
                int liftY = (int)e.UserState;  // Safe cast of UserState
                btnLift.Location = new System.Drawing.Point(btnLift.Location.X, liftY);  // Update lift position
            }
        }

        public Form1()
        {
            InitializeComponent();

            // Setup BackgroundWorker for moving up
            bgWorkerUp.DoWork += BgWorkerUp_DoWork;
            bgWorkerUp.WorkerReportsProgress = true;
            bgWorkerUp.ProgressChanged += BgWorker_ProgressChanged;

            // Setup BackgroundWorker for moving down
            bgWorkerDown.DoWork += BgWorkeDown_DoWork;
            bgWorkerDown.WorkerReportsProgress = true;
            bgWorkerDown.ProgressChanged += BgWorker_ProgressChanged;

            // Initialize lift's top Y boundary
            liftTopY = this.ClientSize.Height - btnLift.Height - 10;
        }

        // Button to move the lift up
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isMovingDown) // Ensure it's not already moving down
            {
                if (!bgWorkerUp.IsBusy) // If bgWorkerUp is not busy, start moving up
                {
                    isMovingUp = true;
                    bgWorkerUp.RunWorkerAsync();
                }
            }
        }

        // Button to move the lift down
        private void button2_Click(object sender, EventArgs e)
        {
            if (!isMovingUp) // Ensure it's not already moving up
            {
                if (!bgWorkerDown.IsBusy) // If bgWorkerDown is not busy, start moving down
                {
                    isMovingDown = true;
                    bgWorkerDown.RunWorkerAsync();
                }
            }
        }

        // Placeholder for unused button
        private void button3_Click(object sender, EventArgs e)
        {
        }

        // Placeholder for form load event
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
