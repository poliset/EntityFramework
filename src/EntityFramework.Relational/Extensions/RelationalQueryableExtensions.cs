// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace System.Linq
{
    public static class RelationalQueryableExtensions
    {
        internal static readonly MethodInfo FromSqlMethodInfo
            = typeof(RelationalQueryableExtensions)
            .GetTypeInfo().GetDeclaredMethod("FromSql");

        public static IQueryable<TEntity> FromSql<TEntity>([NotNull] this IQueryable<TEntity> source, [NotNull] string sql) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotEmpty(sql, nameof(sql));

            return source.Provider.CreateQuery<TEntity>(
                Expression.Call(
                    null,
                    FromSqlMethodInfo.MakeGenericMethod(typeof(TEntity)),
                    source.Expression, Expression.Constant(sql)));
        }
    }
}
