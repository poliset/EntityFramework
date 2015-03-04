// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Reflection;
using Microsoft.Data.Entity.Query.ResultOperators;
using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
using Remotion.Linq.Parsing.Structure;
using Remotion.Linq.Parsing.Structure.ExpressionTreeProcessors;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

namespace Microsoft.Data.Entity.Query
{
    public class QueryParserFactory
    {
        public virtual QueryParser Create()
        {
            return new QueryParser(
                new ExpressionTreeParser(
                    CreateNodeTypeProvider(),
                    new CompoundExpressionTreeProcessor(new IExpressionTreeProcessor[]
                    {
                        new PartialEvaluatingExpressionTreeProcessor(),
                        new TransformingExpressionTreeProcessor(ExpressionTransformerRegistry.CreateDefault())
                    })));
        }

        private CompoundNodeTypeProvider CreateNodeTypeProvider()
        {
            var searchedTypes
                = typeof(MethodInfoBasedNodeTypeRegistry)
                    .GetTypeInfo()
                    .Assembly
                    .DefinedTypes
                    .Select(ti => ti.AsType())
                    .ToList();

            var methodInfoBasedNodeTypeRegistry
                = MethodInfoBasedNodeTypeRegistry.CreateFromTypes(searchedTypes);

            RegisterMethodInfoNodeTypes(methodInfoBasedNodeTypeRegistry);

            var innerProviders
                = new INodeTypeProvider[]
                    {
                        methodInfoBasedNodeTypeRegistry,
                        MethodNameBasedNodeTypeRegistry.CreateFromTypes(searchedTypes)
                    };

            return new CompoundNodeTypeProvider(innerProviders);
        }

        protected virtual void RegisterMethodInfoNodeTypes(MethodInfoBasedNodeTypeRegistry methodInfoBasedNodeTypeRegistry)
        {
            methodInfoBasedNodeTypeRegistry
                .Register(AsNoTrackingExpressionNode.SupportedMethods, typeof(AsNoTrackingExpressionNode));

            methodInfoBasedNodeTypeRegistry
                .Register(IncludeExpressionNode.SupportedMethods, typeof(IncludeExpressionNode));

            methodInfoBasedNodeTypeRegistry
                .Register(ThenIncludeExpressionNode.SupportedMethods, typeof(ThenIncludeExpressionNode));
        }
    }
}
