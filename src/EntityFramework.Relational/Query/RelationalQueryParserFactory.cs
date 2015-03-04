// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Relational.Query.ResultOperators;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

namespace Microsoft.Data.Entity.Relational.Query
{
    public class RelationalQueryParserFactory : QueryParserFactory
    {
        protected override void RegisterMethodInfoNodeTypes(MethodInfoBasedNodeTypeRegistry methodInfoBasedNodeTypeRegistry)
        {
            base.RegisterMethodInfoNodeTypes(methodInfoBasedNodeTypeRegistry);

            methodInfoBasedNodeTypeRegistry
                .Register(FromSqlExpressionNode.SupportedMethods, typeof(FromSqlExpressionNode));
        }
    }
}
