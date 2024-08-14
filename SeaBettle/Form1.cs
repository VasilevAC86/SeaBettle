namespace SeaBettle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            model = new Model();
            model.PlayerShips[0, 0] = CoordStatus.Ship;
            model.PlayerShips[5, 2] = CoordStatus.Ship;
            model.PlayerShips[5, 3] = CoordStatus.Ship;
        }
        Model model;
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(model.Shot(textBox1.Text).ToString());
        }
    }
}