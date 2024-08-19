namespace SeaBettle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            model = new Model();
            model.PlayerShips[0, 0] = CoordStatus.Ship;
            model.PlayerShips[5, 1] = CoordStatus.Ship;
            model.PlayerShips[5, 2] = CoordStatus.Ship;
            model.PlayerShips[5, 3] = CoordStatus.Ship;
            model.PlayerShips[5, 4] = CoordStatus.Ship;
            model.PlayerShips[1, 6] = CoordStatus.Ship;
            model.PlayerShips[2, 6] = CoordStatus.Ship;
            model.PlayerShips[3, 6] = CoordStatus.Ship;
        }
        Model model;
        private void button1_Click(object sender, EventArgs e)
        {

            model.LastShot = model.Shot(textBox1.Text);
            int x = int.Parse(textBox1.Text.Substring(0, 1));
            int y = int.Parse(textBox1.Text.Substring(1, 1));
            switch (model.LastShot)
            {
                case ShotStatus.Miss:
                    model.EnemyShips[x, y] = CoordStatus.Shot;
                    break;
                case ShotStatus.Wounded:
                    model.EnemyShips[x, y] = CoordStatus.Got;
                    break;
                case ShotStatus.Kill:
                    model.EnemyShips[x, y] = CoordStatus.Got;
                    break;
            }
            //model.LastShotCoord = textBox1.Text;
            if (model.LastShot == ShotStatus.Wounded) // Перезаписываем координату последнего выстрела, если мы попали
            {
                model.LastShotCoord = textBox1.Text;
                model.WoundedStatus = true;
            }
            MessageBox.Show(model.Shot(textBox1.Text).ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s;
            int x, y;
            do
            {
                s = model.ShotGen();
                x = int.Parse(s.Substring(0, 1));
                y = int.Parse(s.Substring(1, 1));
            }
            while (model.EnemyShips[x, y] != CoordStatus.None);
            textBox1.Text = s;
        }

        private void button104_Click(object sender, EventArgs e) // Кнопка перерисовать всё
        {
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    //var btn = this.Controls["b" + x.ToString() + y.ToString()];
                    string name = "b" + x.ToString() + y.ToString();
                    var b = this.Controls.Find(name, true); // Массив кнопок с именем name
                    if (b.Count() > 0)
                    {
                        var btn = b[0];
                        switch (model.PlayerShips[x, y])
                        {
                            case CoordStatus.Ship:
                                btn.Text = "x";
                                break;
                            case CoordStatus.None:
                                btn.Text = "";
                                break;
                        }
                    }
                }
        }

        private void button103_Click(object sender, EventArgs e) // Кнопка "Поставить" 
        {
            Direction direction; // Направление размещения корабля
            TypeShips typeShip = TypeShips.x1; // Тип корабля
            if (checkBox1.Checked)            
                direction = Direction.Vertical;
            else
                direction = Direction.Horizontal;
            if (checkBox2.Checked)
            {
                model.AddDelShip(textBox1.Text, typeShip, direction, true);
                button104_Click(sender, e);
                return;
            }
            if (radioButton1.Checked)
                typeShip = TypeShips.x1;
            if (radioButton2.Checked)
                typeShip = TypeShips.x2;
            if (radioButton3.Checked)
                typeShip = TypeShips.x3;
            if (radioButton4.Checked)
                typeShip = TypeShips.x4;            
            model.AddDelShip(textBox1.Text, typeShip);
            button104_Click(sender, e);
        }
    }
}