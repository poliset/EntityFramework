// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Microsoft.Data.Entity.Relational.Query.ResultOperators
{
    public class FromSqlExpressionNode : ResultOperatorExpressionNodeBase
    {
        public static readonly MethodInfo[] SupportedMethods = { RelationalQueryableExtensions.FromSqlMethodInfo };

        private readonly ConstantExpression _sqlConstant;

        public FromSqlExpressionNode(
            MethodCallExpressionParseInfo parseInfo,
            [NotNull] ConstantExpression sqlConstant)
            : base(parseInfo, null, null)
        {
            Check.NotNull(sqlConstant, nameof(sqlConstant));

            _sqlConstant = sqlConstant;
        }

        protected override ResultOperatorBase CreateResultOperator(ClauseGenerationContext clauseGenerationContext)
        {
            return new FromSqlResultOperator(_sqlConstant);
        }

        public override Expression Resolve(
            ParameterExpression inputParameter,
            Expression expressionToBeResolved,
            ClauseGenerationContext clauseGenerationContext)
        {
            Check.NotNull(inputParameter, nameof(inputParameter));
            Check.NotNull(expressionToBeResolved, nameof(expressionToBeResolved));

            return Source.Resolve(inputParameter, expressionToBeResolved, clauseGenerationContext);
        }
    }
}
