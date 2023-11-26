using LibraryWebAPI.Data;
using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/book", () =>
{
    return Results.Ok(BookStore.bookList);
});

app.MapGet("/api/book/{id:int}", (int id) =>
{
    return Results.Ok(BookStore.bookList.FirstOrDefault(u => u.Id == id));
});

app.MapPost("/api/book/", ([FromBody] Book book) =>
{
    if (book.Id != 0 || string.IsNullOrEmpty(book.Title))
    {
        return Results.BadRequest("Invalid Id or Book Name");

    }
    if (BookStore.bookList.FirstOrDefault(u => u.Title.ToLower() == book.Title.ToLower()) != null)
    {
        return Results.BadRequest("Book name already exist");
    }

    book.Id = BookStore.bookList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
    book.Title = book.Title.ToLower();
    book.Created = DateTime.Now;
    book.Returned = DateTime.Now.AddDays(3);
    book.Availabledate = book.Returned.AddDays(1);
    BookStore.bookList.Add(book);
    return Results.Ok(book);
});

app.MapGet("/api/getLoanbooks/", (bool onloan) =>
{
    return Results.Ok(BookStore.bookList.Where(u => u.Onloan == true));
});

app.MapPut("/api/retrunbook/{id:int}", (int id) =>
{
    var book = BookStore.bookList.Where(u => u.Id == id).ToList();

    foreach (var bk in book)
    {
        if (DateTime.Now > bk.Returned)
        {
            bk.Fine = 0.20;
        }
        bk.Onloan = false;

    }
    return Results.Ok(book);
});
app.MapPut("/api/reservebook/{id:int}", (int id) =>
{
    var book = BookStore.bookList.Where(u => u.Id == id).ToList();

    foreach (var bk in book)
    {
        bk.Reserved = true;
    }
    return Results.Ok(book);
});

app.UseHttpsRedirection();
app.Run();




