﻿using System.Globalization;
using CsvHelper;

namespace CodeTool.Domain.Helpers
{
    public static class CsvHelpers
    {
        public static async Task WriteCsvAsync<T>(IEnumerable<T> records, string path)
        {
            await using var writer = new StreamWriter(path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(records);
        }

        public static async Task WriteCsvAsync<T>(IAsyncEnumerable<T> records, string path)
        {
            await using var writer = new StreamWriter(path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(records);
        }
    }
}