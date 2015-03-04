// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Xunit;

namespace Microsoft.Data.Entity.Relational.FunctionalTests
{
    public class QueryRelationalTest<TFixture> : QueryTestBase<TFixture>
        where TFixture : NorthwindQueryFixtureBase, new()
    {
        public QueryRelationalTest(TFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public virtual void Custom_queryable_simple()
        {
            AssertQuery<Customer>(
                cs => cs.FromSql("SELECT * FROM Customers"),
                cs => cs,
                entryCount: 91);
        }

        [Fact]
        public virtual void Custom_queryable_filter()
        {
            AssertQuery<Customer>(
                cs => cs.FromSql("SELECT * FROM Customers WHERE Customers.ContactName LIKE '%z%'"),
                cs => cs.Where(c => c.ContactName.Contains("z")),
                entryCount: 14);
        }

        [Fact]
        public virtual void Custom_queryable_cached_by_query()
        {
            AssertQuery<Customer>(
                cs => cs.FromSql("SELECT * FROM Customers WHERE Customers.City = 'London'"),
                cs => cs.Where(c => c.City == "London"),
                entryCount: 6);

            AssertQuery<Customer>(
                cs => cs.FromSql("SELECT * FROM Customers WHERE Customers.City = 'Seattle'"),
                cs => cs.Where(c => c.City == "Seattle"),
                entryCount: 1);
        }

        [Fact]
        public virtual void Custom_queryable_where_simple_closure_via_query_cache()
        {
            var title = "Sales Associate";

            AssertQuery<Customer>(
                cs => cs.Where(c => c.ContactTitle == title).FromSql("SELECT * FROM Customers WHERE Customers.ContactName LIKE '%o%'"),
                cs => cs.Where(c => c.ContactName.Contains("o")).Where(c => c.ContactTitle == title),
                entryCount: 4);

            title = "Sales Manager";

            AssertQuery<Customer>(
                cs => cs.FromSql("SELECT * FROM Customers WHERE Customers.ContactName LIKE '%o%'").Where(c => c.ContactTitle == title),
                cs => cs.Where(c => c.ContactName.Contains("o")).Where(c => c.ContactTitle == title),
                entryCount: 7);
        }
    }
}
