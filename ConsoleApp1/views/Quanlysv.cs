using MySql.Data.MySqlClient;
using System.Globalization;

namespace ConsoleApp1.views
{
    public class danhsachsv
    {
        private static string connectionString = @"server=localhost;user id=root;password=123456;port=3306;database=world;";


        public static void HienThiDanhSachSinhVien()
        {
            Console.Clear();
            List<SinhVien> SinhVienList = new List<SinhVien>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM sinhvien";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SinhVien student = new SinhVien
                        {
                            MaSinhVien = reader.GetInt32("MaSinhVien"),
                            HoTen = reader.GetString("HoTen"),
                            NgaySinh = reader.GetString("NgaySinh"),
                            GioiTinh = reader.GetString("GioiTinh"),
                            Email = reader.GetString("Email"),
                            SoDienThoai = reader.GetString("SoDienThoai"),
                            DiaChi = reader.GetString("DiaChi"),
                            MaNganhHoc = reader.GetInt32("MaNganhHoc")
                        };

                        SinhVienList.Add(student);
                    }
                }
            }

            int pageSize = 10;
            int pageCount = (SinhVienList.Count + pageSize - 1) / pageSize;  // calculate total pages
            int currentPage = 1;

            ConsoleKeyInfo pressedKey;
            while (true)
            {
                Console.Clear();
                int startRow = pageSize * (currentPage - 1);
                List<SinhVien> sinhvien = SinhVienList.Skip(startRow).Take(pageSize).ToList();

                if (sinhvien.Count == pageSize)
                {
                    Console.Clear();
                    CreateTable(sinhvien);
                }
                else
                {
                    Console.Clear();
                    CreateTable(sinhvien);
                }
                Console.WriteLine("Trang: <\u001b[33m" + currentPage + "/" + pageCount + "\u001b[0m>");
                Console.Write("Nhan Left \u001b[36mArrow/Right\u001b[0m \u001b[32mArrow\u001b[0m de chuyen trang, \u001b[32mEnter/Space\u001b[0m de thoat che do xem");
                while (true)
                {
                    pressedKey = Console.ReadKey(intercept: true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow && currentPage > 1)
                    {
                        currentPage -= 1;
                        break;
                    }
                    if (pressedKey.Key == ConsoleKey.RightArrow && currentPage < pageCount)
                    {
                        currentPage += 1;
                        break;
                    }
                    if (pressedKey.Key == ConsoleKey.Spacebar || pressedKey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        return;
                    }
                }
 

            }
        }

        public static void CreateTable(List<SinhVien> SinhViens)
        {

            var table = new Table();

            table.SetHeaders("Mã sinh viên", "Họ tên", "Ngày sinh", "Giới tính", "Email", "Số điện thoại", "Địa chỉ", "Mã ngành học");
            foreach (SinhVien sv in SinhViens)
            {
                table.AddRow(sv.MaSinhVien.ToString(), sv.HoTen, sv.NgaySinh.ToString(), sv.GioiTinh, sv.Email, sv.SoDienThoai, sv.DiaChi, sv.MaNganhHoc.ToString());
            }
            Console.WriteLine(table.ToString());
        }



        public List<SinhVien> DanhSachSinhVien()
        {
            List<SinhVien> students = new List<SinhVien>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM sinhvien";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SinhVien student = new SinhVien
                        {
                            MaSinhVien = reader.GetInt32("MaSinhVien"),
                            HoTen = reader.GetString("HoTen"),
                            NgaySinh = reader.GetString("NgaySinh"),
                            GioiTinh = reader.GetString("GioiTinh"),
                            Email = reader.GetString("Email"),
                            SoDienThoai = reader.GetString("SoDienThoai"),
                            DiaChi = reader.GetString("DiaChi"),
                            MaNganhHoc = reader.GetInt32("MaNganhHoc")
                        };

                        students.Add(student);
                    }
                }

            }

            return students;
        }
        public void XoaThongTinSinhVien()
        {
            Console.Write("\u001b[36mNhập mã sinh viên cần xóa: \u001b[0m\n");
            if (!int.TryParse(Console.ReadLine(), out int maSinhVien))
            {
                Console.WriteLine("Mã sinh viên phải là một số nguyên.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM sinhvien WHERE MaSinhVien = @MaSinhVien";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Xóa thông tin sinh viên thành công!");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy sinh viên có mã số này.");
                }
            }

            ReturnToMainMenu();
        }

        public void ThemSinhVien()
        {
            Console.WriteLine("\u001b[36m  ■ THÊM TÀI KHOẢN SINH VIÊN ■\u001b[0m\n"); 

            SinhVien sv = new SinhVien();

            // Input student ID
            while (true)
            {
                Console.Write("\u001b[33m▶ Nhập mã sinh viên: \u001b[0m");
                if (!int.TryParse(Console.ReadLine(), out int maSinhVien))
                {
                    Console.WriteLine("Mã sinh viên phải là một số nguyên.");
                    continue;
                }
                List<SinhVien> danhSachSinhVien = DanhSachSinhVien(); 

                if (danhSachSinhVien.Any(s => s.MaSinhVien == maSinhVien))
                {
                    Console.WriteLine("Mã sinh viên đã tồn tại. Vui lòng nhập lại.");
                    continue;
                }

                sv.MaSinhVien = maSinhVien;
                break;
            }


            Console.Write("\u001b[33m▶ Nhập họ tên: \u001b[0m");
            sv.HoTen = Console.ReadLine();
            Console.Write("\u001b[33m▶ Nhập ngày sinh (yyyy-MM-dd): \u001b[0m");
            sv.NgaySinh = Console.ReadLine();
            Console.Write("\u001b[33m▶ Nhập giới tính: \u001b[0m");
            sv.GioiTinh = Console.ReadLine();
            Console.Write("\u001b[33m▶ Nhập email: \u001b[0m");
            sv.Email = Console.ReadLine();
            Console.Write("\u001b[33m▶ Nhập số điện thoại: \u001b[0m");
            sv.SoDienThoai = Console.ReadLine();
            Console.Write("\u001b[33m▶ Nhập địa chỉ: \u001b[0m");
            sv.DiaChi = Console.ReadLine();

            while (true)
            {
                Console.Write("\u001b[33m▶ Nhập mã ngành học: \u001b[0m");
                if (!int.TryParse(Console.ReadLine(), out int maNganhHoc))
                {
                    Console.WriteLine("Mã ngành học phải là một số nguyên.");
                    continue;
                }

                if (!CheckMajorExistence(maNganhHoc))
                {
                    Console.WriteLine("Mã ngành học không tồn tại. Vui lòng nhập lại.");
                    continue;
                }

                sv.MaNganhHoc = maNganhHoc;
                break;
            }


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, DiaChi, MaNganhHoc) VALUES (@MaSinhVien, @HoTen, @NgaySinh, @GioiTinh, @Email, @SoDienThoai, @DiaChi, @MaNganhHoc)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaSinhVien", sv.MaSinhVien);
                command.Parameters.AddWithValue("@HoTen", sv.HoTen);
                command.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                command.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
                command.Parameters.AddWithValue("@Email", sv.Email);
                command.Parameters.AddWithValue("@SoDienThoai", sv.SoDienThoai);
                command.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
                command.Parameters.AddWithValue("@MaNganhHoc", sv.MaNganhHoc);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Thêm sinh viên thành công!");
            ReturnToMainMenu();

        }

        private bool CheckMajorExistence(int MaNganhHoc)
        {
            bool majorExists = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM NganhHoc WHERE MaNganhHoc = @MaNganhHoc";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaNganhHoc", MaNganhHoc);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    majorExists = count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while checking major existence: " + ex.Message);
            }

            return majorExists;
        }

        public void SuaThongTinSinhVien()
        {
            Console.Write("Nhập mã sinh viên cần sửa: ");
            if (!int.TryParse(Console.ReadLine(), out int maSinhVien))
            {
                Console.WriteLine("Mã sinh viên phải là một số nguyên.");
                return;
            }

            List<SinhVien> danhSachSinhVien = DanhSachSinhVien();

            SinhVien sv = danhSachSinhVien.Find(s => s.MaSinhVien == maSinhVien);
            if (sv != null)
            {
                Console.WriteLine("Nhập thông tin mới:");

                Console.Write("Nhập họ tên: ");
                sv.HoTen = Console.ReadLine();

                    Console.Write("Nhập ngày sinh (yyyy-MM-dd): ");
  

                    sv.NgaySinh = Console.ReadLine();


                Console.Write("Nhập giới tính: ");
                sv.GioiTinh = Console.ReadLine();
                Console.Write("Nhập email: ");
                sv.Email = Console.ReadLine();
                Console.Write("Nhập số điện thoại: ");
                sv.SoDienThoai = Console.ReadLine();
                Console.Write("Nhập địa chỉ: ");
                sv.DiaChi = Console.ReadLine();

                while (true)
                {
                    Console.Write("Nhập mã ngành học: ");
                    if (!int.TryParse(Console.ReadLine(), out int maNganhHoc))
                    {
                        Console.WriteLine("Mã ngành học phải là một số nguyên.");
                        continue;
                    }

                    // Check if major ID exists
                    if (!CheckMajorExistence(maNganhHoc))
                    {
                        Console.WriteLine("Mã ngành học không tồn tại. Vui lòng nhập lại.");
                        continue;
                    }

                    sv.MaNganhHoc = maNganhHoc;
                    break;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE SinhVien SET Ho_ten = @HoTen, Ngay_sinh = @NgaySinh, Gioi_tinh = @GioiTinh, Email = @Email, So_dien_thoai = @SoDienThoai, Dia_chi = @DiaChi, Ma_nganh_hoc = @MaNganhHoc WHERE Ma_sinh_vien = @MaSinhVien";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@HoTen", sv.HoTen);
                    command.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                    command.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
                    command.Parameters.AddWithValue("@Email", sv.Email);
                    command.Parameters.AddWithValue("@SoDienThoai", sv.SoDienThoai);
                    command.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
                    command.Parameters.AddWithValue("@MaNganhHoc", sv.MaNganhHoc);
                    command.Parameters.AddWithValue("@MaSinhVien", sv.MaSinhVien);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Sửa thông tin sinh viên thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã số này.");
            }
            ReturnToMainMenu();

        }
        private void ReturnToMainMenu()
        {
            Menu Menu = new Menu();
            Console.Write("\nNhấn phím bất kỳ để quay lại menu chính...");
            Console.ReadKey();
            Console.Clear();
            Menu.index();
        }

    }
}
