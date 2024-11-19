using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton(provider =>
{
    var productsPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "products.json");
    var ordersPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "orders.json");

    var productsJson = File.ReadAllText(productsPath);
    var ordersJson = File.ReadAllText(ordersPath);

    var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();
    var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();

    return new DataService(products, orders);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

public class DataService
{
    public List<Product> Products { get; }
    public List<Order> Orders { get; }

    public DataService(List<Product> products, List<Order> orders)
    {
        Products = products ?? new List<Product>();
        Orders = orders ?? new List<Order>();
    }
}
public record Product(
    [property: JsonPropertyName("product_id")] int ProductId,
    [property: JsonPropertyName("site_id")] int SiteId,
    [property: JsonPropertyName("product_name")] string ProductName,
    [property: JsonPropertyName("cost")] double Cost,
    [property: JsonPropertyName("inventory_only")] bool InventoryOnly,
    [property: JsonPropertyName("private")] bool Private
);

public record OrderItem(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("siteId")] int SiteId,
    [property: JsonPropertyName("productId")] int ProductId,
    [property: JsonPropertyName("currency")] string Currency,
    [property: JsonPropertyName("paymentMethod")] string PaymentMethod,
    [property: JsonPropertyName("retailCost")] double RetailCost,
    [property: JsonPropertyName("accountDiscount")] double AccountDiscount,
    [property: JsonPropertyName("couponDiscount")] double CouponDiscount,
    [property: JsonPropertyName("netCost")] double NetCost
);

public record Order(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("paymentMethod")] string PaymentMethod,
    [property: JsonPropertyName("recruiterId")] int RecruiterId,
    [property: JsonPropertyName("divisionId")] int DivisionId,
    [property: JsonPropertyName("companyId")] int CompanyId,
    [property: JsonPropertyName("validated")] bool Validated,
    [property: JsonPropertyName("created")] DateTime Created,
    [property: JsonPropertyName("completed")] DateTime? Completed,
    [property: JsonPropertyName("items")] List<OrderItem> Items
);