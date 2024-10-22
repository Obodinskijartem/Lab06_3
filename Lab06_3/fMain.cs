using Lab06_3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_5
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
                gvSubscribers.AutoGenerateColumns = false;

                // Додаємо колонки
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "Name";
                column.Name = "Ім'я абонента";
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "PhoneNumber";
                column.Name = "Номер телефону";
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "Address";
                column.Name = "Адреса";
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "CallMinutesPerMonth";
                column.Name = "Хвилин дзвінків на місяць";
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "SMSPerMonth";
                column.Name = "Кількість SMS на місяць";
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "MonthlyFee";
                column.Name = "Щомісячна плата, грн";
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewCheckBoxColumn();
                column.DataPropertyName = "HasRoaming";
                column.Name = "Роумінг";
                column.Width = 60;
                gvSubscribers.Columns.Add(column);

                column = new DataGridViewCheckBoxColumn();
                column.DataPropertyName = "HasDataPlan";
                column.Name = "Тарифний план даних";
                column.Width = 60;
                gvSubscribers.Columns.Add(column);

                // Додаємо записи
                bindSrcSubscribers.Add(new Subscriber("Іван", "+380501234567", "Київ", 100, 50, 200, true, false));
                bindSrcSubscribers.Add(new Subscriber("Олена", "+380671234567", "Львів", 200, 150, 300, false, true));

                // Прив'язуємо джерело даних
                gvSubscribers.DataSource = bindSrcSubscribers;

                // Оновлюємо DataGridView
                gvSubscribers.Refresh();
            }

            private void btnAdd_Click(object sender, EventArgs e)
        {
            IClient client = new Subscriber();
            fSubscriber fs = new fSubscriber(client);
            if (fs.ShowDialog() == DialogResult.OK)
            {
                bindSrcSubscribers.Add((Subscriber)client);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            IClient client = (IClient)bindSrcSubscribers.List[bindSrcSubscribers.Position];
            fSubscriber fs = new fSubscriber(client);
            if (fs.ShowDialog() == DialogResult.OK)
            {
                bindSrcSubscribers.List[bindSrcSubscribers.Position] = (Subscriber)client;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити поточний запис?", "Видалення запису",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcSubscribers.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindSrcSubscribers.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити застосунок?", "Вихід з програми", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void fMain_Resize(object sender, EventArgs e)
        {
            int buttonsSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width + 30;
            btnExit.Margin = new Padding(Width - buttonsSize, 0, 0, 0);
        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
            saveFileDialog.Title = "Зберегти дані у текстовому форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            StreamWriter sw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                try
                {
                    foreach (Subscriber subscriber in bindSrcSubscribers.List)
                    {
                        sw.Write(subscriber.Name + "\t" + subscriber.PhoneNumber + "\t" +
                            subscriber.Address + "\t" + subscriber.CallMinutesPerMonth + "\t" +
                            subscriber.SMSPerMonth + "\t" + subscriber.MonthlyFee + "\t" +
                            subscriber.HasRoaming + "\t" + subscriber.HasDataPlan + "\t");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних (*.subscribers) |*.subscribers|All files (*.*) |*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            BinaryWriter bw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bw = new BinaryWriter(saveFileDialog.OpenFile());
                try
                {
                    foreach (Subscriber subscriber in bindSrcSubscribers.List)
                    {
                        bw.Write(subscriber.Name);
                        bw.Write(subscriber.PhoneNumber);
                        bw.Write(subscriber.Address);
                        bw.Write(subscriber.CallMinutesPerMonth);
                        bw.Write(subscriber.SMSPerMonth);
                        bw.Write(subscriber.MonthlyFee);
                        bw.Write(subscriber.HasRoaming);
                        bw.Write(subscriber.HasDataPlan);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bw.Close();
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            StreamReader sr;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcSubscribers.Clear();
                sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                string s;
                try
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] split = s.Split('\t');
                        Subscriber subscriber = new Subscriber(split[0], split[1], split[2],
                            int.Parse(split[3]), int.Parse(split[4]), double.Parse(split[5]),
                            bool.Parse(split[6]), bool.Parse(split[7]), (int)double.Parse(split[8]),
                           (int)double.Parse(split[9]));
                        bindSrcSubscribers.Add(subscriber);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файли даних (*.subscribers) |*.subscribers|All files (*.*) |*.*";
            openFileDialog.Title = "Прочитати дані у бінарному форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            BinaryReader br;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcSubscribers.Clear();
                br = new BinaryReader(openFileDialog.OpenFile());
                try
                {
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        Subscriber subscriber = new Subscriber();
                        subscriber.Name = br.ReadString();
                        subscriber.PhoneNumber = br.ReadString();
                        subscriber.Address = br.ReadString();
                        subscriber.CallMinutesPerMonth = br.ReadInt32();
                        subscriber.SMSPerMonth = br.ReadInt32();
                        subscriber.MonthlyFee = br.ReadDouble();
                        subscriber.HasRoaming = br.ReadBoolean();
                        subscriber.HasDataPlan = br.ReadBoolean();
                        bindSrcSubscribers.Add(subscriber);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    br.Close();
                }
            }
        }

        private void gvSubscribers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}