﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Clauses.StreamedData;

namespace Microsoft.Data.Entity.Query.ResultOperators
{
    public class AsNoTrackingResultOperator : SequenceTypePreservingResultOperatorBase
    {
        public override string ToString()
        {
            return "AsNoTracking()";
        }

        public override ResultOperatorBase Clone([NotNull] CloneContext cloneContext)
        {
            return new AsNoTrackingResultOperator();
        }

        public override void TransformExpressions([NotNull] Func<Expression, Expression> transformation)
        {
        }

        public override StreamedSequence ExecuteInMemory<T>([NotNull] StreamedSequence input)
        {
            return input; // sequence is not changed by this operator
        }
    }
}
