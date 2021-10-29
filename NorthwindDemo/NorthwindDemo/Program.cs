using System;
using Microsoft.EntityFrameworkCore;
using NorthwindDemo;

using var dataContext = new DataContext();

//var units = 20;
//var productsQuery = dataContext.Products.Where(p => p.UnitsInStock > units).OrderBy(p => p.Name);
//var query = productsQuery.ToQueryString();

//var products = await productsQuery.ToListAsync();

//var categoriesQuery = dataContext.Categories
//    .Include(c => c.Products.OrderByDescending(p => p.UnitPrice).Take(5));

var categoriesQuery = dataContext.Categories.AsSplitQuery().Include(c => c.Products);
var categoriesQueryString = categoriesQuery.ToQueryString();
var categories = await categoriesQuery.ToListAsync();

Console.ReadLine();
