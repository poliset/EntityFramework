﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational.Query.Annotations;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class RelationalDbSetExtensions
    {
        public static DbSet<TEntity> FromSql<TEntity>([NotNull]this DbSet<TEntity> dbSet, [NotNull]string sql, [NotNull] params object[] parameters)
            where TEntity : class
        {
            Check.NotNull(dbSet, nameof(dbSet));
            Check.NotEmpty(sql, nameof(sql));
            Check.NotNull(parameters, nameof(parameters));

            return (dbSet as IDbSetExtender<TEntity>).AnnotateQuery(new FromSqlAnnotation(sql, parameters));
        }
    }
}
