using Microsoft.EntityFrameworkCore;
using Stationery.Data;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Stationery.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StationeryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StationeryContext>>()))
            {

                if (!context.Category.Any())
                {
                    context.Category.AddRange(
                        new Category
                        {
                            Name = "Ручки",
                            ImageUrl = "pens.png"
                        },
                        new Category
                        {
                            Name = "Олівці",
                            ImageUrl = "pencils.png"
                        },
                        new Category
                        {
                            Name = "Зошити",
                            ImageUrl = "notebooks.png"
                        },
                        new Category
                        {
                            Name = "Папір для друку",
                            ImageUrl = "paper.png"
                        },
                        new Category
                        {
                            Name = "Канцелярські набори",
                            ImageUrl = "sets.png"
                        }
                    );
                    context.SaveChanges();
                }


                if (!context.Product.Any())
                {

                    var categories = context.Category.ToList();

                    context.Product.AddRange(
                        new Product
                        {
                            Name = "Ручка кулькова синя",
                            Description = "Зручна кулькова ручка для щоденного використання. Сині чорнила.",
                            Price = 15.00m,
                            StockQuantity = 100,
                            ImageUrl = "blue_pen.png",
                            IsFeatured = true,
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Ручки")?.CategoryId ?? 1
                        },
                        new Product
                        {
                            Name = "Олівець графітовий HB",
                            Description = "Стандартний графітовий олівець твердості HB. Підходить для письма та малювання.",
                            Price = 8.50m,
                            StockQuantity = 150,
                            ImageUrl = "hb_pencil.png",
                            IsFeatured = false,
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Олівці")?.CategoryId ?? 2
                        },
                        new Product
                        {
                            Name = "Зошит в клітинку 48 аркушів",
                            Description = "Зошит шкільний в клітинку, 48 аркушів. Стандартна клітинка.",
                            Price = 25.00m,
                            StockQuantity = 80,
                            ImageUrl = "notebook_клетка.png",
                            IsFeatured = true,
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Зошити")?.CategoryId ?? 3
                        },
                         new Product
                         {
                             Name = "Папір офісний А4, 500 аркушів",
                             Description = "Папір офісний формату А4, 500 аркушів в пачці. Щільність 80 г/м2.",
                             Price = 120.00m,
                             StockQuantity = 50,
                             ImageUrl = "a4_paper.png",
                             IsFeatured = false,
                             CategoryId = categories.FirstOrDefault(c => c.Name == "Папір для друку")?.CategoryId ?? 4
                         },
                        new Product
                        {
                            Name = "Набір канцелярський 'Офісний'",
                            Description = "Базовий набір канцелярського приладдя для офісу. Включає ручку, олівець, гумку, лінійку.",
                            Price = 55.00m,
                            StockQuantity = 60,
                            ImageUrl = "office_set.png",
                            IsFeatured = false,
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Канцелярські набори")?.CategoryId ?? 5 // Призначаємо до категорії "Канцелярські набори" або п'ятій категорії, якщо "Канцелярські набори" не знайдено
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}