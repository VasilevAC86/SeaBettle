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
            for (int i = 0; i < 10; i++) // Инициализируем строки элемента формы dataGridView1
                dataGridView1.Rows.Add(row);
            dataGridView1.ClearSelection(); // Снимаем выделение клеток

        }
        Model model;
        string[] row = { "", "", "", "", "", "", "", "", "", "" }; // Массив наименований строк
        int x4 = 1;
        int x3 = 2;
        int x2 = 3;
        int x1 = 4;
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
            /* for (int x = 0; x < 10; x++)
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
                 }*/
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    {
                        switch (model.PlayerShips[x, y])
                        {
                            case CoordStatus.Ship:
                                dataGridView1[x, y].Value = "x";
                                break;
                            case CoordStatus.None:
                                dataGridView1[x, y].Value = "";
                                break;
                        }
                    }
                }
        }

        private void button103_Click(object sender, EventArgs e) // Кнопка "Поставить" 
        {
            int cnt = dataGridView1.SelectedCells.Count;
            if (cnt > 0)
            {
                if (checkBox2.Checked) // Проверка, чтобы удаление пустых ячеек не засчитывалось за удаление корабля
                {
                    int a, b; // Координаты первой выделенной ячейки
                    a = dataGridView1.SelectedCells[0].RowIndex;
                    b = dataGridView1.SelectedCells[0].ColumnIndex;
                    if (dataGridView1.Rows[a].Cells[b].Value.ToString() == "") return;
                }
                // Изменяем кол-во соответствующих кораблей
                if (cnt == 1)
                    if (!checkBox2.Checked) // Если добавляем корабль, то доступное кол-во уменьшаем
                    {
                        if (x1 == 0) return;
                        x1--;
                    }
                    else x1++; // Если удаляем корабль, то доступное кол-во увеличиваем
                if (cnt == 2)
                    if (!checkBox2.Checked)
                    {
                        if (x2 == 0) return;
                        x2--;
                    }
                    else x2++;
                if (cnt == 3)
                    if (!checkBox2.Checked)
                    {
                        if (x3 == 0) return;
                        x3--;
                    }
                    else x3++;
                if (cnt == 4)
                    if (!checkBox2.Checked)
                    {
                        if (x4 == 0) return;
                        x4--;
                    }
                    else x4++;
                for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                {
                    int x = dataGridView1.SelectedCells[i].ColumnIndex;
                    int y = dataGridView1.SelectedCells[i].RowIndex;
                    CoordStatus coordStatus;
                    if (!checkBox2.Checked)
                        coordStatus = CoordStatus.Ship;
                    else
                        coordStatus = CoordStatus.None;
                    model.PlayerShips[x, y] = coordStatus;
                }
                dataGridView1.ClearSelection(); // Убираем выделение клеток, кода поставили корабль
            }
            /*else // Размещение корабля по координатам
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
            }*/
            button104_Click(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int y = dataGridView1.SelectedCells[0].RowIndex;
            int x = dataGridView1.SelectedCells[0].ColumnIndex;
            textBox1.Text = x.ToString() + y.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) // Меняем название кнопки "Поставить" в зависимости от галочки удалить
        {
            if (checkBox2.Checked) { button103.Text = "Удалить"; }
            else { button103.Text = "Поставить"; }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int cnt = dataGridView1.SelectedCells.Count;
            textBox1.Text = cnt.ToString();
            if (cnt > 4)
            {
                MessageBox.Show("Превышено кол-во клеток!");
                int x = dataGridView1.SelectedCells[cnt - 1].ColumnIndex;
                int y = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows[y].Cells[x].Selected = false; // Не разрешаем больше выделять
                dataGridView1.ClearSelection(); // Очищаем всё, что выделили
            }
            /*if (cnt == 4)
                dataGridView1.SelectedCells.*/
        }

        private void Cell_Click(object sender, EventArgs e) // Обработчик события нажатия на кнопку
        {
            string msg = ((Button)sender).Name;
            textBox1.Text = msg;
        }

        private void Form1_Load(object sender, EventArgs e) // Обработчик события загрузки формы
        {
            foreach (var el in this.Controls) // Обходим все элементы формы
            {
                // Обрабатываем только те кнопки, которые ячейки поля
                if (el is Button && el != button103 && el != button104) 
                {
                    ((Button)el).Click += Cell_Click;
                }
            }
        }
    }
}