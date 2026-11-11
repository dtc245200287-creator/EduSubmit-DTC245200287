using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Dữ liệu mẫu khởi tạo trong bộ nhớ (In-Memory)
var products = new List<ProductDto>
{
    new(1, "Laptop Dell XPS 13", "Laptop mỏng nhẹ cao cấp", 25000000m),
    new(2, "Bàn phím cơ Keychron K2", "Bàn phím cơ không dây Bluetooth", 1800000m),
    new(3, "Chuột Logitech MX Master 3S", "Chuột không dây chuyên đồ họa", 2200000m)
};

// 1. Kiểm tra trạng thái máy chủ (GET /)
app.MapGet("/", () => Results.Ok(new { service = "Catalog API", status = "Healthy" }));

// 2. Lấy danh sách sản phẩm (GET /api/products) - Bổ sung lọc theo query string 'keyword' (Bước 13)
app.MapGet("/api/products", ([FromQuery] string? keyword) =>
{
    if (string.IsNullOrWhiteSpace(keyword))
    {
        return Results.Ok(products);
    }

    var filtered = products
        .Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                   (p.Description != null && p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
        .ToList();

    return Results.Ok(filtered);
});

// 3. Lấy chi tiết sản phẩm theo ID (GET /api/products/{id:int})
app.MapGet("/api/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null)
    {
        return Results.NotFound(new ApiError("PRODUCT_NOT_FOUND", $"Không tìm thấy sản phẩm có mã ID = {id}."));
    }
    return Results.Ok(product);
});

// 4. Tạo sản phẩm mới (POST /api/products)
app.MapPost("/api/products", (CreateProductRequest request) =>
{
    // Kiểm tra dữ liệu đầu vào (Validation)
    if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0)
    {
        return Results.BadRequest(new ApiError("INVALID_PRODUCT", "Tên sản phẩm không được rỗng và giá phải lớn hơn 0."));
    }

    // Loại bỏ khoảng trắng thừa (Trim)
    var cleanName = request.Name.Trim();
    var cleanDescription = request.Description?.Trim();

    // Sinh Id tự động tiếp theo
    int nextId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;

    var newProduct = new ProductDto(nextId, cleanName, cleanDescription, request.Price);
    products.Add(newProduct);

    // Trả về HTTP 201 Created kèm Header Location chỉ tới URI tài nguyên mới
    return Results.Created($"/api/products/{newProduct.Id}", newProduct);
});

app.Run();

// Khai báo các Record DTO & Error ở cuối tệp
public record ProductDto(int Id, string Name, string? Description, decimal Price);
public record CreateProductRequest(string Name, string? Description, decimal Price);
public record ApiError(string Code, string Message);