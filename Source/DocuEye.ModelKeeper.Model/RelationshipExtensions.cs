using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.ModelKeeper.Model
{
    /// <summary>
    /// Extension methods for relationships
    /// </summary>
    public static class RelationshipExtensions
    {
        public static bool IsDataEqual(this Relationship obj, Relationship compareTo)
        {
            // If null or the same, return true
            if (ReferenceEquals(obj, compareTo)) return true;

            // If one of them is null, return false
            if ((obj == null) || (compareTo == null)) return false;

            if(obj.Tags != null && compareTo.Tags != null)
            {
                if (!obj.Tags.SequenceEqual(compareTo.Tags)) return false;
            }
            else if ((obj.Tags != null && compareTo.Tags == null) || obj.Tags == null && compareTo.Tags != null)
            {
                return false;
            }


            var result = obj.SourceId == compareTo.SourceId
                   && obj.DestinationId == compareTo.DestinationId
                   //&& obj.SourceName == compareTo.SourceName
                   //&& obj.DestinationName == compareTo.DestinationName
                   && obj.InteractionStyle == compareTo.InteractionStyle
                   && obj.Url == compareTo.Url
                   && obj.Technology == compareTo.Technology
                   && obj.Description == compareTo.Description;
            return result;
        }

        public static IDictionary<string, Tuple<string, string>> GetDataChanges(this Relationship obj, Relationship compareTo)
        {
            var changes = new Dictionary<string, Tuple<string, string>>();
            if (obj.Description != compareTo.Description)
            {
                changes.Add("Description", new Tuple<string, string>(obj.Description ?? "", compareTo.Description ?? ""));
            }
            if (obj.Technology != compareTo.Technology)
            {
                changes.Add("Technology", new Tuple<string, string>(obj.Technology ?? "", compareTo.Technology ?? ""));
            }

            return changes;
        }
    }
}
