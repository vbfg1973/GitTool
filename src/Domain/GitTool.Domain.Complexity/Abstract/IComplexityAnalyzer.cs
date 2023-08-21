﻿namespace GitTool.Domain.Complexity.Abstract
{
    /// <summary>
    ///     Generic interface for a complexity analyzer
    /// </summary>
    public interface IComplexityAnalyzer
    {
        int ComplexityScore { get; }
    }
}