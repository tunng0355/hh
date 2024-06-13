using System;
using System.Threading;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace ConsoleApp1.views
{
    public class Menu
    {
        public void index()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random random = new Random();
            Console.Clear();

            int luaChon = 1; // Lựa chọn mặc định là 1
            bool tiepTuc = true;
            while (tiepTuc)
            {
                Console.Clear(); // Xóa màn hình để tạo hiệu ứng mở menu

                Console.WriteLine("\u001b[36m  ■ QUẢN LÝ SINH VIÊN ■\u001b[0m\n"); // In tiêu đề với hiệu ứng rung động


                Console.ResetColor();
                Console.WriteLine((luaChon == 1 ? "\u001b[33m\u25B6 " : "  ") + "Xem danh sách sinh viên" + (luaChon == 1 ? "\u001b[0m" : ""));
                Console.WriteLine((luaChon == 2 ? "\u001b[33m\u25B6 " : "  ") + "Thêm sinh viên" + (luaChon == 2 ? "\u001b[0m" : ""));
                Console.WriteLine((luaChon == 3 ? "\u001b[33m\u25B6 " : "  ") + "Sửa thông tin sinh viên" + (luaChon == 3 ? "\u001b[0m" : ""));
                Console.WriteLine((luaChon == 4 ? "\u001b[33m\u25B6 " : "  ") + "Xóa thông tin sinh viên" + (luaChon == 4 ? "\u001b[0m" : ""));
                Console.WriteLine((luaChon == 0 ? "\u001b[33m\u25B6 " : "  ") + "Thoát" + (luaChon == 0 ? "\u001b[0m" : ""));

                var key = Console.ReadKey(true);
                danhsachsv danhsachsv = new danhsachsv();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        luaChon = luaChon == 0 ? 4 : luaChon - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        luaChon = luaChon == 4 ? 0 : luaChon + 1;
                        break;
                    case ConsoleKey.Enter:
                        tiepTuc = luaChon != 0;
                        switch (luaChon)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("\u001b[36mĐang mở danh sách sinh viên...\u001b[0m");
                                Thread.Sleep(1000);
                                Console.Clear();
                                danhsachsv.HienThiDanhSachSinhVien();
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("\u001b[33mĐang mở chức năng thêm sinh viên...\u001b[0m");
                                Thread.Sleep(1000);
                                Console.Clear();
                                danhsachsv.ThemSinhVien();
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("\u001b[33mĐang mở chức năng sửa thông tin sinh viên...\u001b[0m");
                                Thread.Sleep(1000);
                                Console.Clear();
                                danhsachsv.SuaThongTinSinhVien();
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("\u001b[33mĐang mở chức năng xóa thông tin sinh viên...\u001b[0m");
                                Thread.Sleep(1000);
                                Console.Clear();
                                danhsachsv.XoaThongTinSinhVien();
                                break;
                            case 0:
                                tiepTuc = false;
                                break;
                            default:
                                Console.WriteLine("\u001b[33mChức năng không hợp lệ. Vui lòng chọn lại...\u001b[0m");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                  }



               
            }
        }
    }
}
