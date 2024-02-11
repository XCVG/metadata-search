using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace metadata_search
{
    //does it make sense for this to be a model class? iunno

    public class SearchResultSet
    {
        //TODO un-hardcode this
        private static readonly HashSet<string> MEDIA_FILE_EXTENSIONS = new HashSet<string>() { "mkv", "mp4", "webm" };

        private Dictionary<string, SearchResult> Results;
        //we will not expose this directly but will have accessors

        public static async Task<SearchResultSet> SearchFolder(SearchInput input, CancellationToken cancellationToken)
        {
            //for testing sequence/cancellation
            
            List<string> foldersToCheck = new List<string>();
            GetFoldersRecurse(input.FolderPath, input.ExcludeSpecialFolders, input.ExcludePath, foldersToCheck);

            Dictionary<string, SearchResult> results = new Dictionary<string, SearchResult>();

            foreach (string folderPath in foldersToCheck)
            {
                cancellationToken.ThrowIfCancellationRequested();

                foreach(var filePath in Directory.EnumerateFiles(folderPath))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        //TODO filter by extension and then grep
                        string extension = Path.GetExtension(filePath).TrimStart('.');

                        if (!MEDIA_FILE_EXTENSIONS.Contains(extension))
                            continue;

                        string fileName = Path.GetFileNameWithoutExtension(filePath);

                        var tagFile = TagLib.File.Create(filePath);
                        var tags = tagFile.Tag;

                        string title = tags.Title;
                        if (string.IsNullOrEmpty(title))
                            title = fileName; //for now, fall back onto file name

                        string artist = tags.FirstPerformer;
                        if(string.IsNullOrEmpty(artist) && tags is TagLib.Matroska.Tag mTags)
                        {
                            //workaround a bug or issue with taglib
                            string[] artistTag = mTags.Get("ARTIST");
                            if(artistTag != null && artistTag.Length > 0)
                            {
                                artist = artistTag[0];
                            }
                        }

                        if(!string.IsNullOrEmpty(input.Title))
                        {
                            if (string.IsNullOrEmpty(title))
                                continue;

                            //TODO probably make the ignore case configurable
                            if (!title.Contains(input.Title, StringComparison.CurrentCultureIgnoreCase))
                                continue;
                        }

                        if (!string.IsNullOrEmpty(input.Artist))
                        {
                            if (string.IsNullOrEmpty(artist))
                                continue;

                            //TODO should this be a looser compare?
                            if (!artist.Equals(input.Artist, StringComparison.CurrentCultureIgnoreCase))
                                continue;
                        }

                        string channelId = GetChannelIdFromTags(tags);
                        if (!string.IsNullOrEmpty(input.ChannelId))
                        {
                            
                            if(channelId == null || channelId != input.ChannelId)
                            {
                                continue;
                            }

                        }

                        string description = string.Format("Path: {0}\n{1}", filePath, GenerateDescriptionFromTags(tags));
                        results.Add(filePath, new SearchResult() { Description = description, FullPath = filePath, Name = fileName });
                    }
                    catch(Exception ex) 
                    {
                        //TODO log this somewhere
                        ;
                    }
                    
                }
                
            }

            return new SearchResultSet() { Results = results };
        }

        private static string GenerateDescriptionFromTags(TagLib.Tag tags)
        {
            StringBuilder sb = new StringBuilder();
            string title = tags.Title;
            sb.AppendFormat("Title: {0}\n", title);

            string? artist = tags.FirstPerformer;
            string? channelId = null;
            string? date = null;
            string? url = null;
            string? comments = tags.Comment ?? tags.Description;

            if (tags is TagLib.Matroska.Tag mTags)
            {
                if (string.IsNullOrEmpty(artist))
                {
                    //workaround a bug or issue with taglib
                    string[] artistTag = mTags.Get("ARTIST");
                    if (artistTag != null && artistTag.Length > 0)
                    {
                        artist = artistTag[0];
                    }
                }

                var channelIdTags = mTags.Get("CHANNEL_ID");
                if (channelIdTags != null && channelIdTags.Length > 0)
                    channelId = channelIdTags[0];

                var purlTags = mTags.Get("PURL");
                if (purlTags != null && purlTags.Length > 0)
                    url = purlTags[0];

                var dateTags = mTags.Get("DATE");
                if (dateTags != null && dateTags.Length > 0)
                    date = dateTags[0];

                if(string.IsNullOrEmpty(comments))
                {
                    var commentTags = mTags.Get("COMMENT");
                    if (commentTags != null && commentTags.Length > 0)
                        comments = commentTags[0];
                }

                if (string.IsNullOrEmpty(comments))
                {
                    var descriptionTags = mTags.Get("DESCRIPTION");
                    if (descriptionTags != null && descriptionTags.Length > 0)
                        comments = descriptionTags[0];
                }
            }

            if (!string.IsNullOrEmpty(artist))
                sb.AppendFormat("Artist: {0}\n", artist);
            if(!string.IsNullOrEmpty(channelId))
                sb.AppendFormat("Channel ID: {0}\n", channelId);
            if (!string.IsNullOrEmpty(date))
                sb.AppendFormat("Upload Date: {0}\n", date);
            if (!string.IsNullOrEmpty(url))
                sb.AppendFormat("URL: {0}\n", url);
            if (!string.IsNullOrEmpty(comments))
                sb.AppendFormat("Comments:\n{0}\n", comments);

            return sb.ToString();
        }

        private static string? GetChannelIdFromTags(TagLib.Tag tags)
        {
            if(tags is TagLib.Matroska.Tag mTags)
            {
                var channelIdTags = mTags.Get("CHANNEL_ID");
                if (channelIdTags != null && channelIdTags.Length > 0)
                    return channelIdTags[0];
            }

            return null;
        }

        private static void GetFoldersRecurse(string folderPath, bool excludeSpecial, string excludePath, List<string> foldersToCheck)
        {
            if (!string.IsNullOrEmpty(excludePath) && folderPath == excludePath)
                return;

            foreach(var directory in Directory.EnumerateDirectories(folderPath))
            {
                string folderName = Path.GetFileName(directory);
                if (excludeSpecial && folderName.StartsWith("!_"))
                    continue;

                GetFoldersRecurse(directory, excludeSpecial, excludePath, foldersToCheck);
            }

            //base case
            foldersToCheck.Add(folderPath);
        }

        public SearchResult GetResult(string name) => Results[name];

        public List<DisplayableSearchResult> GetDisplayableSearchResults()
        {
            return Results.Select(r => new DisplayableSearchResult() { FileName = r.Value.Name, FilePath = r.Key }).ToList();
        }

    }
}
