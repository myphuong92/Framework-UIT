# <h1 align="center">IS220.N12<h1>
<h2 align="center">Xây dựng HTTT trên các Framework<h2>

<!-- PROJECT LOGO -->
<div align="center">
  <h3 align="center">Bisys Furniture</h3>

  <p align="center">
    Website bán đồ nội thất
    <br />
    <a href="https://github.com/myphuong92/Framework-UIT"><strong>Khám phá »</strong></a>
    <br />
    <br />
    <a href="#">Xem Demo</a>
    ·
    <a href="https://github.com/myphuong92/Framework-UIT/issues">Các yêu cầu & thắc mắc</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Mục lục</summary>
  <ol>
    <li>
      <a href="#muctieu">Mục tiêu đồ án</a>
    </li>
    <li>
      <a href="#dsthanhvien">Danh sách thành viên</a>
    </li>
    <li><a href="#framework">Các Framework</a></li>
    <li>
      <a href="#chucnang">Các chức năng</a>
    </li>
    <li><a href="#yeucau">Yêu cầu hệ thống</a></li>
    <li>
      <a href="#caidat">Cài đặt và sử dụng</a>
      <ul><a href="#setup">Setup môi trường</a></ul>
      <ul><a href="#start">Khởi động dự </a></ul>
    </li>
    <li><a href="#lienhe">Liên hệ</a></li>
    <li><a href="#banquyen">Bản quyền</a></li>
    <li><a href="#thamkhao">Tài liệu tham khảo</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## <h2 id="muctieu">Mục tiêu của đồ án</h2>
Đây là project của môn học Internet và Công Nghệ Web - UIT. Nội dung là tạo một trang website bán đồ nội thất

Trang web cần đảm bảo được các mục tiêu:
- Giúp khách hàng mua hàng được nhanh chóng và đúng sản phẩm mình cần.
- Tiện lợi cho người bán hàng dễ dàng quản lý cửa hàng của mình.
- Giao diện đơn giản, load nhanh.
## <h2 id="dsthanhvien">Các thành viên tham gia project</h2>
 
| STT| Họ tên         | MSSV                 | Email                                                |     Nhiệm vụ    |
|:--:|----------------|------------------------|----------------------------------------------------|-----------------|
| 1  | Trần Ngọc Mỹ Phương       | 20521779 |20521779@gm.uit.edu.vn                                     |Trưởng nhóm, Frontend Developer, Thiết kế Diagram      |
| 2  | Đoàn Tú Quỳnh | 20521825 |20521825@gm.uit.edu.vn                                             |Backend Developer, Thiết kế Diagram và Database|
| 3  | Đinh Thị Ánh Nguyệt| 20521688 |20521688@gm.uit.edu.vn                                        |Thiết kế Diagram, Đặc tả cấu trúc, Tester|
| 4  | Nguyễn Hữu Hiệu| 20520506 |20520506@gm.uit.edu.vn                                        |Backend Developer, hỗ trợ thiết kế Diagram|
| 5  | Trần Thanh Hiếu | 20520508 |20520508@gm.uit.edu.vn                                        |Thiết kế Diagram, Đặc tả cấu trúc, Tester|


### <h2 id="framework">Các Framework sử dụng</h2>

Trang web được xây dựng bởi các thư viện, framework hiện đại:
* Frontend: [Tailwind CSS](https://tailwindcss.com/) + [Bootstrap](https://getbootstrap.com)
* Backend: [ASP.NET Core](https://dotnet.microsoft.com)
* Database Management Systems: [Microsoft SQl Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

# <h2 id="chucnang">Tóm tắt chức năng</h2>
- Khách hàng:<br/>
  + Đăng nhập, đăng ký, thay đổi mật khẩu
  + Đặt và Mua hàng
  + Xem tình trạng đơn hàng
  + Xem hóa đơn đã mua
  + Xem thông tin account khách hàng, cập nhật lại profile 
- Admin:<br/>
  Thêm, sửa, xóa các mục sau:
  + Hóa đơn,đơn hàng (chỉ được thêm)
  + Tài khoản (account)
  + Danh sách khách hàng
  + Danh sách sản phẩm
  + Chi tiết sản phẩm
  + Khuyến mãi <br/>

# <h2 id="bonus">Chức năng Bonus</h2> 
 + Xác thực, phân quyền tài khoản
 + Đồng bộ Database SQL Server bằng Google Cloud Platform

# <h2 id="yeucau">Yêu cầu hệ thống:</>
- NodeJS 14.18.1
- ASP.NET: .Net 6.0 và ASP.NET Core 3.1

# <h2 id="caidat">Cài đặt và sử dụng</h2>
## <h3 id="setup">Setup môi trường</h3>
1. Tải và cài đặt NodeJs 14.18.1. Link tải [NodeJS](https://nodejs.org/dist/v14.18.1/node-v14.18.1-x64.msi)
+ Vào cmd gõ 
 ```sh
   node -v
   ```
2. Tải Microsoft SQL Server:

Bước 1: Tải và cài Microsoft SQL Server:
- Link tải: [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

Bước 2: Tải và cài đặt SQL Server Management Studio - Công cụ làm việc với CSDL SQL Server:
- Link tải: [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

Bước 3. Tải và cài đặt Visual Studio 2019 trở lên, ưu tiên bản 2022 
- Link tải: [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- Sau đó trong Visual Studio Installer, chọn package **ASP.NET and web development** và cài đặt

## <h3 id="start">Khởi động dự án</h3> 
- Bước 1: Mở file "Funiture_Project.sln" để khởi động dự án
- Bước 2: migration database 
+ Cách làm: Tools -> Nuget Package Manager -> Package Manager Console.
+ Gõ câu lệnh: 
```sh
   update-database
   ```
- Bước 3: Kiểm tra trong CSDL xem đã có database "Furniture" chưa?
- Bước 4: Nếu đã xong bước migration database, tiếp theo ta chỉ cần run project.

## <h2 id="lienhe">Liên hệ</h2>

Trần Ngọc Mỹ Phương

Project Link: [https://github.com/myphuong92/Framework-UIT) </br>
Email: [Mỹ Phương](mailto:20521779@gm.uit.edu.vn)

# <h2 id="banquyen">Bản quyền</h3>
Copyright © 2022, [Bisys Furniture](https://github.com/myphuong92/Framework-UIT).
# <h2 id="thamkhao">Tài liệu tham khảo</h2> 
- https://www.w3schools.com/
- https://tailwindcss.com/
- https://dotnet.microsoft.com/learn/aspnet/hello-world-tutorial/intro
