﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Seed.Data
{
    public static class MappingExtensions
    {
        public static Expression<Func<TEntity, TResult>> GetMember<TEntity, TResult>(string memberName)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "p");
        
            var member = Expression.MakeMemberAccess(
                parameter, 
                typeof(TEntity).GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Single());

            return Expression.Lambda<Func<TEntity, TResult>>(member, parameter);
        }
    }
}
