﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Seed.Common.Data
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "OrderByDescending" && method.GetParameters().Length == 2);
        
        private static readonly MethodInfo ThenByMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "ThenBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo ThenByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method => method.Name == "ThenByDescending" && method.GetParameters().Length == 2);

        public static IOrderedQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TSource), "property");
            var orderByProperty = Expression.Property(parameter, propertyName);

            var lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            var genericMethod = OrderByMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });

            return (IOrderedQueryable<TSource>)genericMethod.Invoke(null, new object[] { source, lambda });
        } 
 
        public static IOrderedQueryable<TSource> OrderByPropertyDescending<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TSource), "property");
            var orderByProperty = Expression.Property(parameter, propertyName);

            var lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            var genericMethod = OrderByDescendingMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });

            return (IOrderedQueryable<TSource>)genericMethod.Invoke(null, new object[] { source, lambda });
        }

        public static IOrderedQueryable<TSource> ThenByProperty<TSource>(this IOrderedQueryable<TSource> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TSource), "property");
            var orderByProperty = Expression.Property(parameter, propertyName);

            var lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            var genericMethod = ThenByMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });

            return (IOrderedQueryable<TSource>)genericMethod.Invoke(null, new object[] { source, lambda });
        }

        public static IOrderedQueryable<TSource> ThenByPropertyDescending<TSource>(this IOrderedQueryable<TSource> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TSource), "property");
            var orderByProperty = Expression.Property(parameter, propertyName);

            var lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            var genericMethod = ThenByDescendingMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });

            return (IOrderedQueryable<TSource>)genericMethod.Invoke(null, new object[] { source, lambda });
        } 
    }
}
