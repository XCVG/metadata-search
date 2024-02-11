using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metadata_search
{
    public class SearchResult
    {
        public string FullPath { get; init; }
        public string Name { get; init; }

        //TODO more detailed metadata?

        public string Description { get; init; }
    }
}
