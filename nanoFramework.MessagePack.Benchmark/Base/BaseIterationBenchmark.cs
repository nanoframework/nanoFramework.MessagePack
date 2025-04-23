﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Benchmark.Base
{
    using System;

    /// <summary>
    /// Base benchmark class.
    /// </summary>
    public abstract class BaseIterationBenchmark
    {
        /// <summary>
        /// Internal iteration count.
        /// </summary>
        protected virtual int _iterationCount => 20;

        /// <summary>
        /// Call iteration benchmark method.
        /// </summary>
        /// <param name="methodToRun">Iteration benchmark method.</param>
        /// <exception cref="ArgumentNullException">iteration benchmark method is <see  cref="null"/></exception>
        protected void RunInIteration(Action methodToRun)
        {
            if (methodToRun == null)
            {
                throw new ArgumentNullException(nameof(methodToRun));
            }
            else
            {
                int step = 0;
                while (step++ < this._iterationCount)
                {
                    methodToRun();
                }
            }
        }
    }
}
