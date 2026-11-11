# Bài 2 – RESTful API Catalog API (Phần A & Phần B)

**Họ tên:** Phạm Bảo Lâm | **MSSV:** DTC245200287 | **Lớp:** CNTTK23E  
**Học phần:** Phát triển ứng dụng phân tán | **Nền tảng:** ASP.NET Core Minimal API (.NET 10)

---

## 1. Tổng quan hệ thống và Cấu trúc dự án

Dự án **Catalog API** xây dựng mô hình tương tác RESTful API giữa dịch vụ quản lý danh mục sản phẩm (`Catalog.Api`) và máy khách tự động (`Catalog.Client`).

### Cấu trúc thư mục Bài 2:
```text
CatalogPractice/
├── Catalog.slnx                  # Solution chứa các dự án bài 2
├── Catalog.Api/                  # Máy chủ Web API (Minimal API)
│   ├── Program.cs                # Mã nguồn xử lý các endpoints, validation và DTO
│   └── Catalog.Api.csproj
├── Catalog.Client/               # Máy khách Console Client
│   ├── Program.cs                # Mã nguồn tự động gọi 4 hoạt động HTTP
│   └── Catalog.Client.csproj
└── docs/
    ├── bai2.md                   # Báo cáo tổng kết bài 2
    └── images/                   # Thư mục lưu trữ hình ảnh minh chứng
    