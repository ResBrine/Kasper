using Server.AppData.DataBase;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Pages.Table
{
    /// <summary>
    /// Логика взаимодействия для ReportPahe.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void CreateByRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application app = new Excel.Application()
            {
                Visible = true,
                SheetsInNewWorkbook = 1
            };

            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = true;
            Excel.Worksheet work = (Excel.Worksheet)app.Worksheets.get_Item(1);
            work.Name = "Пользователи по группам";
            work.Cells[1, 2] = "ID";
            work.Cells[1, 3] = "Логин";
            work.Cells[1, 4] = "Пароль";
            work.Cells[1, 5] = "Количество сообщений";
            var row = 2;
            Excel.Range rang;
            foreach (var room in Connect.GetRooms)
            {
                rang = work.get_Range("A"+row, "E"+row.ToString());
                rang.Cells.Font.Name = "Times New Roman";
                rang.Font.Name = 14;
                rang.Font.Bold = true;
                rang.Font.Color = ColorTranslator.ToOle(Color.Black);
                rang.Borders.Color = ColorTranslator.ToOle(Color.Black);

                work.Cells[row++, 1] = room.roomName;
                foreach (var user in Connect.GetUsers)
                {
                    foreach (var link in Connect.GetLinks)
                    {
                        if (link.idRoom == room.idRoom && link.idUser == user.idUser)
                        {
                            rang = work.get_Range("A" + row, "E" + row.ToString());
                            rang.Cells.Font.Name = "Times New Roman";
                            rang.Font.Name = 14;
                            rang.Font.Bold = false;
                            rang.Font.Color = ColorTranslator.ToOle(Color.Black);
                            rang.Borders.Color = ColorTranslator.ToOle(Color.Black);
                            work.Cells[row, 2] = user.idUser;
                            work.Cells[row, 3] = user.userName;
                            work.Cells[row, 4] = user.password;
                            work.Cells[row++, 5] = user.countMessage;
                        }
                    }
                }

            }
            rang = work.get_Range("A" + row, "E" + row.ToString());
            rang.Cells.Font.Name = "Times New Roman";
            rang.Font.Name = 14;
            rang.Font.Bold = true;
            rang.Font.Color = ColorTranslator.ToOle(Color.Black);
            rang.Borders.Color = ColorTranslator.ToOle(Color.Black);
            work.Cells[row, 1] = "Итого пользователей:";
            work.Cells[row, 2] = Connect.GetListUsers.Count;

            work.Columns.AutoFit();

        }

    }
}
