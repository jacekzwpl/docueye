using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DocuEye.ModelKeeper.Model
{
    /// <summary>
    /// Extensions methods for element
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Checks if data of two elements is the same
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="compareTo"></param>
        /// <returns>true if data is the same. False otherwise</returns>
        public static bool IsDataEqual(this Element obj, Element compareTo)
        {
            // If null or the same, return true
            if (ReferenceEquals(obj, compareTo)) return true;

            // If one of them is null, return false
            if ((obj == null) || (compareTo == null)) return false;

            if(obj.Tags != null && compareTo.Tags != null && !obj.Tags.SequenceEqual(compareTo.Tags)) return false;

            if(!ArePropertiesEqual(obj.Properties, compareTo.Properties)) {
                return false;
            }

            var result =  obj.Name == compareTo.Name
                   && obj.Url == compareTo.Url
                   && obj.Group == compareTo.Group
                   && obj.Location == compareTo.Location
                   && obj.Technology == compareTo.Technology
                   && obj.Description == compareTo.Description
                   && obj.InstanceOfId == compareTo.InstanceOfId;
            return result;
        }
        /// <summary>
        /// Gets information about data changes
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public static IDictionary<string, Tuple<string, string>> GetDataChanges(this Element obj, Element compareTo)
        {
            var changes = new Dictionary<string, Tuple<string, string>>();
            if(obj.Name != compareTo.Name)
            {
                changes.Add("Name", new Tuple<string,string>(obj.Name, compareTo.Name));
            }
            if (obj.Technology != compareTo.Technology)
            {
                changes.Add("Technology", new Tuple<string, string>(obj.Technology ?? string.Empty, compareTo.Technology ?? string.Empty));
            }

            return changes;
        }

        public static Element Clone(this Element obj)
        {
            return new Element()
            {
                Description = obj.Description,
                Group = obj.Group,
                Id = obj.Id,
                InstanceOfId = obj.InstanceOfId,
                Location = obj.Location,
                Name = obj.Name,
                ParentId = obj.ParentId,
                Properties = obj.Properties,
                Tags = obj.Tags,
                Technology = obj.Technology,
                Type = obj.Type,
                Url = obj.Url,
                WorkspaceId = obj.WorkspaceId,
                StructurizrId = obj.StructurizrId,
                DslId = obj.DslId,
                SourceCodeUrl = obj.SourceCodeUrl,
                OwnerTeam = obj.OwnerTeam
            };
        }

        private static bool ArePropertiesEqual(IDictionary<string, string>? objProps, IDictionary<string, string>? compareToProps)
        {
            if ((objProps == null) && (compareToProps != null) || (objProps != null) && (compareToProps == null)) return false;

            if(objProps == null && compareToProps == null) return true;

            if (objProps?.Count != compareToProps?.Count)
                return false;
            if (objProps?.Keys.Except(compareToProps?.Keys).Any() == true)
                return false;
            if (compareToProps?.Keys.Except(objProps?.Keys).Any() == true)
                return false;
            if(objProps != null && compareToProps != null)
            {
                foreach (var pair in objProps)
                {
                    if (pair.Value != compareToProps[pair.Key])
                    {
                        return false;
                    }
                }   
            }
            
            return true;
        }
    }
}
