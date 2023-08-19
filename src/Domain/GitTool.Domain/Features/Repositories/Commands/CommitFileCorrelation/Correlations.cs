namespace GitTool.Domain.Features.Repositories.Commands.CommitFileCorrelation
{
    public class Correlations : Dictionary<string, Dictionary<string, int>>
    {
        public IEnumerable<CorrelationData> CorrelationData()
        {
            foreach (var file in Keys)
            {
                var commitCount = this[file].Select(x => x.Value).Sum();

                foreach (var coChangingFile in this[file].Keys)
                    yield return new CorrelationData
                    {
                        File = file,
                        Commits = commitCount,
                        CoChangingFile = coChangingFile,
                        CoChangesCount = this[file][coChangingFile]
                    };
            }
        }

        public void AddSet(IEnumerable<string> set)
        {
            var setArray = set as string[] ?? set.ToArray();

            foreach (var setItem in setArray)
                if (!ContainsKey(setItem))
                    InitialisePivotKey(setItem, setArray);

                else
                    PopulateSubKeys(setArray, setItem);
        }

        private void PopulateSubKeys(string[] setArray, string setItem)
        {
            foreach (var linkedItem in setArray.Where(item => item != setItem))
                if (!this[setItem].ContainsKey(linkedItem))
                    this[setItem][linkedItem] = 1;

                else
                    this[setItem][linkedItem]++;
        }

        private void InitialisePivotKey(string setItem, string[] setArray)
        {
            Add(setItem, new Dictionary<string, int>());

            foreach (var linkedItem in setArray.Where(item => item != setItem))
                this[setItem].TryAdd(linkedItem, 1);
        }
    }
}