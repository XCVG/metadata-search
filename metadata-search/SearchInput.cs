using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metadata_search
{
    public class SearchInput
    {
        public string FolderPath { get; init; }
        public bool ExcludeSpecialFolders { get; init; }
        public string? ExcludePath { get; init; }

        public string? Title { get; init; }
        public string? Artist { get; init; }
        public string? ChannelId { get; init; }

        //TODO eventually other parameters
    }
}
