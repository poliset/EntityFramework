// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Clauses.StreamedData;

namespace Microsoft.Data.Entity.Relational.Query.ResultOperators
{
    public class FromSqlResultOperator : SequenceTypePreservingResultOperatorBase
    {
        private ConstantExpression _sqlExpression;

        public FromSqlResultOperator([NotNull] ConstantExpression sqlExpression)
        {
            Check.NotNull(sqlExpression, nameof(sqlExpression));

            _sqlExpression = sqlExpression;
        }

        public virtual ConstantExpression SqlExpression => _sqlExpression;

        public override string ToString()
        {
            return "FromSql("
                + FormattingExpressionTreeVisitor.Format(_sqlExpression)
                + ")";
        }

        public override ResultOperatorBase Clone([NotNull] CloneContext cloneContext)
        {
            Check.NotNull(cloneContext, nameof(cloneContext));

            return new FromSqlResultOperator(_sqlExpression);
        }

        public override void TransformExpressions([NotNull] Func<Expression, Expression> transformation)
        {
        }

        public override StreamedSequence ExecuteInMemory<T>([NotNull] StreamedSequence input)
        {
            Check.NotNull(input, nameof(input));

            return input;
        }
    }
}
