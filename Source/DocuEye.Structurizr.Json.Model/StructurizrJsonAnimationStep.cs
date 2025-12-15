using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonAnimationStep
    {
        /// <summary>
        /// The order of this animation step.
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// The set of element IDs that should be included in this animation step.
        /// </summary>
        public IEnumerable<string>? Elements { get; set; }
        /// <summary>
        /// The set of relationship IDs that should be included in this animation step.
        /// </summary>
        public IEnumerable<string>? Relationships { get; set; }
    }
}
