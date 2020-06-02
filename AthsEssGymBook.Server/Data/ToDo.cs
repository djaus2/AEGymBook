    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using AthsEssGymBook.Shared;
using System.Linq;

namespace AthsEssGymBook.Server.Data
{
        public static class BookingsoApi
        {
            private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            public static async Task GetAllAsync(HttpContext context)
            {
                var db = new BookingsDBContext();
                var BookingInfo = await db.BookingInfo.ToListAsync();

                await context.Response.WriteJsonAsync(BookingInfo, _options);
            }

            public static async Task GetAsync(HttpContext context)
            {
                //if (!context.Request.RouteValues.TryGet("id", out int id))
                if (!context.Request.RouteValues.ContainsKey("id")) //, out int id))
                {
                    context.Response.StatusCode = 400;
                    return;
                }

                int id = (int)context.Request.RouteValues["id"];
                var db = new BookingsDBContext();
                var todo = await db.BookingInfo.FindAsync(id);
                if (todo == null)
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                await context.Response.WriteJsonAsync(todo, _options);
            }

            public static async Task PostAsync(HttpContext context)
            {
            //var todo = await context.Request.ReadJsonAsync<BookingInfo>(_options);
            var todojson = await context.Request

            var db = new BookingsDBContext();
                await db.BookingInfo.AddAsync(todo);
                await db.SaveChangesAsync();
            }

            public static async Task DeleteAsync(HttpContext context)
            {
                //if (!context.Request.RouteValues.TryGet("id", out int id))
                if (!context.Request.RouteValues.ContainsKey("id"))
                {
                    context.Response.StatusCode = 400;
                    return;
                }

                int id = (int)context.Request.RouteValues["id"];
                var db = new BookingsDBContext();
                var todo = await db.BookingInfo.FindAsync(id);
                if (todo == null)
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                db.BookingInfo.Remove(todo);
                await db.SaveChangesAsync();
            }

            public static void MapRoutes(IEndpointRouteBuilder endpoints)
            {
                endpoints.MapGet("/api/BookingInfo3", GetAllAsync);
                endpoints.MapGet("/api/BookingInfo3/{id}", GetAsync);
                endpoints.MapPost("/api/BookingInfo3", PostAsync);
                endpoints.MapDelete("/api/BookingInfo3/{id}", DeleteAsync);
            }
        }
    }

